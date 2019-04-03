using System.Collections.Generic;

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// Constants
///////////////////////////////////////////////////////////////////////////////
const string ProjectName = "Utilities";

var solutionFile = File($"./src/{ProjectName}.sln");
var projectFile = File($"./src/{ProjectName}/{ProjectName}.csproj");
var artifactsDirectory = Directory("./build_artifacts");

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////
Task("Clean-Environment")
    .Does(() =>
{
    Information("Cleaning bin directories.");
    CleanDirectories(GetDirectories($"./**/bin/{configuration}"));
    
    Information("Cleaning obj directories.");
    CleanDirectories(GetDirectories($"./**/obj/{configuration}"));
    
    Information("Cleaning artifacts directory.");
    CleanDirectory(artifactsDirectory);
});

Task("Restore-Nuget-Packages")
    .IsDependentOn("Clean-Environment")
    .Does(() =>
{
    NuGetRestore(solutionFile);
});

Task("Build")
    .IsDependentOn("Restore-Nuget-Packages")
    .Does(() =>
{
    MSBuild(
        solutionFile,
        new MSBuildSettings()
            .WithTarget("Rebuild")
            .SetVerbosity(Verbosity.Minimal)
            .SetConfiguration(configuration));
});

Task("Pack-Nuget")
    .IsDependentOn("Build")
    .Does(() =>
{
    NuGetPack(
        projectFile,
        new NuGetPackSettings { 
            OutputDirectory = artifactsDirectory,
            Properties = new Dictionary<string, string> 
				{
					{ "Configuration", configuration }
                },
            });
});

Task("Default")
    .IsDependentOn("Pack-Nuget");

RunTarget(target);

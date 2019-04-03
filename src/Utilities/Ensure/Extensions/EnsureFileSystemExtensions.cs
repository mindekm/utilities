namespace Utilities.Extensions
{
    using System;
    using System.IO;

    public static class EnsureFileSystemExtensions
    {
        public static void Exists(this in That<FileSystemInfo> that)
        {
            switch (that.Item)
            {
                case FileInfo file:
                    that.Exists(new FileNotFoundException($"File {file.FullName} not found."));
                    break;
                case DirectoryInfo directory:
                    that.Exists(new DirectoryNotFoundException($"Directory {directory.FullName} not found."));
                    break;
                default:
                    that.Exists(Error.EnsureFailure());
                    break;
            }
        }

        public static void Exists(this in That<FileSystemInfo> that, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));

            if (!that.Item.Exists)
            {
                throw exception;
            }
        }
    }
}
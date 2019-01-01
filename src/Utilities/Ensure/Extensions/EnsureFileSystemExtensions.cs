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
                    that.Exists(new EnsureException());
                    break;
            }
        }

        public static void Exists<TException>(
            this in That<FileSystemInfo> that,
            TException exception)
            where TException : Exception
        {
            Guard.NotNull(exception, nameof(exception));

            that.Exists(() => exception);
        }

        public static void Exists<TException>(
            this in That<FileSystemInfo> that,
            Func<TException> exceptionFactory)
            where TException : Exception
        {
            that.Exists(_ => exceptionFactory());
        }

        public static void Exists<TException>(
            this in That<FileSystemInfo> that,
            Func<FileSystemInfo, TException> exceptionFactory)
            where TException : Exception
        {
            that.Passes(info => info.Exists, exceptionFactory);
        }
    }
}
namespace Utilities.Benchmark
{
    using System;
    using BenchmarkDotNet.Running;

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<GuardVirtualCallBenchmark>();

            Console.ReadKey();
        }
    }
}

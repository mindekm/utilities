namespace Utilities.Benchmark
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;

    public class GuardVirtualCallBenchmark
    {
        private readonly HashSet<string> set;

        public GuardVirtualCallBenchmark()
        {
            var list = Enumerable.Repeat(Guid.NewGuid().ToString(), 100).ToList();
            set = new HashSet<string>(list);
        }

        [Benchmark]
        public void SetDirect()
        {
            Guard.HasElements(set, null);
        }

        [Benchmark]
        public void SetVirtual()
        {
            Guard.HasElements((ICollection<string>)set, null);
        }
    }
}
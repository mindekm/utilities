using BenchmarkDotNet.Attributes;

namespace Utilities.Benchmark;

[MemoryDiagnoser]
public class GuardVirtualCallBenchmark
{
    private HashSet<string> set;
    private ICollection<string> collection;

    [GlobalSetup]
    public void Setup()
    {
        var list = Enumerable.Repeat(Guid.NewGuid().ToString(), 100).ToList();
        set = new HashSet<string>(list);
        collection = set;
    }

    [Benchmark(Baseline = true)]
    public HashSet<string> SetDirect()
    {
        Guard.HasElements(set);

        return set;
    }

    [Benchmark]
    public ICollection<string> SetVirtual()
    {
        Guard.HasElements(collection);

        return collection;
    }
}
using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;

namespace Utilities.Benchmark;

[Config(typeof(Config))]
[MemoryDiagnoser]
[HideColumns(Column.RatioSD)]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class UnsafeBenchmark
{
    private class Config : ManualConfig
    {
        public Config()
        {
            SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Percentage);
        }
    }

    private Guid valueType;
    private string referenceType;

    [GlobalSetup]
    public void Setup()
    {
        valueType = Guid.NewGuid();
        referenceType = Guid.NewGuid().ToString();
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("value type")]
    public Maybe<Guid> SafeValueType()
    {
        return Maybe.Some(valueType);
    }

    [Benchmark]
    [BenchmarkCategory("value type")]
    public Maybe<Guid> UnsafeValueType()
    {
        return Maybe.UnsafeSome(valueType);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("reference type")]
    public Maybe<string> SafeReferenceType()
    {
        return Maybe.Some(referenceType);
    }

    [Benchmark]
    [BenchmarkCategory("reference type")]
    public Maybe<string> UnsafeReferenceType()
    {
        return Maybe.UnsafeSome(referenceType);
    }
}

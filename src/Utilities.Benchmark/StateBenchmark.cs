namespace Utilities.Benchmark;

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;

[Config(typeof(Config))]
[MemoryDiagnoser]
[HideColumns(Column.RatioSD)]
public class StateBenchmark
{
    private class Config : ManualConfig
    {
        public Config()
        {
            SummaryStyle = SummaryStyle.Default.WithRatioStyle(RatioStyle.Percentage);
        }
    }

    [Benchmark]
    public string WithState()
    {
        var initial = Maybe.Some(Guid.NewGuid().ToString());
        var add = Guid.NewGuid().ToString();
        var add2 = Guid.NewGuid().ToString();

        return initial.Map((add, add2), static (v, state) => string.Concat(v, state.add, state.add2)).Unwrap();
    }

    [Benchmark(Baseline = true)]
    public string WithoutState()
    {
        var initial = Maybe.Some(Guid.NewGuid().ToString());
        var add = Guid.NewGuid().ToString();
        var add2 = Guid.NewGuid().ToString();

        return initial.Map(v => string.Concat(v, add, add2)).Unwrap();
    }
}

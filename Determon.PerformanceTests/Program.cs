using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ShaiRandom.Generators;

namespace Determon.PerformanceTests
{
    ///<summary>
    ///|    Method |         Mean |      Error |     StdDev |       Median |
    ///|---------- |-------------:|-----------:|-----------:|-------------:|
    ///| BaselineM |    68.198 ns |  1.2059 ns |  1.1280 ns |    67.490 ns |
    ///|      SinM | 2,294.860 ns | 17.1421 ns | 13.3834 ns | 2,298.089 ns |
    ///|      CosM | 2,619.419 ns | 51.8329 ns | 59.6909 ns | 2,594.356 ns |
    ///|      TanM | 3,739.113 ns | 71.7061 ns | 85.3610 ns | 3,681.668 ns |
    ///| BaselineD |     3.483 ns |  0.0986 ns |  0.1210 ns |     3.414 ns |
    ///|      SinD |    16.974 ns |  0.1640 ns |  0.1369 ns |    16.920 ns |
    ///|      CosD |    17.234 ns |  0.3602 ns |  0.4288 ns |    16.996 ns |
    ///|      TanD |    18.184 ns |  0.1991 ns |  0.1662 ns |    18.180 ns |
    ///| BaselineF |     3.978 ns |  0.1067 ns |  0.1270 ns |     3.915 ns |
    ///|      SinF |    19.037 ns |  0.4107 ns |  0.4730 ns |    18.778 ns |
    ///|      CosF |    18.963 ns |  0.4059 ns |  0.6075 ns |    18.594 ns |
    ///|      TanF |    17.187 ns |  0.1527 ns |  0.1192 ns |    17.223 ns |
    ///</summary>
    ///<remarks>
    ///The timing on the M benchmarks, for decimals, seems inaccurate between runs, but always extremely high.
    ///</remarks>
    public class TrigonometryComparison
    {
        private readonly MizuchiRandom _random = new MizuchiRandom(1UL);

        [Benchmark]
        public decimal BaselineM() => (_random.NextDecimal() * MathD.Tau - MathD.Pi);

        [Benchmark]
        public decimal SinM() => MathD.Sin(_random.NextDecimal() * MathD.Tau - MathD.Pi);
        [Benchmark]
        public decimal CosM() => MathD.Cos(_random.NextDecimal() * MathD.Tau - MathD.Pi);
        [Benchmark]
        public decimal TanM() => MathD.Tan((_random.NextDecimal() - 0.5M) * MathD.Pi - 0.999M);

        [Benchmark]
        public double BaselineD() => (_random.NextDouble() * Math.PI * 2.0 - Math.PI);
        [Benchmark]
        public double SinD() => Math.Sin(_random.NextDouble() * Math.PI * 2.0 - Math.PI);
        [Benchmark]
        public double CosD() => Math.Cos(_random.NextDouble() * Math.PI * 2.0 - Math.PI);
        [Benchmark]
        public double TanD() => Math.Tan((_random.NextDouble()- 0.5) * Math.PI * 0.999);

        [Benchmark]
        public double BaselineF() => (_random.NextFloat() * MathF.PI * 2f - MathF.PI);
        [Benchmark]
        public double SinF() => MathF.Sin(_random.NextFloat() * MathF.PI * 2f - MathF.PI);
        [Benchmark]
        public double CosF() => MathF.Cos(_random.NextFloat() * MathF.PI * 2f - MathF.PI);
        [Benchmark]
        public double TanF() => MathF.Tan((_random.NextFloat() - 0.5f) * MathF.PI * 0.999f);
    }

    internal static class Program
    {
        private static void Main(string[] args)
            => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}

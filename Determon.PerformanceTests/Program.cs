using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ShaiRandom.Generators;

namespace Determon.PerformanceTests
{
    ///<summary>
    ///|    Method |         Mean |      Error |     StdDev |
    ///|---------- |-------------:|-----------:|-----------:|
    ///| BaselineM |    68.428 ns |  0.7111 ns |  0.6304 ns |
    ///|      SinM | 2,334.630 ns | 43.1611 ns | 40.3729 ns |
    ///|      CosM | 2,562.045 ns | 28.4909 ns | 25.2564 ns |
    ///|      TanM | 2,989.138 ns | 58.1600 ns | 62.2305 ns |
    ///| BaselineD |     3.383 ns |  0.0267 ns |  0.0208 ns |
    ///|      SinD |    17.016 ns |  0.1117 ns |  0.0933 ns |
    ///|      CosD |    17.141 ns |  0.3321 ns |  0.3107 ns |
    ///|      TanD |    20.678 ns |  0.4331 ns |  0.4051 ns |
    ///</summary>
    ///<remarks>
    /// The timing on the M benchmarks, for decimals, seems inaccurate between runs, but always extremely high.
    /// </remarks>
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
        public decimal TanM() => MathD.Tan(_random.NextDecimal() - 0.5M);

        [Benchmark]
        public double BaselineD() => (_random.NextDouble() * Math.PI * 2.0 - Math.PI);
        [Benchmark]
        public double SinD() => Math.Sin(_random.NextDouble() * Math.PI * 2.0 - Math.PI);
        [Benchmark]
        public double CosD() => Math.Cos(_random.NextDouble() * Math.PI * 2.0 - Math.PI);
        [Benchmark]
        public double TanD() => Math.Tan(_random.NextDouble() * Math.PI * 2.0 - Math.PI);
    }

    internal static class Program
    {
        private static void Main(string[] args)
            => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}

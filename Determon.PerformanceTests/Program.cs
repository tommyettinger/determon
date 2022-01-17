using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ShaiRandom.Generators;

namespace Determon.PerformanceTests
{
    ///<summary>
    ///|    Method |         Mean |      Error |      StdDev |
    ///|---------- |-------------:|-----------:|------------:|
    ///| BaselineM |    69.602 ns |  1.4124 ns |   1.8365 ns |
    ///|      SinM | 4,217.138 ns | 82.2736 ns | 128.0901 ns |
    ///|      CosM | 2,526.321 ns | 20.2870 ns |  17.9839 ns |
    ///|      TanM | 3,414.984 ns | 63.0010 ns |  58.9312 ns |
    ///| BaselineD |     3.453 ns |  0.0945 ns |   0.1088 ns |
    ///|      SinD |    17.009 ns |  0.3147 ns |   0.2944 ns |
    ///|      CosD |    16.910 ns |  0.2236 ns |   0.1867 ns |
    ///|      TanD |    20.080 ns |  0.3018 ns |   0.2823 ns |
    ///</summary>
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

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
        public double TanD() => Math.Tan((_random.NextDouble() - 0.5) * Math.PI * 0.999);

        [Benchmark]
        public double BaselineF() => (_random.NextFloat() * MathF.PI * 2f - MathF.PI);
        [Benchmark]
        public double SinF() => MathF.Sin(_random.NextFloat() * MathF.PI * 2f - MathF.PI);
        [Benchmark]
        public double CosF() => MathF.Cos(_random.NextFloat() * MathF.PI * 2f - MathF.PI);
        [Benchmark]
        public double TanF() => MathF.Tan((_random.NextFloat() - 0.5f) * MathF.PI * 0.999f);
    }
    ///<summary>
    ///|        Method |         Mean |      Error |     StdDev |       Median |
    ///|-------------- |-------------:|-----------:|-----------:|-------------:|
    ///|     BaselineM |     5.734 ns |  0.0964 ns |  0.0902 ns |     5.785 ns |
    ///|         SqrtM | 1,355.962 ns | 22.5503 ns | 21.0936 ns | 1,348.748 ns |
    ///| SqrtEstimateM | 1,050.689 ns | 12.8492 ns | 12.0191 ns | 1,044.573 ns |
    ///|     BaselineD |     2.392 ns |  0.0767 ns |  0.0821 ns |     2.433 ns |
    ///|         SqrtD |     3.662 ns |  0.1011 ns |  0.1417 ns |     3.737 ns |
    ///|     BaselineF |     3.456 ns |  0.0964 ns |  0.1688 ns |     3.532 ns |
    ///|         SqrtF |     3.891 ns |  0.0851 ns |  0.0711 ns |     3.923 ns |
    ///</summary>
    ///<remarks>
    ///OK, 2 orders of magnitude slower to do decimal square roots, and then some...
    ///</remarks>
    public class SqrtComparison
    {
        private readonly MizuchiRandom _random = new MizuchiRandom(1UL);

        [Benchmark]
        public decimal BaselineM() => _random.NextDecimal();

        [Benchmark]
        public decimal SqrtM() => MathD.Sqrt(_random.NextDecimal());
        [Benchmark]
        public decimal SqrtEstimateM() => MathD.Sqrt(_random.NextDecimal(), 0.7M);

        [Benchmark]
        public double BaselineD() => _random.NextDouble();
        [Benchmark]
        public double SqrtD() => Math.Sqrt(_random.NextDouble());

        [Benchmark]
        public double BaselineF() => _random.NextFloat();
        [Benchmark]
        public double SqrtF() => MathF.Sqrt(_random.NextFloat());
    }
    ///<summary>
    ///|    Method |         Mean |       Error |      StdDev |
    ///|---------- |-------------:|------------:|------------:|
    ///| BaselineM |     4.826 ns |   0.0962 ns |   0.0900 ns |
    ///|      PowM | 7,610.649 ns | 148.9028 ns | 146.2425 ns |
    ///| BaselineD |     2.286 ns |   0.0753 ns |   0.1258 ns |
    ///|      PowD |    32.525 ns |   0.4157 ns |   0.3246 ns |
    ///| BaselineF |     3.410 ns |   0.0954 ns |   0.1645 ns |
    ///|      PowF |    25.626 ns |   0.3908 ns |   0.3655 ns |
    ///</summary>
    ///<remarks>
    ///A 200x to 300x difference, ouch....
    ///</remarks>
    public class PowComparison
    {
        private readonly MizuchiRandom _random = new MizuchiRandom(1UL);

        [Benchmark]
        public decimal BaselineM() => _random.NextDecimal();

        [Benchmark]
        public decimal PowM() => MathD.Pow(_random.NextDecimal(), 0.5M + _random.NextDecimal());

        [Benchmark]
        public double BaselineD() => _random.NextDouble();
        [Benchmark]
        public double PowD() => Math.Pow(_random.NextDouble(), 0.5 + _random.NextDouble());

        [Benchmark]
        public double BaselineF() => _random.NextFloat();
        [Benchmark]
        public double PowF() => MathF.Pow(_random.NextFloat(), 0.5f + _random.NextFloat());
    }

    ///<summary>
    ///</summary>
    ///<remarks>
    ///</remarks>
    public class Atan2Comparison
    {
        private readonly MizuchiRandom _random = new MizuchiRandom(1UL);

        [Benchmark]
        public decimal BaselineM() => _random.NextDecimal();

        [Benchmark]
        public decimal Atan2M() => MathD.Atan2(_random.NextDecimal() - 0.5M, _random.NextDecimal() - 0.5M);

        [Benchmark]
        public decimal Atan2NonNegativeM() => MathD.Atan2NonNegative(_random.NextDecimal() - 0.5M, _random.NextDecimal() - 0.5M);

        [Benchmark]
        public double BaselineD() => _random.NextDouble();
        [Benchmark]
        public double Atan2D() => Math.Atan2(_random.NextDouble() - 0.5, _random.NextDouble() - 0.5);

        [Benchmark]
        public double BaselineF() => _random.NextFloat();
        [Benchmark]
        public double Atan2F() => MathF.Atan2(_random.NextFloat() - 0.5f, _random.NextFloat() - 0.5f);
    }

    internal static class Program
    {
        private static void Main(string[] args)
            => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}

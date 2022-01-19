using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ShaiRandom.Generators;

namespace Determon.PerformanceTests
{
    ///<summary>
    ///|    Method |         Mean |      Error |     StdDev |
    ///|---------- |-------------:|-----------:|-----------:|
    ///| BaselineM |    69.554 ns |  1.2544 ns |  1.1734 ns |
    ///|      SinM | 2,302.758 ns | 45.4408 ns | 50.5073 ns |
    ///|      CosM | 2,553.580 ns | 36.9164 ns | 32.7254 ns |
    ///|      TanM | 3,652.843 ns | 21.6698 ns | 20.2699 ns |
    ///| BaselineD |     3.569 ns |  0.0973 ns |  0.0955 ns |
    ///|      SinD |    16.982 ns |  0.2945 ns |  0.2299 ns |
    ///|      CosD |    17.016 ns |  0.3588 ns |  0.4132 ns |
    ///|      TanD |    18.535 ns |  0.3997 ns |  0.5471 ns |
    ///| BaselineF |     4.031 ns |  0.1089 ns |  0.1596 ns |
    ///|      SinF |    19.721 ns |  0.0510 ns |  0.0452 ns |
    ///|      CosF |    18.929 ns |  0.3622 ns |  0.6248 ns |
    ///|      TanF |    17.529 ns |  0.3202 ns |  0.3559 ns |
    ///|     SinAM |   306.580 ns |  2.5453 ns |  1.9872 ns |
    ///|     CosAM |   321.819 ns |  1.3316 ns |  1.3078 ns |
    ///</summary>
    ///<remarks>
    ///The timing on the M benchmarks, for decimals, seems inaccurate between runs, but always extremely high. AM benchmarks are decimal approximations.
    ///</remarks>
    public class TrigonometryComparison
    {
        private readonly MizuchiRandom _random = new MizuchiRandom(1UL);

        [Benchmark]
        public decimal BaselineM() => (_random.NextDecimal() * MathM.Tau - MathM.Pi);

        [Benchmark]
        public decimal SinM() => MathM.Sin(_random.NextDecimal() * MathM.Tau - MathM.Pi);
        [Benchmark]
        public decimal CosM() => MathM.Cos(_random.NextDecimal() * MathM.Tau - MathM.Pi);
        [Benchmark]
        public decimal TanM() => MathM.Tan((_random.NextDecimal() - 0.5M) * MathM.Pi - 0.999M);

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

        [Benchmark]
        public decimal SinAM() => ApproxM.Sin(_random.NextDecimal() * MathM.Tau - MathM.Pi);
        [Benchmark]
        public decimal CosAM() => ApproxM.Cos(_random.NextDecimal() * MathM.Tau - MathM.Pi);
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
        public decimal SqrtM() => MathM.Sqrt(_random.NextDecimal());
        [Benchmark]
        public decimal SqrtEstimateM() => MathM.Sqrt(_random.NextDecimal(), 0.7M);

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
        public decimal PowM() => MathM.Pow(_random.NextDecimal(), 0.5M + _random.NextDecimal());

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
    ///|            Method |         Mean |       Error |      StdDev |       Median |
    ///|------------------ |-------------:|------------:|------------:|-------------:|
    ///|         BaselineM |     5.134 ns |   0.1257 ns |   0.1762 ns |     5.136 ns |
    ///|            Atan2M | 6,639.725 ns | 130.5516 ns | 218.1223 ns | 6,680.252 ns |
    ///| Atan2NonNegativeM | 6,688.209 ns | 130.1905 ns | 224.5723 ns | 6,704.941 ns |
    ///|         BaselineD |     2.372 ns |   0.0742 ns |   0.1111 ns |     2.363 ns |
    ///|            Atan2D |    34.196 ns |   0.7108 ns |   1.2070 ns |    34.740 ns |
    ///|         BaselineF |     3.356 ns |   0.0954 ns |   0.1305 ns |     3.384 ns |
    ///|            Atan2F |    30.018 ns |   0.6190 ns |   0.8677 ns |    30.380 ns |
    ///</summary>
    ///<remarks>
    ///Again, about a 200x difference. Ouch.
    ///</remarks>
    public class Atan2Comparison
    {
        private readonly MizuchiRandom _random = new MizuchiRandom(1UL);

        [Benchmark]
        public decimal BaselineM() => _random.NextDecimal();

        [Benchmark]
        public decimal Atan2M() => MathM.Atan2(_random.NextDecimal() - 0.5M, _random.NextDecimal() - 0.5M);

        [Benchmark]
        public decimal Atan2NonNegativeM() => MathM.Atan2NonNegative(_random.NextDecimal() - 0.5M, _random.NextDecimal() - 0.5M);

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

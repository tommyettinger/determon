using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Determon.PerformanceTests
{
//    public class RandomULongBoundedComparison
//    {
//
//        private readonly DistinctRandom _distinctRandom = new DistinctRandom(1UL);
//        private readonly LaserRandom _laserRandom = new LaserRandom(1UL);
//        private readonly TricycleRandom _tricycleRandom = new TricycleRandom(1UL);
//        private readonly FourWheelRandom _fourWheelRandom = new FourWheelRandom(1UL);
//        private readonly StrangerRandom _strangerRandom = new StrangerRandom(1UL);
//        private readonly Xoshiro256StarStarRandom _xoshiro256StarStarRandom = new Xoshiro256StarStarRandom(1UL);
//        private readonly RomuTrioRandom _romuTrioRandom = new RomuTrioRandom(1UL);
//        private readonly MizuchiRandom _mizuchiRandom = new MizuchiRandom(1UL);
//
//#if NET6_0
//        private readonly System.Random _seededRandom = new System.Random(1);
//        private readonly System.Random _unseededRandom = new System.Random();
//
//        [Benchmark]
//        public long Seeded() => _seededRandom.NextInt64(1L, 1000L);
//
//        [Benchmark]
//        public long Unseeded() => _unseededRandom.NextInt64(1L, 1000L);
//#endif
//
//        [Benchmark]
//        public ulong Distinct() => _distinctRandom.NextULong(1UL, 1000UL);
//
//        [Benchmark]
//        public ulong Laser() => _laserRandom.NextULong(1UL, 1000UL);
//
//        [Benchmark]
//        public ulong Tricycle() => _tricycleRandom.NextULong(1UL, 1000UL);
//
//        [Benchmark]
//        public ulong FourWheel() => _fourWheelRandom.NextULong(1UL, 1000UL);
//
//        [Benchmark]
//        public ulong Stranger() => _strangerRandom.NextULong(1UL, 1000UL);
//
//        [Benchmark]
//        public ulong XoshiroStarStar() => _xoshiro256StarStarRandom.NextULong(1UL, 1000UL);
//
//        [Benchmark]
//        public ulong RomuTrio() => _romuTrioRandom.NextULong(1UL, 1000UL);
//
//        [Benchmark]
//        public ulong Mizuchi() => _mizuchiRandom.NextULong(1UL, 1000UL);
//    }

    internal static class Program
    {
        private static void Main(string[] args)
            => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}

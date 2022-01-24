using System;
using Xunit;
using Xunit.Abstractions;

namespace Determon.UnitTests
{
    public class MathMTests
    {
        private const decimal Epsilon = ApproxM.Epsilon;
        private const int TestCount = 1000;
        private readonly Random _random = new Random();

        private readonly ITestOutputHelper _debug;

        public MathMTests(ITestOutputHelper output)
        {
            _debug = output;
        }
        [Fact]
        public void FactExp()
        {
            for (int i = 0; i < TestCount; i++)
            {
                double d = _random.NextDouble() * 4.0;
                decimal d1 = (decimal)d;
                decimal d2 = d1;
                d = Math.Exp(d);
                d1 = MathM.Exp(d1);
                d2 = ApproxM.Exp(d2);
                _debug.WriteLine("d=" + d);
                _debug.WriteLine("d1=" + d1);
                _debug.WriteLine("d2=" + d2);
                Assert.True(Math.Abs((decimal)d - d1) < Epsilon);
                Assert.True(Math.Abs((decimal)d - d2) < Epsilon);
            }
        }
        [Fact]
        public void FactSqrt()
        {
            for (int i = 0; i < TestCount; i++)
            {
                double d = _random.NextDouble() * 65536.0;
                decimal d1 = (decimal)d;
                decimal d2 = d1;
                d = Math.Sqrt(d);
                d1 = MathM.Sqrt(d1);
                d2 = ApproxM.Sqrt(d2);
                Assert.True(Math.Abs((decimal)d - d1) < Epsilon);
                Assert.True(Math.Abs((decimal)d - d2) < Epsilon);
            }
        }
        [Fact]
        public void FactCbrt()
        {
            for (int i = 0; i < TestCount; i++)
            {
                double d = _random.NextDouble() * 65536.0;
                decimal d1 = (decimal)d;
                d = Math.Cbrt(d);
                d1 = MathM.Cbrt(d1);
                Assert.True(Math.Abs((decimal)d - d1) < Epsilon);
            }
        }
        [Fact]
        public void FactLog()
        {
            for (int i = 0; i < TestCount; i++)
            {
                double d = (1.0 - _random.NextDouble()) * 65536.0;
                decimal d1 = (decimal)d;
                d = Math.Log(d);
                d1 = MathM.Log(d1);
                Assert.True(Math.Abs((decimal)d - d1) < Epsilon);
            }
        }

        [Fact]
        public void FactLog10()
        {
            for (int i = 0; i < TestCount; i++)
            {
                double d = (1.0 - _random.NextDouble()) * 65536.0;
                decimal d1 = (decimal)d;
                d = Math.Log10(d);
                d1 = MathM.Log10(d1);
                Assert.True(Math.Abs((decimal)d - d1) < Epsilon);
            }
        }
        [Fact]
        public void FactAsin()
        {
            for (int i = 0; i < TestCount; i++)
            {
                double d = _random.NextDouble() - 0.5;
                decimal d1 = (decimal)d;
                d = Math.Asin(d);
                d1 = MathM.Asin(d1);
                Assert.True(Math.Abs((decimal)d - d1) < Epsilon);
            }
        }
        [Fact]
        public void FactAcos()
        {
            for (int i = 0; i < TestCount; i++)
            {
                double d = _random.NextDouble() - 0.5;
                decimal d1 = (decimal)d;
                d = Math.Acos(d);
                d1 = MathM.Acos(d1);
                Assert.True(Math.Abs((decimal)d - d1) < Epsilon);
            }
        }
        [Fact]
        public void FactAtan()
        {
            for (int i = 0; i < TestCount; i++)
            {
                double d = _random.NextDouble() - 0.5;
                decimal d1 = (decimal)d;
                d = Math.Atan(d);
                d1 = MathM.Atan(d1);
                Assert.True(Math.Abs((decimal)d - d1) < Epsilon);
            }
        }
        [Fact]
        public void FactSin()
        {
            for (int i = 0; i < TestCount; i++)
            {
                double d = _random.NextDouble() - 0.5;
                decimal d1 = (decimal)d;
                d = Math.Sin(d);
                d1 = MathM.Sin(d1);
                Assert.True(Math.Abs((decimal)d - d1) < Epsilon);
            }
        }
        [Fact]
        public void FactCos()
        {
            for (int i = 0; i < TestCount; i++)
            {
                double d = _random.NextDouble() - 0.5;
                decimal d1 = (decimal)d;
                d = Math.Cos(d);
                d1 = MathM.Cos(d1);
                Assert.True(Math.Abs((decimal)d - d1) < Epsilon);
            }
        }
        [Fact]
        public void FactTan()
        {
            for (int i = 0; i < TestCount; i++)
            {
                double d = _random.NextDouble() - 0.5;
                decimal d1 = (decimal)d;
                d = Math.Tan(d);
                d1 = MathM.Tan(d1);
                Assert.True(Math.Abs((decimal)d - d1) < Epsilon);
            }
        }
        [Fact]
        public void FactAtan2()
        {
            for (int i = 0; i < TestCount; i++)
            {
                double x = _random.NextDouble() - 0.5;
                double y = _random.NextDouble() - 0.5;
                decimal dx = (decimal)x;
                decimal dy = (decimal)y;
                var d = Math.Atan2(y, x);
                var z = MathM.Atan2(dy, dx);
                Assert.True(Math.Abs((decimal)d - z) < Epsilon);
            }
        }
        [Fact]
        public void FactAtan2NonNegative()
        {
            for (int i = 0; i < TestCount; i++)
            {
                double x = _random.NextDouble() - 0.5;
                double y = _random.NextDouble() - 0.5;
                decimal dx = (decimal)x;
                decimal dy = (decimal)y;
                var d = Math.Atan2(y, x);
                if (d < 0.0) d += Math.PI + Math.PI;
                var z = MathM.Atan2NonNegative(dy, dx);
                Assert.True(Math.Abs((decimal)d - z) < Epsilon);
            }
        }
        [Fact]
        public void FactPow001()
        {
            double x = 10;
            double y = -5;
            double result = Math.Pow(x, y);

            Assert.Equal(1E-5, result);

            decimal dx = 10;
            decimal dy = -5;
            decimal dResult = MathM.Pow(dx, dy);

            Assert.Equal(1E-5M, dResult);
        }
        [Fact]
        public void FactPow002()
        {
            double x = 10;
            double y = 5;
            double result = Math.Pow(x, y);

            Assert.Equal(1E+5, result);

            decimal dx = 10;
            decimal dy = 5;
            decimal dResult = MathM.Pow(dx, dy);

            Assert.Equal(1E+5m, dResult);
        }

    }
}

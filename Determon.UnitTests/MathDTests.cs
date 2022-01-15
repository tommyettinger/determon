using System;
using Xunit;
using Xunit.Abstractions;

namespace Determon.UnitTests
{
    public class MathDTests
    {
        static decimal epsilon = 0.000000000001M;
        static int testCount = 1000;
        static readonly Random Random = new Random();

        private readonly ITestOutputHelper Debug;

        public MathDTests(ITestOutputHelper output)
        {
            this.Debug = output;
        }
        [Fact]
        public void FactExp()
        {
            for (int i = 0; i < testCount; i++)
            {
                double d = Random.NextDouble();
                decimal d1 = (decimal)d;
                d = Math.Exp(d);
                d1 = MathD.Exp(d1);
                Debug.WriteLine("d=" + d);
                Debug.WriteLine("d1=" + d1);
                Assert.True(MathD.Abs((decimal)d - d1) < epsilon);
            }
        }
        [Fact]
        public void FactAsin()
        {
            for (int i = 0; i < testCount; i++)
            {
                double d = Random.NextDouble();
                decimal d1 = (decimal)d;
                d = Math.Asin(d);
                d1 = MathD.Asin(d1);
                Assert.True(MathD.Abs((decimal)d - d1) < epsilon);
            }
        }
        [Fact]
        public void FactAcos()
        {
            for (int i = 0; i < testCount; i++)
            {
                double d = Random.NextDouble();
                decimal d1 = (decimal)d;
                d = Math.Acos(d);
                d1 = MathD.Acos(d1);
                Assert.True(MathD.Abs((decimal)d - d1) < epsilon);
            }
        }
        [Fact]
        public void FactAtan()
        {
            for (int i = 0; i < testCount; i++)
            {
                double d = Random.NextDouble();
                decimal d1 = (decimal)d;
                d = Math.Atan(d);
                d1 = MathD.Atan(d1);
                Assert.True(MathD.Abs((decimal)d - d1) < epsilon);
            }
        }
        [Fact]
        public void FactSin()
        {
            for (int i = 0; i < testCount; i++)
            {
                double d = Random.NextDouble();
                decimal d1 = (decimal)d;
                d = Math.Sin(d);
                d1 = MathD.Sin(d1);
                Assert.True(MathD.Abs((decimal)d - d1) < epsilon);
            }
        }
        [Fact]
        public void FactCos()
        {
            for (int i = 0; i < testCount; i++)
            {
                double d = Random.NextDouble();
                decimal d1 = (decimal)d;
                d = Math.Cos(d);
                d1 = MathD.Cos(d1);
                Assert.True(MathD.Abs((decimal)d - d1) < epsilon);
            }
        }
        [Fact]
        public void FactAtan2()
        {
            for (int i = 0; i < testCount; i++)
            {
                double x = Random.NextDouble();
                double y = Random.NextDouble();
                decimal dx = (decimal)x;
                decimal dy = (decimal)y;
                var d = Math.Atan2(y, x);
                var z = MathD.Atan2(dy, dx);
                Assert.True(MathD.Abs((decimal)d - z) < epsilon);
            }
        }
        [Fact]
        public void FactPow001()
        {
            double x = 10;
            double y = -5;
            double result = Math.Pow(x, y);

            Assert.Equal(1E-05, result);

            decimal dx = 10;
            decimal dy = -5;
            decimal dResult = MathD.Pow(dx, dy);

            Assert.Equal(0.00001m, dResult);
        }
        [Fact]
        public void FactPow002()
        {
            double x = 10;
            double y = 5;
            double result = Math.Pow(x, y);

            Assert.Equal(100000, result);

            decimal dx = 10;
            decimal dy = 5;
            decimal dResult = MathD.Pow(dx, dy);

            Assert.Equal(100000m, dResult);
        }

    }
}

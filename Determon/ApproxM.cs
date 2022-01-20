﻿using System;

namespace Determon
{
    /// <summary>
    /// Like MathM, but meant to be faster at the expense of mathematical accuracy past about 6 digits (without losing determinism).
    /// </summary>
    public static class ApproxM
    {
        /// <summary>
        /// The irrational number pi, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal Pi = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214M;
        /// <summary>
        /// 2M divided by the irrational number pi, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal InverseHalfPi = 2.0M / 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214M;
        /// <summary>
        /// The irrational number pi divided by 2M, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal HalfPi = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214M * 0.5M;
        /// <summary>
        /// The irrational number pi times 2M, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal Pi2 = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214M * 2M;
        /// <summary>
        /// The irrational number pi times 2M, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        /// <remarks>
        /// This is an alias for <see cref="Pi2">Pi2</see>.
        /// </remarks>
        public const decimal Tau = Pi2;
        /// <summary>
        /// The constant multiplier that converts from a measurement in radians to a measurement in degrees, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal RadianToDegree = 180M / 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214M;
        /// <summary>
        /// The constant multiplier that converts from a measurement in degrees to a measurement in radians, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal DegreeToRadian = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214M / 180M;
        /// <summary>
        /// The constant multiplier that converts from a measurement in radians to a measurement in turns, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        /// <remarks>
        /// Using turns as a measurement of angles, where 1 turn equals 2 * Pi radians, can help with some calculations. It works well if you subtract <see cref="decimal.Truncate(decimal)"/> when you
        /// want a measurement between 0 and 1 turn.
        /// </remarks>
        public const decimal RadianToTurn = 0.5M / 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214M;
        /// <summary>
        /// The constant multiplier that converts from a measurement in turns to a measurement in radians, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        /// <remarks>
        /// Using turns as a measurement of angles, where 1 turn equals 2 * Pi radians, can help with some calculations. It works well if you subtract <see cref="decimal.Truncate(decimal)"/> when you
        /// want a measurement between 0 and 1 turn.
        /// </remarks>
        public const decimal TurnToRadian = Pi2;
        /// <summary>
        /// The irrational number e, Euler's totient, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal E = 2.71828182845904523536028747135266249775724709369995957496696762772407663035354759457138217852516642742746M;
        /// <summary>
        /// The irrational number phi, the Golden Ratio, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal Phi = 1.61803398874989484820458683436563811772030917980576286213544862270526046281890244970720720418939113748475M;

        /// <summary>
        /// The smallest positive value this permits as tolerance in equality comparisons.
        /// </summary>
        /// <remarks>
        /// This is more permissive than the Epsilon in MathM.
        /// </remarks>
        public const decimal Epsilon = 0.0000001M;

        /// <summary>
        /// The smallest negative value this permits as tolerance in equality comparisons.
        /// </summary>
        /// <remarks>
        /// This is more permissive than the Epsilon in MathM.
        /// </remarks>
        internal const decimal NEpsilon = -Epsilon;

        /// <summary>
        /// The irrational number pi divided by 4M, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal QuarterPi = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214M * 0.25M;

        /// <summary>
        /// 1.0M divided by e (Euler's totient), to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        private const decimal InverseE = 1M / E;

        /// <summary>
        /// log(10,E) factor
        /// </summary>
        private const decimal InverseLog10 = 0.434294481903251827651128918916605082294397005803666566114M;

        /// <summary>
        /// Represents 0.5M .
        /// </summary>
        private const decimal Half = 0.5M;

        /// <summary>
        /// 1.0M divided by 3.0M. Exactly what it says.
        /// </summary>
        private const decimal Third = decimal.One / 3M;

        /// <summary>
        /// A square root method for decimals.
        /// </summary>
        /// <param name="d">The square to find the square root of.</param>
        /// <returns>The approximate square root of d, as well as a decimal can represent it.</returns>
        public static decimal Sqrt(decimal d)
        {
            decimal guess = d * Half, previous;
            do
            {
                if (guess <= decimal.Zero) return decimal.Zero;
                previous = guess;
                guess = (previous + d / previous) * Half;
            }
            while (Math.Abs(previous - guess) > Epsilon);
            return guess;
        }

        /// <summary>
        /// A variant square root method for decimals that allows specifying an approximate guess, or seed value, that is likely to be close to the actual square root.
        /// </summary>
        /// <remarks>
        /// This has precision up to <see cref="Epsilon"/>. Credit to Bobson for coming up with this recursive method: https://stackoverflow.com/a/13282997/786740
        /// </remarks>
        /// <param name="d">The square to find the square root of.</param>
        /// <param name="guess">A decimal that is hopefully relatively close to the actual square root, used to speed up the algorithm if it is close.</param>
        /// <returns>The exact square root of d, as well as a decimal can represent it.</returns>
        public static decimal Sqrt(decimal d, decimal guess)
        {
            decimal previous;
            do
            {
                if (guess == decimal.Zero) return decimal.Zero;
                previous = guess;
                guess = (previous + d / previous) * Half;
            }
            while (Math.Abs(previous - guess) > Epsilon);
            return guess;
        }

        public static decimal Sin(decimal radians)
        {
            radians *= InverseHalfPi;
            long floor = (radians >= 0M ? (long)radians : (long)radians - 1L) & -2L;
            radians -= floor;
            radians *= 2M - radians;
            return radians * (-0.775M - 0.225M * radians) * ((floor & 2L) - 1L);
        }

        public static decimal Cos(decimal radians)
        {
            radians = radians * InverseHalfPi + decimal.One;
            long floor = (radians >= 0M ? (long)radians : (long)radians - 1L) & -2L;
            radians -= floor;
            radians *= 2M - radians;
            return radians * (-0.775M - 0.225M * radians) * ((floor & 2L) - 1L);
        }
        public static decimal Asin(decimal x)
        {
            decimal x2 = x * x;
            decimal x3 = x * x2;
            if (x >= decimal.Zero)
            {
                return HalfPi - Sqrt(decimal.One - x, 0.7M) *
                        (1.5707288M - 0.2121144M * x + 0.0742610M * x2 - 0.0187293M * x3);
            }
            else
            {
                return Sqrt(decimal.One + x, 0.7M) *
                    (1.5707288M + 0.2121144M * x + 0.0742610M * x2 + 0.0187293M * x3) - HalfPi;
            }
        }
        public static decimal Acos(decimal x)
        {
            decimal x2 = x * x;
            decimal x3 = x * x2;
            if (x >= decimal.Zero)
            {
                return Sqrt(decimal.One - x, 0.7M) * (1.5707288M - 0.2121144M * x + 0.0742610M * x2 - 0.0187293M * x3);
            }
            else
            {
                return Pi - Sqrt(decimal.One + x, 0.7M) * (1.5707288M + 0.2121144M * x + 0.0742610M * x2 + 0.0187293M * x3);
            }
        }

        public static decimal Atan(decimal i)
        {
            decimal n = Math.Abs(i);
            decimal c = (n - decimal.One) / (n + decimal.One);
            decimal c2 = c * c;
            decimal c3 = c * c2;
            decimal c5 = c3 * c2;
            decimal c7 = c5 * c2;
            return (0.7853981633974483M +
                    (0.999215M * c - 0.3211819M * c3 + 0.1462766M * c5 - 0.0389929M * c7)) * Math.Sign(i);
        }

        public static decimal Atan2(decimal y, decimal x)
        {
            decimal n = y / x;
            if (x > 0)
                return Atan(n);
            else if (x < decimal.Zero)
            {
                if (y >= decimal.Zero)
                    return Atan(n) + Pi;
                else
                    return Atan(n) - Pi;
            }
            else if (y > decimal.Zero) return x + HalfPi;
            else if (y < decimal.Zero) return x - HalfPi;
            else return decimal.Zero;
        }

    }
}
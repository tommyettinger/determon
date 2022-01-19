using System;

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
        /// The minimum value this permits as tolerance in equality comparisons.
        /// </summary>
        public const decimal Epsilon = 0.0000000000000000001M;

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
        /// Represents 0.0M .
        /// </summary>
        private const decimal Zero = decimal.Zero;

        /// <summary>
        /// Represents 1.0M .
        /// </summary>
        private const decimal One = decimal.One;

        /// <summary>
        /// Represents 0.5M .
        /// </summary>
        private const decimal Half = 0.5M;

        /// <summary>
        /// 1.0M divided by 3.0M. Exactly what it says.
        /// </summary>
        private const decimal Third = 1M / 3M;

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
            radians = radians * InverseHalfPi + 1M;
            long floor = (radians >= 0M ? (long)radians : (long)radians - 1L) & -2L;
            radians -= floor;
            radians *= 2M - radians;
            return radians * (-0.775M - 0.225M * radians) * ((floor & 2L) - 1L);
        }
    }
}

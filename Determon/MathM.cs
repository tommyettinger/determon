using System;

namespace Determon
{
    /// <summary>
    /// Deterministic/decimal math functions, as accurate as possible for decimals.
    /// </summary>
    /// <remarks>
    /// The vast majority of this class was written by Ramin Rahimzada in https://github.com/raminrahimzada/CSharp-Helper-Classes/tree/master/Math/DecimalMath .
    /// The Sqrt() methods are from a Stack Overflow answer by Bobson. The Remainder() method is common knowledge, but the specific code came from SquidLib, and
    /// likely traces back further to the Uncommon Math Library. Some minor cleanup was done by Tommy Ettinger, such as correcting inconsistent method naming.
    /// </remarks>
    public static class MathM
    {
        /// <summary>
        /// The irrational number pi, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal Pi = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214M;
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

        /// <summary>
        /// Max iteration count in Taylor series.
        /// </summary>
        private const int MaxIterations = 100;

        /// <summary>
        /// Analog to Math.Exp().
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Exp(decimal x)
        {
            var count = 0;

            if (x > One)
            {
                count = decimal.ToInt32(decimal.Truncate(x));
                x -= decimal.Truncate(x);
            }

            if (x < Zero)
            {
                count = decimal.ToInt32(decimal.Truncate(x) - 1);
                x = One + (x - decimal.Truncate(x));
            }

            var iteration = 1;
            var result = One;
            var factorial = One;
            decimal cachedResult;
            do
            {
                cachedResult = result;
                factorial *= x / iteration++;
                result += factorial;
            } while (cachedResult != result);

            if (count == 0)
                return result;
            return result * PowInt(E, count);
        }

        /// <summary>
        /// Analog to Math.Pow().
        /// </summary>
        /// <param name="value"></param>
        /// <param name="exponent"></param>
        /// <returns></returns>
        public static decimal Pow(decimal value, decimal exponent)
        {
            if (exponent == Zero) return One;
            if (exponent == One) return value;
            if (value == One) return One;

            if (value == Zero)
            {
                if (exponent > Zero)
                {
                    return Zero;
                }

                throw new Exception("Invalid Operation: base of zero with a negative power.");
            }

            if (exponent == -One) return One / value;

            var isPowerInteger = IsInteger(exponent);
            if (value < Zero && !isPowerInteger)
            {
                throw new Exception("Invalid Operation: negative base with a non-integer power.");
            }

            if (isPowerInteger && value > Zero)
            {
                int powerInt = (int)exponent;
                return PowInt(value, powerInt);
            }

            if (isPowerInteger && value < Zero)
            {
                int powerInt = (int)exponent;
                if (powerInt % 2 == 0)
                {
                    return Exp(exponent * Log(-value));
                }

                return -Exp(exponent * Log(-value));
            }

            return Exp(exponent * Log(value));
        }

        private static bool IsInteger(decimal value)
        {
            var longValue = (long)value;
            return Abs(value - longValue) <= Epsilon;
        }

        /// <summary>
        /// Raises a decimal value to an integer power and returns the result.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="exponent"></param>
        /// <returns></returns>
        public static decimal PowInt(decimal value, int exponent)
        {
            while (true)
            {
                if (exponent == Zero) return One;
                if (exponent < Zero)
                {
                    value = One / value;
                    exponent = -exponent;
                    continue;
                }

                var q = exponent;
                var prod = One;
                var current = value;
                while (q > 0)
                {
                    if (q % 2 == 1)
                    {
                        // detects the 1s in the binary expression of power
                        prod = current * prod; // picks up the relevant power
                        q--;
                    }

                    current *= current; // value^i -> value^(2*i)
                    q >>= 1;
                }

                return prod;
            }
        }

        /// <summary>
        /// Analog to Math.Log10().
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Log10(decimal x)
        {
            return Log(x) * InverseLog10;
        }

        /// <summary>
        /// Analog to Math.Log().
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Log(decimal x)
        {
            if (x <= Zero)
            {
                throw new ArgumentException("x must be greater than zero.");
            }
            var count = 0;
            while (x >= One)
            {
                x *= InverseE;
                count++;
            }
            while (x <= InverseE)
            {
                x *= E;
                count--;
            }
            x--;
            if (x == Zero) return count;
            var result = Zero;
            var iteration = 0;
            var y = One;
            var cacheResult = result - One;
            while (cacheResult != result && iteration < MaxIterations)
            {
                iteration++;
                cacheResult = result;
                y *= -x;
                result += y / iteration;
            }
            return count - result;
        }

        /// <summary>
        /// Analog to Math.Cos().
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Cos(decimal x)
        {
            //truncating to  [-2*PI;2*PI]
            TruncateToPeriodicInterval(ref x);

            // now x in (-2pi,2pi)
            if (x >= Pi && x <= Pi2)
            {
                return -Cos(x - Pi);
            }
            if (x >= -Pi2 && x <= -Pi)
            {
                return -Cos(x + Pi);
            }

            x *= x;
            //y=1-x/2!+x^2/4!-x^3/6!...
            var xx = -x * Half;
            var y = One + xx;
            var cachedY = y - One;//init cache  with different value
            for (var i = 1; cachedY != y && i < MaxIterations; i++)
            {
                cachedY = y;
                decimal factor = i * ((i << 1) + 3) + 1; //2i^2+2i+i+1=2i^2+3i+1
                factor = -Half / factor;
                xx *= x * factor;
                y += xx;
            }
            return y;
        }

        /// <summary>
        /// Analog to Math.Tan().
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Tan(decimal x)
        {
            var cos = Cos(x);
            if (cos == Zero) throw new ArgumentException(nameof(x));
            //calculate sin using cos
            var sin = CalculateSinFromCos(x, cos);
            return sin / cos;
        }
        /// <summary>
        /// Helper function for calculating sin(x) from cos(x).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="cos"></param>
        /// <returns></returns>
        private static decimal CalculateSinFromCos(decimal x, decimal cos)
        {
            var moduleOfSin = Sqrt(One - (cos * cos), Half);
            if (IsSignOfSinePositive(x)) return moduleOfSin;
            return -moduleOfSin;
        }
        /// <summary>
        /// Analog to Math.Sin().
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Sin(decimal x)
        {
            return Cos(x - HalfPi);
        }


        /// <summary>
        /// Truncates a decimal ref parameter to [-2*PI;2*PI].
        /// </summary>
        /// <param name="x"></param>
        private static void TruncateToPeriodicInterval(ref decimal x)
        {
            while (x >= Pi2)
            {
                var divide = Math.Abs(decimal.ToInt32(x / Pi2));
                x -= divide * Pi2;
            }

            while (x <= -Pi2)
            {
                var divide = Math.Abs(decimal.ToInt32(x / Pi2));
                x += divide * Pi2;
            }
        }


        private static bool IsSignOfSinePositive(decimal x)
        {
            //truncating to  [-2*PI;2*PI]
            TruncateToPeriodicInterval(ref x);

            //now x in [-2*PI;2*PI]
            return x <= -Pi || (x > Zero && x <= Pi);
        }
        /// <summary>
        /// A square root method for decimals.
        /// </summary>
        /// <param name="d">The square to find the square root of.</param>
        /// <returns>The exact square root of d, as well as a decimal can represent it.</returns>
        public static decimal Sqrt(decimal d)
        {
            return Sqrt(d, d * 0.5M);
        }

        /// <summary>
        /// A variant square root method for decimals that allows specifying an approximate guess, or seed value, that is likely to be close to the actual square root.
        /// </summary>
        /// <remarks>
        /// This has maximum precision. Credit to Bobson for coming up with this recursive method: https://stackoverflow.com/a/13282997/786740
        /// </remarks>
        /// <param name="d">The square to find the square root of.</param>
        /// <param name="guess">A decimal that is hopefully relatively close to the actual square root, used to speed up the algorithm if it is close.</param>
        /// <returns>The exact square root of d, as well as a decimal can represent it.</returns>
        public static decimal Sqrt(decimal d, decimal guess)
        {
            var result = d / guess;
            var average = (guess + result) * 0.5M;

            if (average == guess) // This checks for the maximum precision possible with a decimal.
                return average;
            else
                return Sqrt(d, average);
        }
        /// <summary>
        /// Gets the cube root of the given decimal, correctly returning negative results when negative inputs are given.
        /// </summary>
        /// <remarks>
        /// This uses <see cref="Pow(decimal, decimal)">Pow()</see> with an exponent of one third, and changes the sign for negative inputs before and after calling Pow().
        /// </remarks>
        /// <param name="d">The cube to find the cube root of.</param>
        /// <returns>The exact cube root of d, as well as a decimal can represent it.</returns>
        public static decimal Cbrt(decimal d)
        {
            return d < Zero ? -Pow(-d, Third) : Pow(d, Third);
        }

        /// <summary>
        /// Analog to Math.Sinh().
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Sinh(decimal x)
        {
            var y = Exp(x);
            var yy = One / y;
            return (y - yy) * Half;
        }

        /// <summary>
        /// Analog to Math.Cosh().
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Cosh(decimal x)
        {
            var y = Exp(x);
            var yy = One / y;
            return (y + yy) * Half;
        }

        /// <summary>
        /// Analog to Math.Tanh().
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Tanh(decimal x)
        {
            var y = Exp(x);
            var yy = One / y;
            return (y - yy) / (y + yy);
        }

        /// <summary>
        /// Analog to Math.Abs().
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Abs(decimal x)
        {
            if (x <= Zero)
            {
                return -x;
            }
            return x;
        }

        /// <summary>
        /// Analog to Math.Asin().
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Asin(decimal x)
        {
            if (x > One || x < -One)
            {
                throw new ArgumentException("x must be in [-1,1].");
            }
            //known values
            if (x == Zero) return Zero;
            if (x == One) return HalfPi;
            //asin function is odd function
            if (x < Zero) return -Asin(-x);

            //my optimize trick here

            // used a math formula to speed up :
            // asin(x)=0.5*(pi/2-asin(1-2*x*x)) 
            // if x>=0 is true

            var newX = One - 2 * x * x;

            //for calculating new value near to zero than current
            //because we gain more speed with values near to zero
            if (Abs(x) > Abs(newX))
            {
                var t = Asin(newX);
                return Half * (HalfPi - t);
            }
            var y = Zero;
            var result = x;
            decimal cachedResult;
            var i = 1;
            y += result;
            var xx = x * x;
            do
            {
                cachedResult = result;
                result *= xx * (One - Half / (i));
                y += result / ((i << 1) + 1);
                i++;
            } while (cachedResult != result);
            return y;
        }

        /// <summary>
        /// Analog to Math.Atan().
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Atan(decimal x)
        {
            if (x == Zero) return Zero;
            if (x == One) return QuarterPi;
            return Asin(x / Sqrt(One + x * x));
        }
        /// <summary>
        /// Analog to Math.Acos().
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal Acos(decimal x)
        {
            if (x == Zero) return HalfPi;
            if (x == One) return Zero;
            if (x < Zero) return Pi - Acos(-x);
            return HalfPi - Asin(x);
        }

        /// <summary>
        /// Analog to Math.Atan2(). Returns 0.0M if both arguments are zero.
        /// </summary>
        /// <param name="y">The y-coordinate of what is typically a 2D point. Note the order of parameters.</param>
        /// <param name="x">The x-coordinate of what is typically a 2D point.</param>
        /// <returns>An angle in radians between -Pi and Pi.</returns>
        public static decimal Atan2(decimal y, decimal x)
        {
            if (x > Zero)
            {
                return Atan(y / x);
            }
            if (x < Zero && y >= Zero)
            {
                return Atan(y / x) + Pi;
            }
            if (x < Zero && y < Zero)
            {
                return Atan(y / x) - Pi;
            }
            if (x == Zero && y > Zero)
            {
                return HalfPi;
            }
            if (x == Zero && y < Zero)
            {
                return -HalfPi;
            }
            if (x == Zero && y == Zero)
            {
                return Zero;
            }
            throw new ArgumentException("Invalid atan2() arguments, potentially including NaN.");
        }
        /// <summary>
        /// Analog to Math.Atan2(), but will return values in the Pi to Pi * 2 range instead of returning negative results for negative y.
        /// Returns 0.0M if both arguments are zero.
        /// </summary>
        /// <param name="y">The y-coordinate of what is typically a 2D point. Note the order of parameters.</param>
        /// <param name="x">The x-coordinate of what is typically a 2D point.</param>
        /// <returns>An angle in radians between 0 and Pi2.</returns>
        public static decimal Atan2NonNegative(decimal y, decimal x)
        {
            if (x > Zero)
            {
                return Atan(y / x) + (y >= Zero ? Zero : Pi2);
            }
            if (x < Zero)
            {
                return Atan(y / x) + Pi;
            }
            if (x == Zero && y > Zero)
            {
                return HalfPi;
            }
            if (x == Zero && y < Zero)
            {
                return Pi + HalfPi;
            }
            if (x == Zero && y == Zero)
            {
                return Zero;
            }
            throw new ArgumentException("Invalid atan2() arguments, potentially including NaN.");
        }
        /// <summary>
        /// Like the modulo operator %, but the result will always match the sign of d instead of op.
        /// </summary>
        /// <param name="op">The dividend; negative values are permitted and wrap instead of producing negative results.</param>
        /// <param name="d">The divisor; if this is negative then the result will be negative, otherwise it will be positive.</param>
        /// <returns>The remainder of the division of op by d, with a sign matching d.</returns>
        public static decimal Remainder(decimal op, decimal d)
        {
            return (op % d + d) % d;
        }

    }
}

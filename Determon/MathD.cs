using System;

namespace Determon
{
    /// <summary>
    /// Deterministic/decimal math functions, most of them approximations.
    /// </summary>
    public class MathD
    {
        /// <summary>
        /// The irrational number pi, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal PI = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214M;
        /// <summary>
        /// The irrational number pi divided by 2, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal HALF_PI = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214M * 0.5M;
        /// <summary>
        /// The irrational number pi times 2, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal PI2 = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899862803482534211706798214M * 2M;
        /// <summary>
        /// The irrational number e, Euler's totient, to 104 digits as a decimal. It is likely that far fewer digits will actually be used.
        /// </summary>
        public const decimal E = 2.71828182845904523536028747135266249775724709369995957496696762772407663035354759457138217852516642742746M;

        /// <summary>
        /// 
        /// </summary>
        public const decimal PHI = 1.61803398874989484820458683436563811772030917980576286213544862270526046281890244970720720418939113748475M;
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

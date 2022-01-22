# determon
Deterministic math functions in C# using decimals. Gotta match em all.

## Why?

There's been some concern for a while about floating-point determinism in C#. This would be where the exact floating-point result of
calculations on `float` or `double` types can be different, depending on the platform that runs the program, its JIT compilation
settings, and so on. Visual Studio offers a checkbox for "Deterministic," which may be enough, but just in case it isn't...

## This Library.

Determon was mostly created to help support replays in games with procedural generation; in these cases, absolute mathematical
accuracy is less important than absolute reliability across all machines that can run the application. The MathM class provides
both of these; the ApproxM class only provides the second. The ApproxM class seems to be quite a bit faster than MathM, but both
are rather slow compared to Math and MathF. This is because all the math here operates on `decimal` numbers, rather than `float`
or `double`; this gives us determinism at the expense of speed.

## License

Determon is licensed under MIT; see the file LICENSE for more. The MathM class is based primarily on
Ramin Rahimzada's [DecimalMath](https://github.com/raminrahimzada/CSharp-Helper-Classes/tree/master/Math/DecimalMath) library,
in a larger repo of C# helper classes. The approximation code is not from the same repo, and draws from a fairly wide range of
approximations. The 1955 research study by RAND Corp, "Approximations for Digital Computers," was very useful for the inverse
trigonometric function approximations (it uses Taylor series, but these seem to be good enough in this case). Some other
approximations were published on the web in various articles. The Sqrt() methods in both the precise and approximate math are
from Stack Overflow, answering [this question](https://stackoverflow.com/questions/4124189/performing-math-operations-on-decimal-datatype-in-c/13282997#13282997).
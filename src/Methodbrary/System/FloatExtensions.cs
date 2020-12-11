using System;

namespace Methodbrary.System
{
    public static class FloatExtensions
    {
        public static bool IsNear(this float value, float compare, float tolerance = 0.0001f)
            => Math.Abs(value - compare) < tolerance;

        public static float Sign(this float value) => Math.Sign(value);
        public static float Abs(this float value) => Math.Abs(value);
    }
}
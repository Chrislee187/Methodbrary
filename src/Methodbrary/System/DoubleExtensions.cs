using System;

namespace Methodbrary.System
{
    public static class DoubleExtensions
    {
        public const double Deg2Rad = 0.0174533d;
        public static double Sin(this double value) => Math.Sin(value);
        public static double Cos(this double value) => Math.Cos(value);
        public static double Tan(this double value) => Math.Tan(value);
        public static double ATan(this double value) => Math.Atan(value);
        public static double ATan2(this double value, float x) => Math.Atan2(value, x);

        public static double SinD(this double value) => Math.Sin(value * Deg2Rad);
        public static double CosD(this double value) => Math.Cos(value * Deg2Rad);
        public static double TanD(this double value) => Math.Tan(value * Deg2Rad);
        public static double ATanD(this double value) => Math.Atan(value * Deg2Rad);
        public static double ATan2D(this double value, float x) => Math.Atan2(value * Deg2Rad, x);
    }
}
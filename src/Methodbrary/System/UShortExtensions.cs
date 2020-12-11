using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Methodbrary.System
{
    public static class UShortExtensions
    {
        public static ushort SwapBytes(this ushort value)
            => (ushort)((value >> 8) | ((value & 0xFF) << 8));

        public static byte[] ToByteArray(this ushort value)
            => new byte[2]
            {
                (byte) (value >> 8),
                (byte) value
            };

    }


    public static class FloatExtensions
    {
        public static bool IsNear(this float value, float compare, float tolerance = 0.0001f)
            => Math.Abs(value - compare) < tolerance;

        public static float Sign(this float value) => Math.Sign(value);
        public static float Abs(this float value) => Math.Abs(value);
    }

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

    public static class Vector3Extensions
    {
        public static Vector3 ChangeX(this Vector3 vertex, float x) => new Vector3(x, vertex.Y, vertex.Z);
        public static Vector3 ChangeY(this Vector3 vertex, float y) => new Vector3(vertex.X, y, vertex.Z);
        public static Vector3 ChangeZ(this Vector3 vertex, float z) => new Vector3(vertex.X, vertex.Y, z);

        public static Vector3 AddX(this Vector3 vertex, float x) => new Vector3(vertex.X + x, vertex.Y, vertex.Z);
        public static Vector3 AddY(this Vector3 vertex, float y) => new Vector3(vertex.X, vertex.Y + y, vertex.Z);
        public static Vector3 AddZ(this Vector3 vertex, float z) => new Vector3(vertex.X, vertex.Y, vertex.Z + z);

        public static Vector3 Abs(this Vector3 vertex) => new Vector3(vertex.X.Abs(), vertex.Y.Abs(), vertex.Z.Abs());
        public static Vector3 Sign(this Vector3 vertex) => new Vector3(vertex.X.Sign(), vertex.Y.Sign(), vertex.Z.Sign());


    }
}
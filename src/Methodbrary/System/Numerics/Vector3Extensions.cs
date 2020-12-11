using System.Numerics;

namespace Methodbrary.System.Numerics
{
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
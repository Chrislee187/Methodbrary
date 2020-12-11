namespace Methodbrary.System
{
    public static class BoolExtensions
    {
        public static byte ToOneOrZero(this bool value) => value ? (byte)1 : (byte)0;
    }
}
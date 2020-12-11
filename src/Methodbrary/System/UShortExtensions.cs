namespace Methodbrary.System
{
    public static class UShortExtensions
    {
        public static ushort SwapBytes(this ushort value)
            => (ushort)((value >> 8) | ((value & 0xFF) << 8));

        public static byte[] ToByteArray(this ushort value)
            => new[]
            {
                (byte) (value >> 8),
                (byte) value
            };

    }
}
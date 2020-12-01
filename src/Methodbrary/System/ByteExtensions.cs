using System;
using System.Linq;
using System.Text;

namespace Methodbrary.System
{
    public static class ByteExtensions
    {
        public static string ToBlockCopyString(this byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        public static string ToCharCastString(this byte[] bytes)
        {
            return new string(bytes.Select(b => (char)b).ToArray());
        }

        public static string ToHex(this byte[] bytes)
        {
            return string.Join(" ", bytes.Select(b => b.ToString("X2")));
        }

        public static string ToUtf8String(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static string ToUtf8(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static int ToInt(this byte[] bytes, bool bigendian = false)
        {
            // Are these the right way round lol?
            return bigendian ? bytes[0] * 256 + bytes[1] : bytes[1] * 256 + bytes[0];
        }
    }
}
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

        public static ushort GetUShort(this Span<byte> bytes)
            => (ushort)(bytes[0] << 8 | bytes[1]);

        public static void SetUShort(this Span<byte> buffer, ushort value)
        {
            buffer[0] = (byte)(value >> 8);
            buffer[1] = (byte)(value >> 0);
        }
        
        public static void SetUInt(this Span<byte> buffer, uint value)
        {
            buffer[0] = (byte)(value >> 24);
            buffer[1] = (byte)(value >> 16);
            buffer[2] = (byte)(value >> 8);
            buffer[3] = (byte)(value >> 0);
        }
        
        public static uint GetUInt(this Span<byte> bytes)
            => (uint)(bytes[0] << 24 | bytes[1] << 16 | bytes[2] << 8 | bytes[3]);

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Methodbrary.System.IO
{
    public static class BinaryReaderExtensions
    {
        public static byte[] ReadUntil(this BinaryReader reader, Func<byte, bool> until)
        {
            var b = reader.ReadByte();
            var result = new List<byte>();
            while (!until(b))
            {
                result.Add(b);
                b = reader.ReadByte();
            }

            return result.ToArray();
        }

        public static string ReadTerminatedString(this BinaryReader reader, byte terminate)
        {
            return new string(reader.ReadUntil(b => b == terminate).Select(Convert.ToChar).ToArray());
        }
    }
}
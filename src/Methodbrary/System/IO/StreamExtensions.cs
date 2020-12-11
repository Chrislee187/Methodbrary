using System.IO;

namespace Methodbrary.System.IO
{
    public static class StreamExtensions
    {
        public static byte PeekByte(this Stream stream)
        {
            if (stream.Position == -1) throw new EndOfStreamException();

            var @byte = stream.ReadByte();

            if (stream.Position == -1)
                stream.Position = stream.Length;
            else
            {
                stream.Position -= 1;
            }

            return (byte)@byte;
        }

        public static byte[] ToByteArray(this Stream stream)
        {
            var buffer = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(buffer, 0, (int)stream.Length);
            return buffer;
        }
    }

}
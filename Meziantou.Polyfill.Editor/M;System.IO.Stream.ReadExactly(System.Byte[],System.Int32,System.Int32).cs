using System.IO;

static partial class PolyfillExtensions
{
    public static void ReadExactly(this Stream target, byte[] buffer, int offset, int count)
    {
        int totalRead = 0;
        while (totalRead < count)
        {
            int read = target.Read(buffer, offset + totalRead, count - totalRead);
            if (read == 0)
                throw new EndOfStreamException("Unable to read beyond the end of the stream.");

            totalRead += read;
        }
    }
}
using System.Text;

static partial class PolyfillExtensions
{
    extension(Encoding)
    {
        public static Encoding Latin1 => Encoding.GetEncoding(28591);
    }
}

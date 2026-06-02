static partial class PolyfillExtensions
{
    extension(System.Net.Mime.MediaTypeNames.Application)
    {
        public static string GZip => "application/gzip";
    }
}
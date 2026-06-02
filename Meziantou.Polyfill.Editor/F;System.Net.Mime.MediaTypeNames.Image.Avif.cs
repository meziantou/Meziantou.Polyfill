static partial class PolyfillExtensions
{
    extension(System.Net.Mime.MediaTypeNames.Image)
    {
        public static string Avif => "image/avif";
    }
}
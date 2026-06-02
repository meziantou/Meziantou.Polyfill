static partial class PolyfillExtensions
{
    extension(System.Net.Mime.MediaTypeNames.Image)
    {
        public static string Svg => "image/svg+xml";
    }
}
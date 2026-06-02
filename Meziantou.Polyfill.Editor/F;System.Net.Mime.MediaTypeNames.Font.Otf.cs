// define-type System.Net.Mime.MediaTypeNames.Font
static partial class PolyfillExtensions
{
#if MEZIANTOU_POLYFILL_TYPE_SYSTEM_NET_MIME_MEDIATYPENAMES_FONT
    extension(System.Net.Mime.MediaTypeNames.Font)
    {
        public static string Otf => "font/otf";
    }
#else
    extension(MediaTypeNamesFontPolyfill instance)
    {
        public string Otf => "font/otf";
    }
#endif
}
// define-type System.Net.Mime.MediaTypeNames.Font
static partial class PolyfillExtensions
{
#if MEZIANTOU_POLYFILL_TYPE_SYSTEM_NET_MIME_MEDIATYPENAMES_FONT
    extension(System.Net.Mime.MediaTypeNames.Font)
    {
        public static string Collection => "font/collection";
    }
#else
    extension(MediaTypeNamesFontPolyfill instance)
    {
        public string Collection => "font/collection";
    }
#endif
}
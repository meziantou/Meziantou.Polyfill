// define-type System.Net.Mime.MediaTypeNames.Multipart
static partial class PolyfillExtensions
{
#if MEZIANTOU_POLYFILL_TYPE_SYSTEM_NET_MIME_MEDIATYPENAMES_MULTIPART
    extension(System.Net.Mime.MediaTypeNames.Multipart)
    {
        public static string Mixed => "multipart/mixed";
    }
#else
    extension(MediaTypeNamesMultipartPolyfill instance)
    {
        public string Mixed => "multipart/mixed";
    }
#endif
}
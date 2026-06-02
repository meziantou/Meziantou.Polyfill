// define-type System.Net.Mime.MediaTypeNames.Multipart
static partial class PolyfillExtensions
{
#if MEZIANTOU_POLYFILL_TYPE_SYSTEM_NET_MIME_MEDIATYPENAMES_MULTIPART
    extension(System.Net.Mime.MediaTypeNames.Multipart)
    {
        public static string Related => "multipart/related";
    }
#else
    extension(MediaTypeNamesMultipartPolyfill instance)
    {
        public string Related => "multipart/related";
    }
#endif
}
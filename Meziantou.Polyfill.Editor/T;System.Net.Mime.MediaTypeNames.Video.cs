namespace System.Net.Mime
{
    internal static class MediaTypeNames
    {
        public static class Application
        {
            public const string Octet = "application/octet-stream";
            public const string Pdf = "application/pdf";
            public const string Rtf = "application/rtf";
            public const string Soap = "application/soap+xml";
            public const string Zip = "application/zip";

            public const string Json = "application/json";
            public const string Xml = "application/xml";

            public const string FormUrlEncoded = "application/x-www-form-urlencoded";
            public const string JsonPatch = "application/json-patch+json";
            public const string JsonSequence = "application/json-seq";
            public const string Manifest = "application/manifest+json";
            public const string ProblemJson = "application/problem+json";
            public const string ProblemXml = "application/problem+xml";
            public const string Wasm = "application/wasm";
            public const string XmlDtd = "application/xml-dtd";
            public const string XmlPatch = "application/xml-patch+xml";

            public const string GZip = "application/gzip";

            public const string Yaml = "application/yaml";
        }

        public static class Font
        {
            public const string Collection = "font/collection";
            public const string Otf = "font/otf";
            public const string Sfnt = "font/sfnt";
            public const string Ttf = "font/ttf";
            public const string Woff = "font/woff";
            public const string Woff2 = "font/woff2";
        }

        public static class Image
        {
            public const string Gif = "image/gif";
            public const string Jpeg = "image/jpeg";
            public const string Tiff = "image/tiff";

            public const string Avif = "image/avif";
            public const string Bmp = "image/bmp";
            public const string Icon = "image/x-icon";
            public const string Png = "image/png";
            public const string Svg = "image/svg+xml";
            public const string Webp = "image/webp";
        }

        public static class Multipart
        {
            public const string ByteRanges = "multipart/byteranges";
            public const string FormData = "multipart/form-data";

            public const string Mixed = "multipart/mixed";
            public const string Related = "multipart/related";
        }

        public static class Text
        {
            public const string Html = "text/html";
            public const string Plain = "text/plain";
            public const string RichText = "text/richtext";
            public const string Xml = "text/xml";

            public const string Css = "text/css";
            public const string Csv = "text/csv";
            public const string JavaScript = "text/javascript";
            public const string Markdown = "text/markdown";
            public const string Rtf = "text/rtf";

            public const string EventStream = "text/event-stream";
        }

        public static class Video
        {
            public const string Mp4 = "video/mp4";
            public const string Mpeg = "video/mpeg";
            public const string Ogg = "video/ogg";
            public const string QuickTime = "video/quicktime";
            public const string WebM = "video/webm";
        }
    }
}

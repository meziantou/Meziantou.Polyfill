static partial class PolyfillExtensions
{
    extension(System.Net.Mime.MediaTypeNames.Text)
    {
        public static string Csv => "text/csv";
    }
}
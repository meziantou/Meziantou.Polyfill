static partial class PolyfillExtensions
{
    extension(System.Net.Mime.MediaTypeNames.Application)
    {
        public static string FormUrlEncoded => "application/x-www-form-urlencoded";
    }
}
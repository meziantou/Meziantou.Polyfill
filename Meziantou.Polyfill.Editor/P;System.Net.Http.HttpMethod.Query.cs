using System.Net.Http;

static partial class PolyfillExtensions
{
    extension(System.Net.Http.HttpMethod)
    {
        public static HttpMethod Query
        {
            get => HttpMethodQuery.Instance;
        }
    }
}

file static class HttpMethodQuery
{
    public static readonly HttpMethod Instance = new("QUERY");
}

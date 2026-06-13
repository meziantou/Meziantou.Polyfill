using System.Net.Mime;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class SystemNetMimeTests
{
    [Fact]
    public void MediaTypeNames_Members()
    {
        // Application
        Assert.Equal("application/json", MediaTypeNames.Application.Json);
        Assert.Equal("application/xml", MediaTypeNames.Application.Xml);
        Assert.Equal("application/x-www-form-urlencoded", MediaTypeNames.Application.FormUrlEncoded);
        Assert.Equal("application/json-patch+json", MediaTypeNames.Application.JsonPatch);
        Assert.Equal("application/json-seq", MediaTypeNames.Application.JsonSequence);
        Assert.Equal("application/manifest+json", MediaTypeNames.Application.Manifest);
        Assert.Equal("application/problem+json", MediaTypeNames.Application.ProblemJson);
        Assert.Equal("application/problem+xml", MediaTypeNames.Application.ProblemXml);
        Assert.Equal("application/wasm", MediaTypeNames.Application.Wasm);
        Assert.Equal("application/xml-dtd", MediaTypeNames.Application.XmlDtd);
        Assert.Equal("application/xml-patch+xml", MediaTypeNames.Application.XmlPatch);
        Assert.Equal("application/gzip", MediaTypeNames.Application.GZip);
        Assert.Equal("application/yaml", MediaTypeNames.Application.Yaml);

        // Font
        Assert.Equal("font/collection", MediaTypeNames.Font.Collection);
        Assert.Equal("font/otf", MediaTypeNames.Font.Otf);
        Assert.Equal("font/sfnt", MediaTypeNames.Font.Sfnt);
        Assert.Equal("font/ttf", MediaTypeNames.Font.Ttf);
        Assert.Equal("font/woff", MediaTypeNames.Font.Woff);
        Assert.Equal("font/woff2", MediaTypeNames.Font.Woff2);

        // Image
        Assert.Equal("image/avif", MediaTypeNames.Image.Avif);
        Assert.Equal("image/bmp", MediaTypeNames.Image.Bmp);
        Assert.Equal("image/x-icon", MediaTypeNames.Image.Icon);
        Assert.Equal("image/png", MediaTypeNames.Image.Png);
        Assert.Equal("image/svg+xml", MediaTypeNames.Image.Svg);
        Assert.Equal("image/webp", MediaTypeNames.Image.Webp);

        // Multipart
        Assert.Equal("multipart/byteranges", MediaTypeNames.Multipart.ByteRanges);
        Assert.Equal("multipart/form-data", MediaTypeNames.Multipart.FormData);
        Assert.Equal("multipart/mixed", MediaTypeNames.Multipart.Mixed);
        Assert.Equal("multipart/related", MediaTypeNames.Multipart.Related);

        // Text
        Assert.Equal("text/css", MediaTypeNames.Text.Css);
        Assert.Equal("text/csv", MediaTypeNames.Text.Csv);
        Assert.Equal("text/javascript", MediaTypeNames.Text.JavaScript);
        Assert.Equal("text/markdown", MediaTypeNames.Text.Markdown);
        Assert.Equal("text/rtf", MediaTypeNames.Text.Rtf);
        Assert.Equal("text/event-stream", MediaTypeNames.Text.EventStream);

        // Video
        Assert.Equal("video/mp4", MediaTypeNames.Video.Mp4);
        Assert.Equal("video/mpeg", MediaTypeNames.Video.Mpeg);
        Assert.Equal("video/ogg", MediaTypeNames.Video.Ogg);
        Assert.Equal("video/quicktime", MediaTypeNames.Video.QuickTime);
        Assert.Equal("video/webm", MediaTypeNames.Video.WebM);
    }
}

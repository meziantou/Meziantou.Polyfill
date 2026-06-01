using System.Net.Mime;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public sealed class SystemNetMimeTests
{
    [Fact]
    public void MediaTypeNames_Application()
    {
        Assert.Equal("application/octet-stream", MediaTypeNames.Application.Octet);
        Assert.Equal("application/pdf", MediaTypeNames.Application.Pdf);
        Assert.Equal("application/rtf", MediaTypeNames.Application.Rtf);
        Assert.Equal("application/soap+xml", MediaTypeNames.Application.Soap);
        Assert.Equal("application/zip", MediaTypeNames.Application.Zip);
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
    }

    [Fact]
    public void MediaTypeNames_Font()
    {
        Assert.Equal("font/collection", MediaTypeNames.Font.Collection);
        Assert.Equal("font/otf", MediaTypeNames.Font.Otf);
        Assert.Equal("font/sfnt", MediaTypeNames.Font.Sfnt);
        Assert.Equal("font/ttf", MediaTypeNames.Font.Ttf);
        Assert.Equal("font/woff", MediaTypeNames.Font.Woff);
        Assert.Equal("font/woff2", MediaTypeNames.Font.Woff2);
    }

    [Fact]
    public void MediaTypeNames_Image()
    {
        Assert.Equal("image/gif", MediaTypeNames.Image.Gif);
        Assert.Equal("image/jpeg", MediaTypeNames.Image.Jpeg);
        Assert.Equal("image/tiff", MediaTypeNames.Image.Tiff);
        Assert.Equal("image/avif", MediaTypeNames.Image.Avif);
        Assert.Equal("image/bmp", MediaTypeNames.Image.Bmp);
        Assert.Equal("image/x-icon", MediaTypeNames.Image.Icon);
        Assert.Equal("image/png", MediaTypeNames.Image.Png);
        Assert.Equal("image/svg+xml", MediaTypeNames.Image.Svg);
        Assert.Equal("image/webp", MediaTypeNames.Image.Webp);
    }

    [Fact]
    public void MediaTypeNames_Multipart()
    {
        Assert.Equal("multipart/byteranges", MediaTypeNames.Multipart.ByteRanges);
        Assert.Equal("multipart/form-data", MediaTypeNames.Multipart.FormData);
        Assert.Equal("multipart/mixed", MediaTypeNames.Multipart.Mixed);
        Assert.Equal("multipart/related", MediaTypeNames.Multipart.Related);
    }

    [Fact]
    public void MediaTypeNames_Text()
    {
        Assert.Equal("text/html", MediaTypeNames.Text.Html);
        Assert.Equal("text/plain", MediaTypeNames.Text.Plain);
        Assert.Equal("text/richtext", MediaTypeNames.Text.RichText);
        Assert.Equal("text/xml", MediaTypeNames.Text.Xml);
        Assert.Equal("text/css", MediaTypeNames.Text.Css);
        Assert.Equal("text/csv", MediaTypeNames.Text.Csv);
        Assert.Equal("text/javascript", MediaTypeNames.Text.JavaScript);
        Assert.Equal("text/markdown", MediaTypeNames.Text.Markdown);
        Assert.Equal("text/rtf", MediaTypeNames.Text.Rtf);
        Assert.Equal("text/event-stream", MediaTypeNames.Text.EventStream);
    }

    [Fact]
    public void MediaTypeNames_Video()
    {
        Assert.Equal("video/mp4", MediaTypeNames.Video.Mp4);
        Assert.Equal("video/mpeg", MediaTypeNames.Video.Mpeg);
        Assert.Equal("video/ogg", MediaTypeNames.Video.Ogg);
        Assert.Equal("video/quicktime", MediaTypeNames.Video.QuickTime);
        Assert.Equal("video/webm", MediaTypeNames.Video.WebM);
    }
}

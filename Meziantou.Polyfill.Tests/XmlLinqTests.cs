using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class XmlLinqTests
{
    private const string SampleXml = @"<?xml version=""1.0"" encoding=""utf-8""?><root><item id=""1"">Value1</item><item id=""2"">Value2</item></root>";

    [Fact]
    public async Task XDocument_SaveAsync_Stream()
    {
        var document = XDocument.Parse(SampleXml);
        using var stream = new MemoryStream();

        await document.SaveAsync(stream, SaveOptions.None, CancellationToken.None);

        stream.Position = 0;
        var result = await new StreamReader(stream).ReadToEndAsync();
        Assert.Contains("<root>", result);
        Assert.Contains("<item", result);
    }

    [Fact]
    public async Task XDocument_SaveAsync_TextWriter()
    {
        var document = XDocument.Parse(SampleXml);
        using var writer = new StringWriter();

        await document.SaveAsync(writer, SaveOptions.None, CancellationToken.None);

        var result = writer.ToString();
        Assert.Contains("<root>", result);
        Assert.Contains("<item", result);
    }

    [Fact]
    public async Task XDocument_SaveAsync_XmlWriter()
    {
        var document = XDocument.Parse(SampleXml);
        using var stringWriter = new StringWriter();
        using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Async = true }))
        {
            await document.SaveAsync(xmlWriter, CancellationToken.None);
        }

        var result = stringWriter.ToString();
        Assert.Contains("<root>", result);
        Assert.Contains("<item", result);
    }

    [Fact]
    public async Task XDocument_LoadAsync_Stream()
    {
        using var stream = new MemoryStream();
        using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen: true))
        {
            await writer.WriteAsync(SampleXml);
        }
        stream.Position = 0;

        var document = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);

        Assert.NotNull(document.Root);
        Assert.Equal("root", document.Root.Name.LocalName);
    }

    [Fact]
    public async Task XDocument_LoadAsync_TextReader()
    {
        using var reader = new StringReader(SampleXml);

        var document = await XDocument.LoadAsync(reader, LoadOptions.None, CancellationToken.None);

        Assert.NotNull(document.Root);
        Assert.Equal("root", document.Root.Name.LocalName);
    }

    [Fact]
    public async Task XDocument_LoadAsync_XmlReader()
    {
        using var stringReader = new StringReader(SampleXml);
        using var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings { Async = true });

        var document = await XDocument.LoadAsync(xmlReader, LoadOptions.None, CancellationToken.None);

        Assert.NotNull(document.Root);
        Assert.Equal("root", document.Root.Name.LocalName);
    }

    [Fact]
    public async Task XElement_SaveAsync_Stream()
    {
        var element = XElement.Parse("<root><item>Value</item></root>");
        using var stream = new MemoryStream();

        await element.SaveAsync(stream, SaveOptions.None, CancellationToken.None);

        stream.Position = 0;
        var result = await new StreamReader(stream).ReadToEndAsync();
        Assert.Contains("<root>", result);
        Assert.Contains("<item>", result);
    }

    [Fact]
    public async Task XElement_SaveAsync_TextWriter()
    {
        var element = XElement.Parse("<root><item>Value</item></root>");
        using var writer = new StringWriter();

        await element.SaveAsync(writer, SaveOptions.None, CancellationToken.None);

        var result = writer.ToString();
        Assert.Contains("<root>", result);
        Assert.Contains("<item>", result);
    }

    [Fact]
    public async Task XElement_SaveAsync_XmlWriter()
    {
        var element = XElement.Parse("<root><item>Value</item></root>");
        using var stringWriter = new StringWriter();
        using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Async = true }))
        {
            await element.SaveAsync(xmlWriter, CancellationToken.None);
        }

        var result = stringWriter.ToString();
        Assert.Contains("<root>", result);
        Assert.Contains("<item>", result);
    }

    [Fact]
    public async Task XElement_LoadAsync_Stream()
    {
        var xml = "<root><item>Value</item></root>";
        using var stream = new MemoryStream();
        using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen: true))
        {
            await writer.WriteAsync(xml);
        }
        stream.Position = 0;

        var element = await XElement.LoadAsync(stream, LoadOptions.None, CancellationToken.None);

        Assert.Equal("root", element.Name.LocalName);
        Assert.NotNull(element.Element("item"));
    }

    [Fact]
    public async Task XElement_LoadAsync_TextReader()
    {
        var xml = "<root><item>Value</item></root>";
        using var reader = new StringReader(xml);

        var element = await XElement.LoadAsync(reader, LoadOptions.None, CancellationToken.None);

        Assert.Equal("root", element.Name.LocalName);
        Assert.NotNull(element.Element("item"));
    }

    [Fact]
    public async Task XElement_LoadAsync_XmlReader()
    {
        var xml = "<root><item>Value</item></root>";
        using var stringReader = new StringReader(xml);
        using var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings { Async = true });

        var element = await XElement.LoadAsync(xmlReader, LoadOptions.None, CancellationToken.None);

        Assert.Equal("root", element.Name.LocalName);
        Assert.NotNull(element.Element("item"));
    }
}

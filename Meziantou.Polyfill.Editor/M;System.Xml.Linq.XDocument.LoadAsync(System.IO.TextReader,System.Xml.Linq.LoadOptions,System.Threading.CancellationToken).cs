#pragma warning disable CA1510 // Use ArgumentNullException throw helper

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml.Linq;

static partial class PolyfillExtensions_XDocument
{
    /// <summary>
    /// Asynchronously loads an <see cref="XDocument"/> from a <see cref="TextReader"/>.
    /// </summary>
    /// <param name="textReader">The text reader containing the XML data.</param>
    /// <param name="options">A set of options that control how to load the document.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous load operation and returns the loaded document.</returns>
    extension(XDocument)
    {
        public static
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        async
#endif
        Task<XDocument> LoadAsync(TextReader textReader, LoadOptions options, CancellationToken cancellationToken)
        {
            if (textReader is null)
                throw new ArgumentNullException(nameof(textReader));

            cancellationToken.ThrowIfCancellationRequested();
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            var settings = new XmlReaderSettings
            {
                Async = true,
            };

            using var reader = XmlReader.Create(textReader, settings);
            return await XDocument.LoadAsync(reader, options, cancellationToken).ConfigureAwait(false);
#else
            return Task.FromResult(XDocument.Load(textReader, options));
#endif
        }
    }
}

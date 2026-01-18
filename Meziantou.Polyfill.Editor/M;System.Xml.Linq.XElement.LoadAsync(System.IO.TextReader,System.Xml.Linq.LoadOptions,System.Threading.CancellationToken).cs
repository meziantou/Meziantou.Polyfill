#pragma warning disable CA1510 // Use ArgumentNullException throw helper

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml.Linq;

static partial class PolyfillExtensions_XElement
{
    /// <summary>
    /// Asynchronously loads an <see cref="XElement"/> from a <see cref="TextReader"/>.
    /// </summary>
    /// <param name="textReader">The text reader containing the XML data.</param>
    /// <param name="options">A set of options that control how to load the element.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous load operation and returns the loaded element.</returns>
    extension(XElement)
    {
        public static
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        async
#endif
        Task<XElement> LoadAsync(TextReader textReader, LoadOptions options, CancellationToken cancellationToken)
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
            return await XElement.LoadAsync(reader, options, cancellationToken).ConfigureAwait(false);
#else
            return Task.FromResult(XElement.Load(textReader, options));
#endif
        }
    }
}

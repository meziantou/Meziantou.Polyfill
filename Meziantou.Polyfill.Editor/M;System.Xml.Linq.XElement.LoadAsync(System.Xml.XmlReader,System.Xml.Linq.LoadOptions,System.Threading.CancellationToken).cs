#pragma warning disable CA1510 // Use ArgumentNullException throw helper

using System.Threading;
using System.Threading.Tasks;

namespace System.Xml.Linq;

static partial class PolyfillExtensions_XElement
{
    /// <summary>
    /// Asynchronously loads an <see cref="XElement"/> from an <see cref="XmlReader"/>.
    /// </summary>
    /// <param name="reader">The XML reader containing the XML data.</param>
    /// <param name="options">A set of options that control how to load the element.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous load operation and returns the loaded element.</returns>
    extension(XElement)
    {
        public static
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        async
#endif
        Task<XElement> LoadAsync(XmlReader reader, LoadOptions options, CancellationToken cancellationToken)
        {
            if (reader is null)
                throw new ArgumentNullException(nameof(reader));

            cancellationToken.ThrowIfCancellationRequested();
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return (XElement)await XNode.ReadFromAsync(reader, cancellationToken).ConfigureAwait(false);
#else
            return Task.FromResult(XElement.Load(reader, options));
#endif
        }
    }
}

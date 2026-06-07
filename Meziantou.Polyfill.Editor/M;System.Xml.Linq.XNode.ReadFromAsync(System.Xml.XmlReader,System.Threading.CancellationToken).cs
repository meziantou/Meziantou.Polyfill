#pragma warning disable CA1510 // Use ArgumentNullException throw helper

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

static partial class PolyfillExtensions_XNode
{
    /// <summary>
    /// Creates an <see cref="XNode"/> from an <see cref="XmlReader"/> asynchronously.
    /// </summary>
    /// <param name="reader">The XML reader positioned at the node to read.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous read operation and returns the loaded node.</returns>
    extension(XNode)
    {
        public static Task<XNode> ReadFromAsync(XmlReader reader, CancellationToken cancellationToken)
        {
            if (reader is null)
                throw new ArgumentNullException(nameof(reader));

            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(XNode.ReadFrom(reader));
        }
    }
}

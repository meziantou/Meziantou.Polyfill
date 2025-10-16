using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    extension(File)
    {
        public static Task WriteAllTextAsync(string path, ReadOnlyMemory<char> contents, CancellationToken cancellationToken = default)
             => WriteAllTextAsync(path, contents, Helpers.UTF8NoBOM, cancellationToken);
    }
}

file static class Helpers
{
    public static readonly System.Text.Encoding UTF8NoBOM = new System.Text.UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
}
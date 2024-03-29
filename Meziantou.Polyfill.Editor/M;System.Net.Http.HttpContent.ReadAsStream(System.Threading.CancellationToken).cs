﻿using System.IO;
using System.Net.Http;
using System.Threading;

static partial class PolyfillExtensions
{
    public static Stream ReadAsStream(this HttpContent httpContent, CancellationToken cancellationToken)
    {
        var ms = new MemoryStream();
        httpContent.CopyTo(ms, context: null, cancellationToken);
        ms.Seek(0, SeekOrigin.Begin);
        return ms;
    }
}
﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

static partial class PolyfillExtensions
{
    public static async Task CopyToAsync(this HttpContent target, Stream stream, TransportContext? context, CancellationToken cancellationToken)
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

        var method = typeof(HttpContent).GetMethod("SerializeToStreamAsync", 
            BindingFlags.NonPublic | BindingFlags.Instance, 
            binder: null, 
            new Type[] { typeof(Stream), typeof(TransportContext) },
            modifiers: null);

        await (Task)method!.Invoke(target, new object?[] { stream, context })!;
    }
}

﻿using System.IO;
using System.Net.Http;
using System.Threading;

static partial class PolyfillExtensions
{
    public static Stream ReadAsStream(this HttpContent httpContent)
    {
        return httpContent.ReadAsStream(CancellationToken.None);    
    }
}
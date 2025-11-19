using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Meziantou.Polyfill.Tests;

public class SystemSecurityCryptographyTests
{
    [Fact]
    public void SHA256_HashData_ReadOnlySpan()
    {
        // Test with empty span - SHA256 of empty string
        var hash = SHA256.HashData(ReadOnlySpan<byte>.Empty);
        Assert.Equal(32, hash.Length);
        var expected = new byte[] { 0xE3, 0xB0, 0xC4, 0x42, 0x98, 0xFC, 0x1C, 0x14, 0x9A, 0xFB, 0xF4, 0xC8, 0x99, 0x6F, 0xB9, 0x24, 0x27, 0xAE, 0x41, 0xE4, 0x64, 0x9B, 0x93, 0x4C, 0xA4, 0x95, 0x99, 0x1B, 0x78, 0x52, 0xB8, 0x55 };
        Assert.Equal(expected, hash);

        // Test with actual data - SHA256 of "abc"
        ReadOnlySpan<byte> data = [0x61, 0x62, 0x63]; // "abc" in ASCII
        hash = SHA256.HashData(data);
        Assert.Equal(32, hash.Length);
        expected = new byte[] { 0xBA, 0x78, 0x16, 0xBF, 0x8F, 0x01, 0xCF, 0xEA, 0x41, 0x41, 0x40, 0xDE, 0x5D, 0xAE, 0x22, 0x23, 0xB0, 0x03, 0x61, 0xA3, 0x96, 0x17, 0x7A, 0x9C, 0xB4, 0x10, 0xFF, 0x61, 0xF2, 0x00, 0x15, 0xAD };
        Assert.Equal(expected, hash);
    }

    [Fact]
    public void MD5_HashData_ReadOnlySpan()
    {
        var hash = MD5.HashData(ReadOnlySpan<byte>.Empty);
        Assert.Equal(16, hash.Length);
        var expected = new byte[] { 0xD4, 0x1D, 0x8C, 0xD9, 0x8F, 0x00, 0xB2, 0x04, 0xE9, 0x80, 0x09, 0x98, 0xEC, 0xF8, 0x42, 0x7E };
        Assert.Equal(expected, hash);
    }

}

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
    public void CryptographicOperations_FixedTimeEquals_Value()
    {
        Assert.True(CryptographicOperations.FixedTimeEquals(ReadOnlySpan<byte>.Empty, (byte)42));
        Assert.True(CryptographicOperations.FixedTimeEquals([42, 42, 42], (byte)42));
        Assert.False(CryptographicOperations.FixedTimeEquals([42, 41, 42], (byte)42));
    }

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

    [Fact]
    public async Task SHA_HashData_Families()
    {
        var data = Encoding.UTF8.GetBytes("abc");
        await AssertAlgorithm(SHA1.Create, SHA1.HashData, SHA1.HashDataAsync, SHA1.TryHashData, 20);
        await AssertAlgorithm(SHA256.Create, SHA256.HashData, SHA256.HashDataAsync, SHA256.TryHashData, 32);
        await AssertAlgorithm(SHA384.Create, SHA384.HashData, SHA384.HashDataAsync, SHA384.TryHashData, 48);
        await AssertAlgorithm(SHA512.Create, SHA512.HashData, SHA512.HashDataAsync, SHA512.TryHashData, 64);

        async Task AssertAlgorithm(Func<HashAlgorithm> create, Func<byte[], byte[]> hashData, Func<Stream, CancellationToken, ValueTask<byte[]>> hashDataAsync, TryHashDataDelegate tryHashData, int size)
        {
            using var algorithm = create();
            var expected = algorithm.ComputeHash(data);
            Assert.Equal(expected, hashData(data));

            Span<byte> destination = stackalloc byte[size];
            Assert.True(tryHashData(data, destination, out var written));
            Assert.Equal(size, written);
            Assert.Equal(expected, destination.ToArray());

            using var stream = new MemoryStream(data);
            Assert.Equal(expected, await hashDataAsync(stream, CancellationToken.None));
        }
    }

    private delegate bool TryHashDataDelegate(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten);

    [Fact]
    public void IncrementalHash_AppendData_ReadOnlySpan()
    {
        using var incrementalHash = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
        incrementalHash.AppendData("abc"u8);

        var hash = incrementalHash.GetHashAndReset();
        var expected = SHA256.HashData("abc"u8);
        Assert.Equal(expected, hash);
    }

    [Fact]
    public void CryptographicOperations_FixedTimeEquals_Spans()
    {
        Assert.True(CryptographicOperations.FixedTimeEquals(ReadOnlySpan<byte>.Empty, ReadOnlySpan<byte>.Empty));
        Assert.True(CryptographicOperations.FixedTimeEquals([1, 2, 3], [1, 2, 3]));
        Assert.False(CryptographicOperations.FixedTimeEquals([1, 2, 3], [1, 2, 4]));
        Assert.False(CryptographicOperations.FixedTimeEquals([1, 2, 3], [1, 2]));
    }

    [Fact]
    public async Task HMAC_HashData_Families()
    {
        var key = Encoding.UTF8.GetBytes("Jefe");
        var data = Encoding.UTF8.GetBytes("what do ya want for nothing?");

        // HMAC-SHA256 test vector from RFC 4231 (test case 2)
        Assert.Equal(FromHex("5bdcc146bf60754e6a042426089575c75a003f089d2739839dec58b964ec3843"), HMACSHA256.HashData(key, data));

        await AssertAlgorithm(new HMACSHA256(key), HMACSHA256.HashData, HMACSHA256.HashData, HMACSHA256.HashData, HMACSHA256.HashData, HMACSHA256.HashData, HMACSHA256.HashData, HMACSHA256.TryHashData, HMACSHA256.HashDataAsync, HMACSHA256.HashDataAsync, HMACSHA256.HashDataAsync, 32);
        await AssertAlgorithm(new HMACSHA384(key), HMACSHA384.HashData, HMACSHA384.HashData, HMACSHA384.HashData, HMACSHA384.HashData, HMACSHA384.HashData, HMACSHA384.HashData, HMACSHA384.TryHashData, HMACSHA384.HashDataAsync, HMACSHA384.HashDataAsync, HMACSHA384.HashDataAsync, 48);
        await AssertAlgorithm(new HMACSHA512(key), HMACSHA512.HashData, HMACSHA512.HashData, HMACSHA512.HashData, HMACSHA512.HashData, HMACSHA512.HashData, HMACSHA512.HashData, HMACSHA512.TryHashData, HMACSHA512.HashDataAsync, HMACSHA512.HashDataAsync, HMACSHA512.HashDataAsync, 64);

        async Task AssertAlgorithm(
            HMAC algorithm,
            Func<byte[], byte[], byte[]> hashBytes,
            Func<byte[], Stream, byte[]> hashBytesStream,
            HmacSpanDelegate hashSpan,
            HmacSpanDestinationDelegate hashSpanDestination,
            HmacSpanStreamDelegate hashSpanStream,
            HmacSpanStreamDestinationDelegate hashSpanStreamDestination,
            TryHmacDelegate tryHash,
            Func<byte[], Stream, CancellationToken, ValueTask<byte[]>> hashBytesAsync,
            Func<ReadOnlyMemory<byte>, Stream, CancellationToken, ValueTask<byte[]>> hashMemoryAsync,
            Func<ReadOnlyMemory<byte>, Stream, Memory<byte>, CancellationToken, ValueTask<int>> hashMemoryDestinationAsync,
            int size)
        {
            using (algorithm)
            {
                var expected = algorithm.ComputeHash(data);
                Assert.Equal(size, expected.Length);

                Assert.Equal(expected, hashBytes(key, data));
                Assert.Equal(expected, hashBytesStream(key, new MemoryStream(data)));
                Assert.Equal(expected, hashSpan(key, data));
                Assert.Equal(expected, hashSpanStream(key, new MemoryStream(data)));

                var destination = new byte[size];
                Assert.Equal(size, hashSpanDestination(key, data, destination));
                Assert.Equal(expected, destination);

                destination = new byte[size];
                Assert.Equal(size, hashSpanStreamDestination(key, new MemoryStream(data), destination));
                Assert.Equal(expected, destination);

                destination = new byte[size];
                Assert.True(tryHash(key, data, destination, out var written));
                Assert.Equal(size, written);
                Assert.Equal(expected, destination);
                Assert.False(tryHash(key, data, new byte[size - 1], out written));
                Assert.Equal(0, written);

                Assert.Throws<ArgumentException>(() => hashSpanDestination(key, data, new byte[size - 1]));

                Assert.Equal(expected, await hashBytesAsync(key, new MemoryStream(data), CancellationToken.None));
                Assert.Equal(expected, await hashMemoryAsync(key, new MemoryStream(data), CancellationToken.None));

                destination = new byte[size];
                Assert.Equal(size, await hashMemoryDestinationAsync(key, new MemoryStream(data), destination, CancellationToken.None));
                Assert.Equal(expected, destination);
            }
        }
    }

    private delegate byte[] HmacSpanDelegate(ReadOnlySpan<byte> key, ReadOnlySpan<byte> source);
    private delegate int HmacSpanDestinationDelegate(ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, Span<byte> destination);
    private delegate byte[] HmacSpanStreamDelegate(ReadOnlySpan<byte> key, Stream source);
    private delegate int HmacSpanStreamDestinationDelegate(ReadOnlySpan<byte> key, Stream source, Span<byte> destination);
    private delegate bool TryHmacDelegate(ReadOnlySpan<byte> key, ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten);

    [Fact]
    public void Rfc2898DeriveBytes_Pbkdf2()
    {
        // Test vectors from RFC 6070 (PBKDF2-HMAC-SHA1)
        Assert.Equal(FromHex("0c60c80f961f0e71f3a9b524af6012062fe037a6"), Rfc2898DeriveBytes.Pbkdf2("password", Encoding.UTF8.GetBytes("salt"), 1, HashAlgorithmName.SHA1, 20));
        Assert.Equal(FromHex("4b007901b765489abead49d926f721d065a429c1"), Rfc2898DeriveBytes.Pbkdf2("password", Encoding.UTF8.GetBytes("salt"), 4096, HashAlgorithmName.SHA1, 20));
        Assert.Equal(
            FromHex("3d2eec4fe41c849b80c8d83662c0e44a8b291a964cf2f07038"),
            Rfc2898DeriveBytes.Pbkdf2("passwordPASSWORDpassword", Encoding.UTF8.GetBytes("saltSALTsaltSALTsaltSALTsaltSALTsalt"), 4096, HashAlgorithmName.SHA1, 25));

        // Every overload must agree with the instance-based implementation, including multi-block outputs
        var password = "p\u00e4ssw\u00f6rd";
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var salt = Encoding.UTF8.GetBytes("NaCl-salt-value");
        foreach (var algorithm in new[] { HashAlgorithmName.SHA1, HashAlgorithmName.SHA256, HashAlgorithmName.SHA384, HashAlgorithmName.SHA512 })
        {
            const int Iterations = 1000;
            const int Length = 150;
            byte[] expected;
#pragma warning disable SYSLIB0060, CA5379, CA5387 // Reference implementation used to validate the polyfill
            using (var instance = new Rfc2898DeriveBytes(passwordBytes, salt, Iterations, algorithm))
            {
                expected = instance.GetBytes(Length);
            }
#pragma warning restore SYSLIB0060, CA5379, CA5387

            Assert.Equal(expected, Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt, Iterations, algorithm, Length));
            Assert.Equal(expected, Rfc2898DeriveBytes.Pbkdf2(passwordBytes.AsSpan(), salt.AsSpan(), Iterations, algorithm, Length));
            Assert.Equal(expected, Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, algorithm, Length));
            Assert.Equal(expected, Rfc2898DeriveBytes.Pbkdf2(password.AsSpan(), salt.AsSpan(), Iterations, algorithm, Length));

            var destination = new byte[Length];
            Rfc2898DeriveBytes.Pbkdf2(passwordBytes.AsSpan(), salt.AsSpan(), destination.AsSpan(), Iterations, algorithm);
            Assert.Equal(expected, destination);

            destination = new byte[Length];
            Rfc2898DeriveBytes.Pbkdf2(password.AsSpan(), salt.AsSpan(), destination.AsSpan(), Iterations, algorithm);
            Assert.Equal(expected, destination);
        }

        Assert.Empty(Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt, 1, HashAlgorithmName.SHA256, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt, 0, HashAlgorithmName.SHA256, 32));
        Assert.Throws<ArgumentOutOfRangeException>(() => Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt, 1, HashAlgorithmName.SHA256, -1));
        Assert.Throws<CryptographicException>(() => Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt, 1, new HashAlgorithmName("FOO"), 32));
        Assert.Throws<ArgumentNullException>(() => Rfc2898DeriveBytes.Pbkdf2((string)null!, salt, 1, HashAlgorithmName.SHA256, 32));
        Assert.Throws<EncoderFallbackException>(() => Rfc2898DeriveBytes.Pbkdf2("a\uD800", salt, 1, HashAlgorithmName.SHA256, 32));
    }

    private static byte[] FromHex(string value)
    {
        var result = new byte[value.Length / 2];
        for (var i = 0; i < result.Length; i++)
        {
            result[i] = Convert.ToByte(value.Substring(i * 2, 2), 16);
        }

        return result;
    }
}

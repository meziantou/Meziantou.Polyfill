using System;
using System.Security.Cryptography;
static partial class PolyfillExtensions_Rfc2898DeriveBytes
{
    extension(Rfc2898DeriveBytes)
    {
        public static byte[] Pbkdf2(ReadOnlySpan<byte> password, ReadOnlySpan<byte> salt, int iterations, HashAlgorithmName hashAlgorithm, int outputLength)
        {
            if (outputLength < 0)
                throw new ArgumentOutOfRangeException(nameof(outputLength), outputLength, "Output length must be a non-negative value.");

            var result = new byte[outputLength];
            Pbkdf2Helper.Derive(password.ToArray(), salt, result, iterations, hashAlgorithm);
            return result;
        }
    }
}

file static class Pbkdf2Helper
{
    public static void Derive(byte[] password, ReadOnlySpan<byte> salt, Span<byte> destination, int iterations, HashAlgorithmName hashAlgorithm)
    {
        if (iterations <= 0)
            throw new ArgumentOutOfRangeException(nameof(iterations), iterations, "Iterations must be a positive value.");

        var name = hashAlgorithm.Name;
        if (name is null)
            throw new ArgumentNullException(nameof(hashAlgorithm));
        if (name.Length == 0)
            throw new ArgumentException("The hash algorithm name cannot be null or empty.", nameof(hashAlgorithm));

        using HMAC hmac = name switch
        {
            "SHA1" => new HMACSHA1(password),
            "SHA256" => new HMACSHA256(password),
            "SHA384" => new HMACSHA384(password),
            "SHA512" => new HMACSHA512(password),
            _ => throw new CryptographicException("'" + name + "' is not a known hash algorithm."),
        };

        // PBKDF2 (RFC 8018): T_i = U_1 ^ U_2 ^ ... ^ U_c, with U_1 = PRF(P, S || INT(i)) and U_j = PRF(P, U_{j-1})
        var block = new byte[salt.Length + 4];
        salt.CopyTo(block);
        var offset = 0;
        var blockIndex = 1u;
        while (offset < destination.Length)
        {
            block[salt.Length] = (byte)(blockIndex >> 24);
            block[salt.Length + 1] = (byte)(blockIndex >> 16);
            block[salt.Length + 2] = (byte)(blockIndex >> 8);
            block[salt.Length + 3] = (byte)blockIndex;

            var u = hmac.ComputeHash(block);
            var t = (byte[])u.Clone();
            for (var i = 1; i < iterations; i++)
            {
                u = hmac.ComputeHash(u);
                for (var j = 0; j < t.Length; j++)
                {
                    t[j] ^= u[j];
                }
            }

            var count = Math.Min(t.Length, destination.Length - offset);
            t.AsSpan(0, count).CopyTo(destination.Slice(offset));
            offset += count;
            blockIndex++;
        }
    }
}

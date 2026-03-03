partial class PolyfillExtensions
{
    extension(System.Guid)
    {
        public static System.Guid CreateVersion7(System.DateTimeOffset timestamp)
        {
            // UUIDv7 structure (RFC 9562, Section 5.7):
            //  0                   1                   2                   3
            //  0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
            // +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            // |                           unix_ts_ms                          |
            // +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            // |          unix_ts_ms           |  ver  |       rand_a          |
            // +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            // |var|                        rand_b                             |
            // +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
            // |                           rand_b                              |
            // +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+

            long unixMs = timestamp.ToUnixTimeMilliseconds();

            if (unchecked((ulong)unixMs) > 0xFFFF_FFFF_FFFFUL)
            {
                throw new System.ArgumentOutOfRangeException(nameof(timestamp));
            }

            byte[] rand = new byte[10];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(rand);
            }

            // Set version 7 in high nibble of UUID byte 6 (rand[0])
            rand[0] = (byte)((rand[0] & 0x0F) | 0x70);

            // Set variant 0b10 in high 2 bits of UUID byte 8 (rand[2])
            rand[2] = (byte)((rand[2] & 0x3F) | 0x80);

            // Build the Guid from the UUID v7 big-endian byte layout:
            // UUID bytes 0-3 → Data1 (upper 32 bits of 48-bit timestamp, big-endian)
            // UUID bytes 4-5 → Data2 (lower 16 bits of timestamp, big-endian)
            // UUID bytes 6-7 → Data3 (version nibble + rand_a, big-endian)
            // UUID bytes 8-15 → Data4 (variant bits + rand_b, stored as-is)
            return new System.Guid(
                unchecked((int)(unixMs >> 16)),
                unchecked((short)(unixMs & 0xFFFF)),
                (short)((rand[0] << 8) | rand[1]),
                rand[2], rand[3], rand[4], rand[5], rand[6], rand[7], rand[8], rand[9]);
        }
    }
}

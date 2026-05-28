using System;

static partial class PolyfillExtensions
{
    extension(Enum)
    {
        public static bool TryParse(Type enumType, string? value, bool ignoreCase, out object? result)
        {
            if (enumType is null)
                throw new ArgumentNullException(nameof(enumType));
            if (!enumType.IsEnum)
                throw new ArgumentException("Type provided must be an Enum.", nameof(enumType));
            if (value is null)
            {
                result = null;
                return false;
            }

            try
            {
                result = Enum.Parse(enumType, value, ignoreCase);
                return true;
            }
            catch (ArgumentException)
            {
                result = null;
                return false;
            }
            catch (OverflowException)
            {
                result = null;
                return false;
            }
        }
    }
}

namespace TypingSpeedTest.Utilities
{
    public static class TypeExtensions
    {
        public static bool IsNull<T>(this T value)
        {
            return value == null;
        }
        public static bool IsNotNull<T>(this T value)
        {
            return value != null;
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}

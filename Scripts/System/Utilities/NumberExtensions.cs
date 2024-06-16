using System.Linq;

namespace UserSystemFramework.Scripts.System.Utilities
{
    /// <summary>
    /// Simple string extension methods to reduce parsing needs.
    /// </summary>
    public static class NumberExtensions
    {
        public static int ToInt(this string input)
        {
            int.TryParse(input, out var outer);
            return outer;
        }
        public static float ToFloat(this string input)
        {
            float.TryParse(input, out var outer);
            return outer;
        }
        public static double ToDouble(this string input)
        {
            double.TryParse(input, out var outer);
            return outer;
        }
        public static long ToLong(this string input)
        {
            long.TryParse(input, out var outer);
            return outer;
        }
        public static string ToDigitsOnly(this string input)
        {
            return string.Concat(input.ToArray().Reverse().TakeWhile(char.IsNumber).Reverse());
        }
        public static int ExtractToInt(this string input)
        {
            return input.ToDigitsOnly().ToInt();

        }
    }
}
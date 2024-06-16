using System;

namespace UserSystemFramework.Scripts.System.Utilities
{
    public static class StatusCodeChecker
    {
        private static readonly int[] SuccessCodes = new[] { 0, 6, 7};
        private static readonly int[] NeutralCodes = new[] { 16, 23 };
        private static readonly int[] ErrorCodes = new[] { 1, 2, 3, 4, 5, 8, 9, 10, 11, 12, 13, 14, 15, 17, 18, 19, 20, 21 ,22, 999, 998 };
        public static bool ContainsErrorCode(this string stringToCheck) => Array.Exists(ErrorCodes, x => stringToCheck.Contains("[" + x.ToString() + "]"));
        public static bool ContainsSuccessCode(this string stringToCheck) => Array.Exists(SuccessCodes, x => stringToCheck.Contains("[" + x.ToString() + "]"));
        public static bool ContainsNeutralCode(this string stringToCheck) => Array.Exists(NeutralCodes, x => stringToCheck.Contains("[" + x.ToString() + "]"));
    }
}
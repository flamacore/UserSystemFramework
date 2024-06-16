using System;
using System.Collections;
using System.Collections.Generic;
using UserSystemFramework.Scripts.System.Data.Interfaces;

namespace UserSystemFramework.Scripts.System.Utilities
{
    /// <summary>
    /// Simple string extension methods to stylize.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly string[] ErrorStringGuessList = new []
        {
            "500 Internal Server Error",
            "501 Not Implemented",
            "502 Bad Gateway",
            "503 Service Unavailable",
            "504 Gateway Timeout",
            "505 HTTP Version Not Supported",
            "511 Network Authentication Required",
            "400 Bad Request",
            "401 Unauthorized",
            "402 Payment Required",
            "403 Forbidden",
            "404 Not Found",
            "405 Method Not Allowed",
            "406 Not Acceptable",
            "407 Proxy Authentication Required",
            "408 Request Timeout",
            "409 Conflict",
            "410 Gone",
            "411 Length Required",
            "412 Precondition Failed",
            "413 Request Entity Too Large",
            "414 Request-URI Too Long",
            "415 Unsupported Media Type",
            "416 Requested Range Not Satisfiable",
            "417 Expectation Failed",
            "429 Too Many Requests",
            "431 Request Header Fields Too Large",
            "444 No Response",
            "449 Retry With",
            "450 Blocked by Windows Parental Controls",
            "451 Unavailable For Legal Reasons",
            "499 Client Closed Request"
        };

        /// <summary>
        /// Returns the input string wrapped in html bold tags with &lt;b&gt;
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>&lt;b&gt;str&lt;/b&gt;</returns>
        public static string Bold(this string str) => "<b>" + str + "</b>";

        /// <summary>
        /// Returns the input string wrapped in html color tags with &lt;color&gt;
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="clr">Input color in hex format like #ffffff</param>
        /// <returns>&lt;color=clr&gt;str&lt;/color&gt;</returns>
        public static string Color(this string str, string clr) => $"<color={clr}>{str}</color>";
        
        /// <summary>
        /// Returns the input string wrapped in html bold tags with &lt;i&gt;
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>&lt;i&gt;str&lt;/i&gt;</returns>
        public static string Italic(this string str) => "<i>" + str + "</i>";
        
        /// <summary>
        /// Returns the input string wrapped in html size tags with &lt;size&gt;
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="size">Input size</param>
        /// <returns>&lt;size=size&gt;str&lt;/size&gt;</returns>
        public static string Size(this string str, int size) => $"<size={size}>{str}</size>";

        public static string StylizeError(this string str)
        {
            string errorString = "";
            bool hasError = Array.Exists(ErrorStringGuessList, x =>
            {
                bool b = false;
                if (str.Contains(x, StringComparison.OrdinalIgnoreCase))
                {
                    b = true;
                    errorString = x;
                }
                return b;
            });
            return hasError ? str.Replace(errorString, $"<color=red>{errorString}</color>") : str;
        }
    }
}
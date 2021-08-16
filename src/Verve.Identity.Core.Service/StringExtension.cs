using System.Collections.Generic;
using System.Linq;

namespace Verve.Identity.Core.Service
{
    /// <summary>
    /// Contains String extension methods
    /// </summary>
    public static class StringExtension
    {
        private const string AlphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string NormalizedString(this string textToNormalize, string overrides = null)
        {
            if (string.IsNullOrEmpty(textToNormalize))
            {
                return string.Empty;
            }

            overrides ??= string.Empty;

            var retString = textToNormalize.ToUpper().Replace(" ", string.Empty);
            return string.Concat(GetCharsAndNumbers(retString, overrides));
        }

        private static IEnumerable<char> GetCharsAndNumbers(string retString, string overrides)
        {
            return retString.Where(ch => IsAlphaNumeric(ch) || overrides.Contains(ch));
        }

        private static bool IsAlphaNumeric(char ch)
        {
            return AlphaNumeric.Contains(ch.ToString());
        }
    }
}

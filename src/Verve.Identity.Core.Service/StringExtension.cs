
using System.Collections.Generic;
using System.Text;

namespace Verve.Identity.Core.Service
{
    public static class StringExtension
    {
        private const string AlphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string NormalizedString(this string textToNormalize, string overrides=null)
        {
            if (string.IsNullOrEmpty(textToNormalize))
            {
                return string.Empty;
            }

            if (overrides == null)
            {
                overrides = string.Empty;
            }

            var retString = textToNormalize.ToUpper().Replace(" ", string.Empty);
            return string.Concat(GetCharsAndNumbers(retString, overrides));
        }

        private static IEnumerable<char> GetCharsAndNumbers(string retString, string overrides)
        {
            foreach (char ch in retString.ToCharArray())
            {
                if (IsNumberOrAlphabet(ch) || overrides.Contains(ch))
                {
                    yield return ch;
                }
            }
        }

        private static bool IsNumberOrAlphabet(char ch)
        {
            return AlphaNumeric.Contains(ch.ToString());
        }
    }
}

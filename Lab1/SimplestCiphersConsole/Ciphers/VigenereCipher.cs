using System.Diagnostics.SymbolStore;
using System.Security.Cryptography;
using System.Text;

namespace SimplestCiphers.Ciphers
{
    public static class VigenereCipher
    {
        private static Language _language = Language.RuLang;

        private const int KeyShift = 1;

        public static string Encode(string source, string key)
        {
            string str = StringScaner.GetDesiredString(source, _language).ToLower();
            key = key.ToLower();

            int i = 0;
            StringBuilder result = new StringBuilder();

            foreach (char c in str)
            {
                result.Append(_language[(_language[c] + _language[key[i++]]) % _language.Length]);

                if (i == key.Length)
                {
                    i = 0;
                    StringBuilder temp = new StringBuilder();

                    foreach (char nc in key)
                    {
                        temp.Append(_language[(_language[nc] + KeyShift) % _language.Length]);
                    }
                    key = temp.ToString();
                }
            }
            return StringScaner.EmbedString(result.ToString(), source, _language);
        }

        public static string Decode(string source, string key)
        {
            string str = StringScaner.GetDesiredString(source, _language).ToLower();
            key = key.ToLower();

            int i = 0;
            StringBuilder result = new StringBuilder();

            foreach (char c in str)
            {
                result.Append(_language[(_language[c] - _language[key[i++]] + _language.Length) % _language.Length]);

                if (i == key.Length)
                {
                    i = 0;
                    StringBuilder temp = new StringBuilder();

                    foreach (char nc in key)
                    {
                        temp.Append(_language[(_language[nc] + KeyShift) % _language.Length]);
                    }
                    key = temp.ToString();
                }
            }

            return StringScaner.EmbedString(result.ToString(), source, _language);
        }
    }
}

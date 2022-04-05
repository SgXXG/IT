using System.Diagnostics.SymbolStore;
using System.Security.Cryptography;
using Microsoft.VisualBasic.CompilerServices;

namespace SimplestCiphers.Ciphers
{
    public static class VigenereCipher
    {
        private const StringScaner.Language _language = StringScaner.Language.Ru;
        private const char AlphabetStart = 'а';
        private const int KeyShift = 1;

        public static char CharEncode(char source, int count)
        {
            int Len = (int) 'я' - (int) 'а' + 1;
            return (char) (((int) source - (int) AlphabetStart + count + Len) % Len + (int) AlphabetStart);
        }
        public static string Encode(string source, string key)
        {
            string str = StringScaner.GetDesiredString(source, _language);

            int i = 0;
            string result = String.Empty;
            foreach (char c in str)
            {
                result += CharEncode(c, (int)key[i++] - (int)AlphabetStart);
                if (i == key.Length)
                {
                    i = 0;
                    string temp = String.Empty;
                    foreach (char nc in key)
                    {
                        temp += CharEncode(nc, KeyShift);
                    }
                    key = temp;
                }
            }

            return StringScaner.EmbedString(result, source, _language);
        }
        public static string Decode(string source, string key)
        {
            string str = StringScaner.GetDesiredString(source, _language);

            int i = 0;
            string result = String.Empty;
            foreach (char c in str)
            {
                result += CharEncode(c, -((int)key[i++] - (int)AlphabetStart));
                if (i == key.Length)
                {
                    i = 0;
                    string temp = String.Empty;
                    foreach (char nc in key)
                    {
                        temp += CharEncode(nc, KeyShift);
                    }
                    key = temp;
                }
            }

            return StringScaner.EmbedString(result, source, _language);
        }
    }
}

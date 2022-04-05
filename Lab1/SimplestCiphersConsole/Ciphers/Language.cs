using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplestCiphers.Ciphers
{
    public class Language
    {
        public static readonly Language RuLang = new Language("абвгдеёжзийклмнопрстуфхцчшщъыьэюя");

        public static readonly Language EnLang = new Language("abcdefghijklmnopqrstuvwxyz");

        private string _letters;

        public string Letters => (string)_letters.Clone();

        public Language(string letters)
        {
            _letters = ((string)letters.Clone()).ToLower();
        }

        public int Length => _letters.Length;

        public bool Contains(char c) => _letters.Contains(char.ToLower(c));

        public char this[int index] => _letters[index];

        public int this[char c] => _letters.IndexOf(char.ToLower(c));

    }
}

using System.ComponentModel;

namespace SimplestCiphers.Ciphers
{
    public static class RailFenceCipher
    {
        private enum Direction { Up, Down }

        private static Language _language = Language.EnLang;

        public static string Encode(string source, int key)
        {
            string str = StringScaner.GetDesiredString(source, _language);

            var array = new string[key];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = string.Empty;
            }

            int j = 0;
            Direction dir = Direction.Down;
            foreach (char c in str)
            {
                array[j] += c;

                j += (dir == Direction.Down ? 1 : -1);
                if (j == key - 1) { dir = Direction.Up; }
                if (j == 0) { dir = Direction.Down; }
            }

            return StringScaner.EmbedString(string.Join("", array), source, _language);
        }
        public static string Decode(string source, int key)
        {
            string str = StringScaner.GetDesiredString(source, _language);

            var array = new string[key];
            var counts = new int[key];
            var index = new int[key];
            for (int i = 0; i < key; i++)
            {
                array[i] = string.Empty;
                counts[i] = 0;
                index[i] = 0;
            }

            int j = 0;
            Direction dir = Direction.Down;
            foreach (char c in str)
            {
                counts[j]++;

                j += (dir == Direction.Down ? 1 : -1);
                if (j == key - 1) { dir = Direction.Up; }
                if (j == 0) { dir = Direction.Down; }
            }

            j = 0;
            for (int i = 0; i < key; i++)
            {
                array[i] = str[j..(j + counts[i])];
                j += counts[i];
            }

            j = 0;
            dir = Direction.Down;
            string result = string.Empty;
            foreach (char c in str)
            {
                result += array[j][index[j]++];

                j += (dir == Direction.Down ? 1 : -1);
                if (j == key - 1) { dir = Direction.Up; }
                if (j == 0) { dir = Direction.Down; }
            }

            return StringScaner.EmbedString(result, source, _language);
        }
    }
}

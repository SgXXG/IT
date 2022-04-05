namespace SimplestCiphers.Ciphers
{
    public static class PlayfairCipher
    {
        private static Language _language = Language.EnLang;

        const int CIPHER_TABLE_SIZE = 5;
        static char[,] CIPHER_TABLE = { { 'C', 'R', 'Y', 'P', 'T' },
                                        { 'O', 'G', 'A', 'H', 'B' },
                                        { 'D', 'E', 'F', 'I', 'K' },
                                        { 'L', 'M', 'N', 'Q', 'S' },
                                        { 'U', 'V', 'W', 'X', 'Z' }
        };

        public delegate bool IsCorrectSymbol(char symbol);
        public static bool IsCorrectSymbolRu(char symbol)
        {
            if (symbol == 'ё' || symbol == 'Ё')
                return true;

            for (char c = 'А'; c <= 'я'; c++)
            {
                if (c == symbol)
                    return true;
            }
            return false;
        }

        public static bool IsCorrectSymbolEn(char symbol)
        {
            for (char c = 'A'; c <= 'z'; c++)
            {
                if (c == symbol)
                    return true;
            }
            return false;
        }

        public static char Symbol(char symbol)
        {
            if (symbol == 'J')
                return 'I';
            else
                return symbol;
        }

        public static (int, int) FindIndex(char symbol)
        {
            for (int i = 0; i < CIPHER_TABLE_SIZE; i++)
            {
                for (int j = 0; j < CIPHER_TABLE_SIZE; j++)
                {
                    if (CIPHER_TABLE[i, j] == symbol)
                    {
                        var index = (i, j);
                        return index;
                    }
                }
            }
            return (-1, -1);
        }

        public static IsCorrectSymbol GetCorrectSymbolMethod(string language)
        {
            if (language == "Ru")
                return IsCorrectSymbolRu;
            else
                return IsCorrectSymbolEn;
        }

        public static string ClearText(string str, string language)
        {
            string correctText = "";
            IsCorrectSymbol isCorrectSymbol = GetCorrectSymbolMethod(language);

            for (int i = 0; i < str.Length; i++)
            {
                if (isCorrectSymbol(str[i]))
                    correctText += str[i];
            }
            return correctText;
        }
        public static void ResultText(string str, ref string source, string language)
        {
            IsCorrectSymbol isCorrectSymbol = GetCorrectSymbolMethod(language);

            string tmpStr = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (!isCorrectSymbol(str[i]))
                {
                    tmpStr = "";
                    tmpStr += str[i];
                    if (i < source.Length)
                        source = source.Insert(i, tmpStr);
                    else
                        source = string.Concat(source, tmpStr);
                }
            }
        }

        public static string Encode(string plaintext)
        {
            IsCorrectSymbol isCorrectSymbol = GetCorrectSymbolMethod("En");
            string tmpStr = plaintext;
            plaintext = ClearText(plaintext, "En");
            plaintext = plaintext.ToUpper();
            string resultStr = "";
            string nullSymbol = "X";
            int i = 0;
            int j = 0;
            bool isConcat = false;
            bool isInsert = false;

            while (i < plaintext.Length)
            {
                (int row, int column) firstSymbol = FindIndex(Symbol(plaintext[i]));
                i++;

                while (!isCorrectSymbol(tmpStr[j]))
                    j++;
                j++;

                isConcat = false;
                if (i >= plaintext.Length)
                {
                    plaintext = string.Concat(plaintext, nullSymbol);
                    tmpStr = tmpStr.Insert(j, nullSymbol);
                    isConcat = true;
                }

                isInsert = false;
                if (plaintext[i] == plaintext[i - 1] && !isConcat)
                {
                    plaintext = plaintext.Insert(i, nullSymbol);
                    tmpStr = tmpStr.Insert(j, nullSymbol);
                    isInsert = true;
                    j++;
                }

                (int row, int column) secondSymbol = FindIndex(Symbol(plaintext[i]));

                if (firstSymbol.row == secondSymbol.row)
                {
                    if (firstSymbol.column == CIPHER_TABLE_SIZE - 1)
                        firstSymbol.column = 0;
                    else
                        firstSymbol.column++;

                    if (secondSymbol.column == CIPHER_TABLE_SIZE - 1)
                        secondSymbol.column = 0;
                    else
                        secondSymbol.column++;
                }

                if (firstSymbol.column == secondSymbol.column && plaintext[i] != plaintext[i - 1])
                {
                    if (firstSymbol.row == CIPHER_TABLE_SIZE - 1)
                        firstSymbol.row = 0;
                    else
                        firstSymbol.row++;

                    if (secondSymbol.row == CIPHER_TABLE_SIZE - 1)
                        secondSymbol.row = 0;
                    else
                        secondSymbol.row++;
                }

                if ((firstSymbol.row != secondSymbol.row) &&
                    (firstSymbol.column != secondSymbol.column))
                {
                    int difference = firstSymbol.column - secondSymbol.column;
                    if (difference > 0)
                    {
                        firstSymbol.column -= difference;
                        secondSymbol.column += difference;
                    }
                    else
                    {
                        difference = -difference;
                        firstSymbol.column += difference;
                        secondSymbol.column -= difference;
                    }
                }
                resultStr += CIPHER_TABLE[firstSymbol.row, firstSymbol.column];
                resultStr += CIPHER_TABLE[secondSymbol.row, secondSymbol.column];
                i++;
                if (!isInsert)
                    j++;
            }

            ResultText(tmpStr, ref resultStr, "En");
            return resultStr;
        }
        public static string Decode(string encryptedText)
        {
            IsCorrectSymbol isCorrectSymbol = GetCorrectSymbolMethod("En");
            string tmpStr = encryptedText;
            encryptedText = ClearText(encryptedText, "En");
            encryptedText = encryptedText.ToUpper();
            string nullSymbol = "X";
            int i = 0;
            int j = 0;
            bool isConcat = false;
            string resultStr = "";


            while (i < encryptedText.Length)
            {
                (int row, int column) firstSymbol = FindIndex(Symbol(encryptedText[i]));
                i++;

                while (!isCorrectSymbol(tmpStr[j]))
                    j++;
                j++;

                isConcat = false;
                if (i >= encryptedText.Length)
                {
                    encryptedText = string.Concat(encryptedText, nullSymbol);
                    tmpStr = tmpStr.Insert(j, nullSymbol);
                    isConcat = true;
                }

                if (encryptedText[i] == encryptedText[i - 1] && !isConcat)
                {
                    encryptedText = encryptedText.Insert(i, nullSymbol);
                    tmpStr = tmpStr.Insert(j, nullSymbol);
                    j++;
                }

                (int row, int column) secondSymbol = FindIndex(Symbol(encryptedText[i]));

                if (firstSymbol.row == secondSymbol.row)
                {
                    if (firstSymbol.column == 0)
                        firstSymbol.column = CIPHER_TABLE_SIZE - 1;
                    else
                        firstSymbol.column--;

                    if (secondSymbol.column == 0)
                        secondSymbol.column = CIPHER_TABLE_SIZE - 1;
                    else
                        secondSymbol.column--;
                }

                if (firstSymbol.column == secondSymbol.column && encryptedText[i] != encryptedText[i - 1])
                {
                    if (firstSymbol.row == 0)
                        firstSymbol.row = CIPHER_TABLE_SIZE - 1;
                    else
                        firstSymbol.row--;

                    if (secondSymbol.row == 0)
                        secondSymbol.row = CIPHER_TABLE_SIZE - 1;
                    else
                        secondSymbol.row--;
                }

                if ((firstSymbol.row != secondSymbol.row) &&
                    (firstSymbol.column != secondSymbol.column))
                {
                    int difference = firstSymbol.column - secondSymbol.column;
                    if (difference > 0)
                    {
                        firstSymbol.column -= difference;
                        secondSymbol.column += difference;
                    }
                    else
                    {
                        difference = -difference;
                        firstSymbol.column += difference;
                        secondSymbol.column -= difference;
                    }
                }
                resultStr += CIPHER_TABLE[firstSymbol.row, firstSymbol.column];
                resultStr += CIPHER_TABLE[secondSymbol.row, secondSymbol.column];
                i++;
            }

            ResultText(tmpStr, ref resultStr, "En");
            return resultStr;
        }
    }
}

namespace SimplestCiphers.Ciphers
{
    public static class PlayfairCipher
    {
        private const StringScaner.Language _language = StringScaner.Language.En;

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

        public static string Encode(string source)
        {
            IsCorrectSymbol isCorrectSymbol = GetCorrectSymbolMethod("En");

            string str = StringScaner.GetDesiredString(source, _language);

            string tmpStr = source;
            str = str.ToUpper();
            string resultStr = "";
            string nullSymbol = "X";
            int i = 0;
            int j = 0;
            bool isConcat = false;
            bool isInsert = false;

            while (i < str.Length)
            {
                (int row, int column) firstSymbol = FindIndex(Symbol(str[i]));
                i++;

                while (!isCorrectSymbol(tmpStr[j]))
                    j++;
                j++;

                isConcat = false;
                if (i >= str.Length)
                {
                    str = string.Concat(str, nullSymbol);
                    tmpStr = tmpStr.Insert(j, nullSymbol);
                    isConcat = true;
                }

                isInsert = false;
                if (str[i] == str[i - 1] && !isConcat)
                {
                    str = str.Insert(i, nullSymbol);
                    tmpStr = tmpStr.Insert(j, nullSymbol);
                    isInsert = true;
                    j++;
                }
                (int row, int column) secondSymbol = FindIndex(Symbol(str[i]));

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

                if (firstSymbol.column == secondSymbol.column && str[i] != str[i - 1])
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
        public static string Decode(string source)
        {
            string str = StringScaner.GetDesiredString(source, _language);

            IsCorrectSymbol isCorrectSymbol = GetCorrectSymbolMethod("En");
            string tmpStr = source;
            source = ClearText(source, "En");
            source = source.ToUpper();
            string nullSymbol = "X";
            int i = 0;
            int j = 0;
            bool isConcat = false;
            string resultStr = "";


            while (i < source.Length)
            {
                (int row, int column) firstSymbol = FindIndex(Symbol(source[i]));
                i++;

                while (!isCorrectSymbol(tmpStr[j]))
                    j++;
                j++;

                isConcat = false;
                if (i >= source.Length)
                {
                    source = string.Concat(source, nullSymbol);
                    tmpStr = tmpStr.Insert(j, nullSymbol);
                    isConcat = true;
                }

                if (source[i] == source[i - 1] && !isConcat)
                {
                    source = source.Insert(i, nullSymbol);
                    tmpStr = tmpStr.Insert(j, nullSymbol);
                    j++;
                }

                (int row, int column) secondSymbol = FindIndex(Symbol(source[i]));

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

                if (firstSymbol.column == secondSymbol.column && source[i] != source[i - 1])
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

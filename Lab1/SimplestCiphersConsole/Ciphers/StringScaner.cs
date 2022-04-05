using System.Diagnostics;
using System.Diagnostics.Tracing;

namespace SimplestCiphers.Ciphers;

public static class StringScaner
{
    public enum Language { En, Ru }

    public static string GetDesiredString(string source, Language language)
    {
        string result = String.Empty;
        foreach (char c in source)
        {
            if (language == Language.En)
            {
                if (c is >= 'a' and <= 'z' or >= 'A' and <= 'Z')
                {
                    result += c;
                }
            }
            else
            {
                if (c is >= 'а' and <= 'я' or >= 'А' and <= 'Я')
                {
                    result += c;
                }
            }
        }
        return result;

    }
    
    public static string EmbedString(string source, string destination, Language language)
    {
        string result = String.Empty;
        int j = 0;
        foreach (char c in destination)
        {
            if (language == Language.En)
            {
                if (c is >= 'a' and <= 'z' or >= 'A' and <= 'Z')
                {
                    result += source[j++];
                }
                else
                {
                    result += c;
                }
            }
            else
            {
                if (c is >= 'а' and <= 'я' or >= 'А' and <= 'Я')
                {
                    result += source[j++];
                }
                else
                {
                    result += c;
                }
            }
        }

        if (j != source.Length - 1)
        {
            result += source[j..];
        }

        return result;
    }
}
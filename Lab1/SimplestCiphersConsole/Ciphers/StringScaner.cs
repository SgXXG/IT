using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Text;

namespace SimplestCiphers.Ciphers;

public static class StringScaner
{
    public static string GetDesiredString(string source, Language language)
    {
        StringBuilder result = new StringBuilder();
        foreach (char c in source)
            if (language.Contains(c))
                result.Append(c);

        return result.ToString();
    }

    public static string EmbedString(string source, string destination, Language language)
    {
        StringBuilder result = new StringBuilder();
        int j = 0;

        foreach (char c in destination)
            result.Append(language.Contains(c) ? source[j++] : c);

        if (j != source.Length - 1)
        {
            result.Append(source[j..]);
        }

        return result.ToString();
    }
}
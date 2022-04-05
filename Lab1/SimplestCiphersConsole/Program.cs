using SimplestCiphers.Ciphers;

Console.WriteLine(VigenereCipher.CharEncode('а', -2));
string key = "абвг";
Console.WriteLine(VigenereCipher.Encode("аааааа", key));
Console.WriteLine(VigenereCipher.Decode(VigenereCipher.Encode("аааааа", key), key));
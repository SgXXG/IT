using System.Text;
using RabinEncryptionConsole;
//
 var enc = new RabinEncryptor(991, 983, 790);
//
// var text = Encoding.ASCII.GetBytes("BSUIR");
// var encrypted = enc.Encrypt(text);
//
// var decrypted = enc.Decrypt(encrypted);
//
// Console.WriteLine(Encoding.ASCII.GetString(decrypted));

var i = RabinEncryptor.Gcd(5, 113); 
Console.WriteLine($"{i.Item2} {i.Item3} {i.Item1}");

 Parallel.For(0, 100, i =>
 {
     Console.WriteLine(i);
 });


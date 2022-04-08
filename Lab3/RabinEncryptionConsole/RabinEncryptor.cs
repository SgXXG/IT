using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace RabinEncryptionConsole
{
    public class RabinEncryptor
    {
        private int _qValue;
        private int _pValue;
        private int _nValue;
        private int _bValue;

        public RabinEncryptor(int qValue, int pValue, int bValue)
        {
            if (!IsPrime(qValue))
                throw new ArgumentException("Q должно быть простым");
            if (!IsPrime(pValue))
                throw new ArgumentException("P должно быть простым");

            if (qValue % 4 != 3)
                throw new ArgumentException("Q должно быть сравнимо с 3 по модулю 4");
            if (pValue % 4 != 3)
                throw new ArgumentException("P должно быть сравнимо с 3 по модулю 4");
            if (bValue >= qValue * pValue)
                throw new ArgumentException("B должно быть меньше произведения Q и P");

            _qValue = qValue;
            _pValue = pValue;
            _nValue = qValue * pValue;
            _bValue = bValue;
        }

        public byte[] Encrypt(byte[] data)
        {

            var result = new byte[data.Length * 4];
            var convertedBytes = new byte[4];
            for (var i = 0; i < data.Length; i++)
            {
                var encryptedData = (data[i] * (data[i] + _bValue)) % _nValue;
                convertedBytes = BitConverter.GetBytes(encryptedData);
                Array.Copy(convertedBytes, 0, result, i * 4, 4);
            }

            return result;

        }

        public byte[] Decrypt(byte[] data)
        {
            var (_, yp, yq) = Gcd(_pValue, _qValue);

            var bytesCount = data.Length / 4;

            var d = new int[bytesCount, 4];

            var encryptedData = new int[bytesCount];
            for (var i = 0; i < bytesCount; i++)
                encryptedData[i] = BitConverter.ToInt32(data[(i * 4)..((i + 1) * 4)]);

            for (var i = 0; i < bytesCount; i++)
            {
                var discriminant = (_bValue * _bValue + 4 * encryptedData[i]) % _nValue;
                var mp = Pow(discriminant, (_pValue + 1) / 4, _pValue);
                var mq = Pow(discriminant, (_qValue + 1) / 4, _qValue);

                d[i, 0] = (yp * _pValue * mq + yq * _qValue * mp) % _nValue;
                d[i, 1] = _nValue - d[i, 0];
                d[i, 2] = (yp * _pValue * mq - yq * _qValue * mp) % _nValue;
                d[i, 3] = _nValue - d[i, 2];
            }

            var m = new int[bytesCount][];
            for (var i = 0; i < bytesCount; i++) m[i] = new int[4];
            
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < bytesCount; j++)
                {
                    var temp = d[j, i] - _bValue;
                    if (temp % 2 != 0) temp += _nValue;

                    m[j][i] = (temp / 2) % _nValue;
                }
            }

            return m.Select(column => column.Where(i => i is >= 0 and <= 255).FirstOrDefault(0))
                .Select(i => (byte)i).ToArray();
        }

        private static bool IsPrime(int number)
        {
            if (number == 1) return false;
            if (number == 2) return true;

            var limit = Math.Ceiling(Math.Sqrt(number));

            for (var i = 2; i <= limit; ++i)
                if (number % i == 0)
                    return false;
            return true;
        }

        private static int Pow(int value, int power, int mod)
        {
            int mul = 1;
            for (var i = 0; i < power; i++)
            {
                mul *= value;
                mul %= mod;
            }
            return mul;
        }

        public static (int, int, int) Gcd(int a, int b)
        {
            Console.WriteLine($"{a} {b}");
            if (b == 0)
                return (a, 1, 0);
            else
            {
                var (d, x, y) = Gcd(b, a % b);
                Console.WriteLine($"{x} {y} {d}");
                return (d, y, x - y * (a / b));
            }
        }
    }
}


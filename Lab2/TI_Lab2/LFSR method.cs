using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TI_Lab2
{
    static class LFSR_method
    {
        public const int REGISTER_SIZE = 36;
        static byte[] register = new byte[REGISTER_SIZE];      
        
        private static void FillRegister(string initStr)
        {
            for(int i = 0; i < initStr.Length; i++)
            {
                register[i] = byte.Parse(initStr[i].ToString());
            }   
        }

        public static string TextFromBin(List<byte> bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                string tmp = Convert.ToString(b, 2);
                sb.Append("00000000"[..(8 - tmp.Length)] + tmp);
            }
            return sb.ToString();
        }

        private static byte ShiftReg(byte[] reg)
        {
            byte olderBit = reg[0];
            byte oldThirdBit = reg[25];

            for (int i = 0; i < REGISTER_SIZE - 1; i++)
                reg[i] = reg[i + 1];
            
            reg[REGISTER_SIZE - 1] = (byte)(olderBit^oldThirdBit);

            return olderBit;
        }

        public static List<byte> FormKey(string initStateStr, int length)
        {
            List<byte> key = new List<byte>();
            FillRegister(initStateStr);
            byte[] shiftRegister = register;
            int i = 0;

            while (length > 0)
            {
                key.Add(ShiftReg(shiftRegister));
                byte b = 0;
                int count = 7;
                
                while (length > 0 && count > 0)
                {
                    key[i] = (byte)(key[i] << 1);
                    b = ShiftReg(shiftRegister);
                    key[i] |= b;
                    count--;
                    length--;                   
                }
                i++;
                length--;
            }
            return key;
        }

        public static byte[] BinTextForm(string plaintext)
        {
            byte b = 0;
            int j = 0;
            int binTextSize = plaintext.Length / 8; 
            byte[] binText = new byte[binTextSize];

            for (int i = 0; i < plaintext.Length; i++)
            {
                binText[j] = byte.Parse(plaintext[i].ToString());

                int count = 7;
                while (i < plaintext.Length - 1 && count > 0)
                {
                    binText[j] = (byte)(binText[j] << 1);
                    i++;
                    b = byte.Parse(plaintext[i].ToString());
                    binText[j] |= b;
                    count--;
                }
                j++;
            }
            return binText;
        }

        public static List<byte> Encrypt(byte[] plaintextBin, byte[] keyBin)
        {
            List<byte> resultBin = new List<byte>();

            for(int i = 0; i < plaintextBin.Length; i++)
                resultBin.Add((byte)(plaintextBin[i]^keyBin[i]));
            return resultBin;
        }
    }
}

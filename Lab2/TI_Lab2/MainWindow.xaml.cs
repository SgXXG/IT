using System;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace TI_Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static string _filePath = String.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();      

            if (ofd.ShowDialog() == true)
            {
                _filePath = ofd.FileName;
                byte[] bytes = File.ReadAllBytes(ofd.FileName);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    string tmp = Convert.ToString(b, 2);
                    sb.Append("00000000"[..(8 - tmp.Length)] + tmp);
                }

                Plaintext.Text = sb.ToString();
            }
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            if (Key.Text == String.Empty)
            {
                MessageBox.Show("Ошибка. Необходимо сгенерировать ключ шифрования!");
                return;
            }
            ResultText.Text = LFSR_method.TextFromBin(LFSR_method.Encrypt(
                LFSR_method.BinTextForm(Plaintext.Text), LFSR_method.BinTextForm(Key.Text)));

            string fileName = System.IO.Path.GetDirectoryName(_filePath) + '\\' + "New_" +
                System.IO.Path.GetFileName(_filePath);
            File.WriteAllBytes(fileName, LFSR_method.BinTextForm(ResultText.Text));
        }

        private void GenerateKey_Click(object sender, RoutedEventArgs e)
        {
            if (InitialString.Text == String.Empty)
            {
                MessageBox.Show("Ошибка. Необходимо ввести начальное состояние для регситра!");
                return;
            }

            if (Plaintext.Text == String.Empty)
            {
                MessageBox.Show("Ошибка. Необходимо выбрать файл для шифрования!");
                return;
            }

            foreach(char c in InitialString.Text)
            {
                if (c != '0' && c != '1')
                {
                    MessageBox.Show("Ошибка. Необходимо вводить только 0 или 1");
                    return;
                }                 
            }

            if (InitialString.Text.Length != LFSR_method.REGISTER_SIZE)
            {
                MessageBox.Show("Ошибка. Число разрядов регистра - 25!");
                return;
            }

            Key.Text = LFSR_method.TextFromBin(LFSR_method.FormKey(
                InitialString.Text, Plaintext.Text.Length));          
        }
    }   
}

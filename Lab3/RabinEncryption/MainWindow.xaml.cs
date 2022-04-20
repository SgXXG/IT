using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using RabinEncryptionConsole;

namespace RabinEncryption
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public RabinEncryptor InitializeEncryptor()
        {
            try
            {
                return new RabinEncryptor(int.Parse(QTextBox.Text.Trim()),
                    int.Parse(PTextBox.Text.Trim()), int.Parse(BTextBox.Text.Trim()));
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Параметры должны быть числами");
            }
            return null;
        }

        private void EncryptionButton_OnClick(object sender, RoutedEventArgs e)
        {
            var encryptor = InitializeEncryptor();
            if (encryptor is null) return;

            EncryptedText.Text = string.Empty;
            DecryptedText.Text = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                byte[] data = File.ReadAllBytes(fileName);

                StringBuilder sb = new StringBuilder();
                for (var i = 0; i < (data.Length < 1024 ? data.Length : 1024); i++) 
                    sb.Append($"{data[i]} ");
                EncryptedText.Text = sb.ToString();

                var (result, list) = encryptor.Encrypt(data);

                sb = new StringBuilder();
                foreach (var i in list.Take(1024))
                {
                    sb.Append($"{i} ");
                }
                DecryptedText.Text = sb.ToString();

                string NewFileName = Path.GetDirectoryName(fileName) + "\\" + "Encrypted" + "_" + Path.GetFileName(fileName);
                File.WriteAllBytes(NewFileName, result);
            }

        }

        private void DecryptionButton_OnClick(object sender, RoutedEventArgs e)
        {
            var encryptor = InitializeEncryptor();
            if (encryptor is null) return;

            EncryptedText.Text = string.Empty;
            DecryptedText.Text = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                byte[] data = File.ReadAllBytes(fileName);

                StringBuilder sb = new StringBuilder();
                var list = new List<int>();
                for (var i = 0; i < data.Length / 4; i++)
                    list.Add(BitConverter.ToInt32(data[(i * 4)..((i + 1) * 4)]));

                foreach (var i in list.Take(1024))
                {
                    sb.Append($"{i} ");
                }

                DecryptedText.Text = sb.ToString();

                var result = encryptor.Decrypt(data);

                sb = new StringBuilder();

                foreach (var bytes in result.Take(1024))
                {
                    for (var i = 0; i < bytes.Length; i++)
                    {
                        if (i > 0)
                            sb.Append('/');
                        sb.Append(bytes[i]);
                    }
                    sb.Append(' ');
                }

                EncryptedText.Text = sb.ToString();

                string NewFileName = Path.GetDirectoryName(fileName) + "\\" + "Decrypted" + "_" + Path.GetFileName(fileName);
                File.WriteAllBytes(NewFileName, result.Select(i => i.FirstOrDefault()).ToArray());
            }

        }
    }
}

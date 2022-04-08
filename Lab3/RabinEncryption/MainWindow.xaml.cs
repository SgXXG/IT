using System;
using System.IO;
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

                var result = encryptor.Encrypt(data);

                sb = new StringBuilder();
                for (var i = 0; i < (result.Length < 1024 * 4 ? result.Length : 1024 * 4); i++)
                    sb.Append($"{result[i]} ");
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
                for (var i = 0; i < (data.Length < 1024 * 4 ? data.Length : 1024 * 4); i++) 
                    sb.Append($"{data[i]} ");
                DecryptedText.Text = sb.ToString();

                var result = encryptor.Decrypt(data);

                sb = new StringBuilder();
                for (var i = 0; i < (result.Length < 1024 ? result.Length : 1024); i++)
                    sb.Append($"{result[i]} ");
                EncryptedText.Text = sb.ToString();

                string NewFileName = Path.GetDirectoryName(fileName) + "\\" + "Decrypted" + "_" + Path.GetFileName(fileName);
                File.WriteAllBytes(NewFileName, result);
            }
        }
    }
}

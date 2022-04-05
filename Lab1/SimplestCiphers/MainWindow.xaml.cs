using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using SimplestCiphers.Ciphers;

namespace SimplestCiphers
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

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (CiphersComboBox.Text == String.Empty)
            {
                MessageBox.Show("Не выбран метод шифрования!");
                return;
            }

            if (CiphersComboBox.Text == "Метод железнодорожной изгороди(En)")
            {
                if (EncodingKeyTextBox.Text == "")
                {
                    MessageBox.Show("Не задан ключ шифрования!");
                    return;
                }

                int key;
                if (!int.TryParse(EncodingKeyTextBox.Text, out key) || key < 1)
                {
                    MessageBox.Show("Неверный ключ шифрования!");
                    return;
                }

                if (btn.Tag.ToString() == "Encode")
                {
                    DecodingTextBox.Text = RailFenceCipher.Encode(EncodingTextBox.Text, key);
                }
                else
                {
                    EncodingTextBox.Text = RailFenceCipher.Decode(DecodingTextBox.Text, key);
                }
            }

            if (CiphersComboBox.Text == "Метод Плейфейра(En)")
            {
                string text = EncodingTextBox.Text;
                text = text.ToUpper();
                text = PlayfairCipher.ClearText(text, "En");

                if ((text[text.Length - 1] == 'X') && (text.Length % 2 != 0))
                {
                    MessageBox.Show("Ошибка в исходном тексте!");
                    return;
                }

                for (int i = 1; i < text.Length; i += 2)
                {
                    if (text[i] == 'X' && text[i - 1] == 'X')
                    {
                        MessageBox.Show("Ошибка в исходном тексте. Две буквы X подряд");
                        return;
                    }
                }

                if (btn.Tag.ToString() == "Encode")
                {
                    DecodingTextBox.Text = PlayfairCipher.Encode(EncodingTextBox.Text);
                }
                else
                {
                    EncodingTextBox.Text = PlayfairCipher.Decode(DecodingTextBox.Text);
                }
            }

            if (CiphersComboBox.Text == "Метод Виженера с прогрессивным ключем(Ru)")
            {

                Language language = Ciphers.Language.RuLang;
                string key = StringScaner.GetDesiredString(EncodingKeyTextBox.Text, language);

                if (key == "")
                {
                    MessageBox.Show("Ключ шифрования не может быть пустым");
                    return;
                }

                if (btn.Tag.ToString() == "Encode")
                {
                    DecodingTextBox.Text = VigenereCipher.Encode(EncodingTextBox.Text, EncodingKeyTextBox.Text);
                }
                else
                {
                    EncodingTextBox.Text = VigenereCipher.Decode(DecodingTextBox.Text, EncodingKeyTextBox.Text);
                }
            }
        }

        private void FileOpenButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;

                if (btn.Tag.ToString() == "Encode")
                {
                    EncodingTextBox.Text = File.ReadAllText(fileName);
                }
                else
                {
                    DecodingTextBox.Text = File.ReadAllText(fileName);
                }

                ButtonClick(sender, e);

                string NewFileName = Path.GetDirectoryName(fileName) + "\\" +
                                     btn.Tag.ToString() + "_" + Path.GetFileName(fileName);

                FileStream NewFile = File.OpenWrite(NewFileName);
                StreamWriter NewStreamWriter = new StreamWriter(NewFile);

                if (btn.Tag.ToString() == "Encode")
                {
                    NewStreamWriter.Write(DecodingTextBox.Text);
                }
                else
                {
                    NewStreamWriter.Write(EncodingTextBox.Text);
                }

                NewStreamWriter.Close();

            }
        }
    }
}
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
                new NewMessageBox("Не выбран метод шифрования!", MessageType.Confirmation, MessageButtons.Ok).ShowDialog();
                return;
            }

            if (CiphersComboBox.Text == "Метод железнодорожной изгороди(Ru)")
            {
                if (EncodingKeyTextBox.Text == "")
                {
                    new NewMessageBox("Не задан ключ шифрования!", MessageType.Confirmation, MessageButtons.Ok).ShowDialog();
                    return;
                }

                int key;
                if (!int.TryParse(EncodingKeyTextBox.Text, out key))
                {
                    new NewMessageBox("Неверный ключ шифрования!", MessageType.Confirmation, MessageButtons.Ok).ShowDialog();
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

            if (CiphersComboBox.Text == "Метод поворачивающейся решетки(En)")
            {
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
                if (EncodingKeyTextBox.Text == "")
                {
                    new NewMessageBox("Не задан ключ шифрования!", MessageType.Confirmation, MessageButtons.Ok).ShowDialog();
                    return;
                }

                foreach (char c in EncodingKeyTextBox.Text)
                {
                    if (!(c is >= 'а' and <= 'я' or >= 'А' and <= 'Я'))
                    {
                        new NewMessageBox("Неверный ключ шифрования!", MessageType.Confirmation, MessageButtons.Ok).ShowDialog();
                        return;
                    }
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
                if (btn.Tag.ToString() == "Encode")
                {
                    EncodingTextBox.Text = File.ReadAllText(openFileDialog.FileName);
                }
                else
                {
                    DecodingTextBox.Text = File.ReadAllText(openFileDialog.FileName);
                }
                ButtonClick(sender, e);
            }
        }
    }
}
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Trivium.Models;
using Trivium.ViewModels;

namespace Trivium.Views
{
    /// <summary>
    /// Interaction logic for EncryptionView.xaml
    /// </summary>
    public partial class EncryptionView : UserControl
    {
        private Encryptor2 encryptor;

        public EncryptionView()
        {
            InitializeComponent();
            if (EncryptionViewModel is not null)
                EncryptionViewModel.PropertyChanged += SubscribeChanges;
        }

        public EncryptionViewModel EncryptionViewModel
        {
            get
            {
                return this.DataContext as EncryptionViewModel;
            }
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            var encyptedText = EncryptionViewModel.Encryptor.Encrypt(EncryptionViewModel.Text);
            EncryptionViewModel.EncryptedText = encyptedText;
            this.EncryptedTextBlock.Text = encyptedText;
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(EncryptionViewModel.Text) || string.IsNullOrEmpty(EncryptionViewModel.KeyVaue))
                return;

            var dialog = new SaveFileDialog();
            dialog.Filter = "Text|*.txt|All|*.*";
            if (dialog.ShowDialog() == true)
            {
                var path = dialog.FileName;
                var decryptor = new Decryptor(EncryptionViewModel.Encryptor);
                var bruteForce = new BruteForce(EncryptionViewModel.Text, EncryptionViewModel.EncryptedText, decryptor);
                using var stream = new StreamWriter(path, append: true);
                foreach (var attachResult in bruteForce.Atack())
                {
                    var msg = attachResult.ToString();
                    stream.WriteLine(msg);
                    stream.Flush();
                    this.EncryptedTextBlock.Text = msg;
                }
            }
        }

        private void SubscribeChanges(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "KeyLength")
                TruncateKeyValueToKeyLength();
        }

        private void TruncateKeyValueToKeyLength()
        {
            var maxCharCount = (int)EncryptionViewModel.KeyLength / 4;
            var charCount = Math.Min(EncryptionViewModel.KeyVaue.Length, maxCharCount);
            EncryptionViewModel.KeyVaue = EncryptionViewModel.KeyVaue.Substring(0, charCount);
            this.KeyValueBox.Text = EncryptionViewModel.KeyVaue;
        }
    }
}
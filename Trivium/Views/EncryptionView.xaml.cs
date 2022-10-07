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
            this.DataContextChanged += Init;
        }

        private void Init(object? sender, DependencyPropertyChangedEventArgs e)
        {
            if (EncryptionViewModel is not null)
                EncryptionViewModel.PropertyChanged += SubscribeChanges;
            ManageButtonActivation();
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

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            var decryptor = new Decryptor(EncryptionViewModel.Encryptor);
            var decyptedText = decryptor.Decrypt(EncryptionViewModel.EncryptedText);
            this.EncryptedTextBlock.Text = decyptedText;
        }

        private void SubscribeChanges(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "KeyLength")
                TruncateKeyValueToKeyLength();
            ManageButtonActivation();
        }

        private void TruncateKeyValueToKeyLength()
        {
            var maxCharCount = (int)EncryptionViewModel.KeyLength / 4;
            var charCount = Math.Min(EncryptionViewModel.KeyVaue.Length, maxCharCount);
            EncryptionViewModel.KeyVaue = EncryptionViewModel.KeyVaue.Substring(0, charCount);
            this.KeyValueBox.Text = EncryptionViewModel.KeyVaue;
        }

        private void ManageButtonActivation()
        {
            this.EncryptButton.IsEnabled = EncryptionViewModel is not null && !string.IsNullOrEmpty(EncryptionViewModel.KeyVaue) && !string.IsNullOrEmpty(EncryptionViewModel.Text);
            this.DecryptButton.IsEnabled = EncryptionViewModel is not null && !string.IsNullOrEmpty(EncryptionViewModel.EncryptedText);
        }
    }
}
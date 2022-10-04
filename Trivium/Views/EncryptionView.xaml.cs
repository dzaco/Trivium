using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public EncryptionView()
        {
            this.DataContext = this.EncryptionViewModel;
            InitializeComponent();
            EncryptionViewModel.PropertyChanged += SubscribeChanges;
        }

        public EncryptionViewModel EncryptionViewModel
        {
            get
            {
                return GetValue(EncryptionVMProperty) as EncryptionViewModel;
            }
            set
            {
                SetValue(EncryptionVMProperty, value);
            }
        }

        public static readonly DependencyProperty EncryptionVMProperty =
        DependencyProperty.RegisterAttached(
            name: "EncryptionViewModel",
            propertyType: typeof(EncryptionViewModel),
            ownerType: typeof(EncryptionView),
            defaultMetadata: new PropertyMetadata(new EncryptionViewModel()));
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
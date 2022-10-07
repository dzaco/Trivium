using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AtackView.xaml
    /// </summary>
    public partial class AtackView : UserControl
    {
        public AtackView()
        {
            InitializeComponent();
        }

        public EncryptionViewModel EncryptionViewModel
        {
            get
            {
                return this.DataContext as EncryptionViewModel;
            }
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            AttackClickMock();
            return;
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
                    this.AttackLogGrid.Items.Add(msg);
                }
            }
        }

        private void AttackClickMock()
        {
            var id = 1;

            while (id < 10)
            {
                var attackResult = new AttackResult()
                {
                    Id = id.ToString(),
                    DecryptedText = "mock"
                };
                this.AttackLogGrid.Items.Add(attackResult.ToString());
                id++;
            }
        }
    }
}
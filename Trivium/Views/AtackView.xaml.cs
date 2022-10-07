using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using System.Windows.Threading;
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

        public BruteForceViewModel BruteForceViewModel
        {
            get
            {
                return this.DataContext as BruteForceViewModel;
            }
        }

        public EncryptionViewModel EncryptionViewModel => BruteForceViewModel.EncryptionViewModel;

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            this.BruteForceViewModel.AttackLogs.CollectionChanged += Refresh;
            //foreach (var res in BruteForceViewModel.AttackClickMock().ToList())
            //{
            //}

            //return;

            if (string.IsNullOrEmpty(EncryptionViewModel.Text) || string.IsNullOrEmpty(EncryptionViewModel.KeyVaue))
                return;

            var dialog = new SaveFileDialog();
            dialog.Filter = "Text|*.txt|All|*.*";
            if (dialog.ShowDialog() == true)
            {
                var path = dialog.FileName;
                using var stream = new StreamWriter(path, append: true);
                foreach (var attachResult in BruteForceViewModel.Atack())
                {
                    stream.WriteLine(attachResult.ToString());
                    stream.Flush();
                }
            }
        }

        private void Refresh(object? sender, NotifyCollectionChangedEventArgs e)
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(() =>
            {
                this.AttackLogGrid.Items.Refresh();
                this.AttackLogGrid.ItemsSource = this.BruteForceViewModel.AttackLogs;
            });
        }
    }
}
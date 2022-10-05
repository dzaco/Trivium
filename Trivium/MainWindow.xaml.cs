using System;
using System.Collections.Generic;
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
using Trivium.Views;

namespace Trivium
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public EncryptionViewModel EncryptionViewModel;
        private BruteForceViewModel bruteForceViewModel;

        public MainWindow()
        {
            CreateModels();
            this.DataContext = this;
            InitializeComponent();
            this.EncryptionView.DataContext = this.EncryptionViewModel;
        }

        private void CreateModels()
        {
            this.EncryptionViewModel = new EncryptionViewModel();
            this.bruteForceViewModel = new BruteForceViewModel();
        }
    }
}
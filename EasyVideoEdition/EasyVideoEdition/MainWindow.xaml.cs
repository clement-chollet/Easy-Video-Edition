using EasyVideoEdition.View;
using EasyVideoEdition.ViewModel;
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

namespace EasyVideoEdition
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainViewModel mainVM = MainViewModel.INSTANCE;

            this.DataContext = mainVM;
            foreach (TabItem t in tabControl.Items)
            {
                t.Width = this.ActualWidth / 8.3;
            }

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (TabItem t in tabControl.Items)
            {
                t.Width = this.ActualWidth / 8.3;
            }
        }
    }
}

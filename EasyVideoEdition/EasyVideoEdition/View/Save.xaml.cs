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

namespace EasyVideoEdition.View
{
    /// <summary>
    /// Logique d'interaction pour Save.xaml
    /// </summary>
    public partial class Save : UserControl
    {
        public Save()
        {
            InitializeComponent();
        }

        // Action when the window is resized
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Save/Export project buttons and progress bar resizing
            SaveProject.Width = this.ActualWidth / 2;
            ExportProject.Width = this.ActualWidth / 2;
            Progressbar.Width = this.ActualWidth / 2.5;
        }
    }
}

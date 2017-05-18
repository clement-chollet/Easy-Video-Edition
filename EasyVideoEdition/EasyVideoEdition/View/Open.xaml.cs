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
    /// Logique d'interaction pour Open.xaml
    /// </summary>
    public partial class Open : UserControl
    {
        public Open()
        {
            InitializeComponent();
            
        }

        // Actions when the window is resized
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // New/Open project buttons resizing
            NewProject.Width = this.ActualWidth / 2;
            OpenProject.Width = this.ActualWidth / 2;
        }
    }
}

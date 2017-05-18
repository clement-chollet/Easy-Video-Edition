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
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            /*
            // Subtitles tab text changing
            if (subtitles_adding.Width < 150)
            {
                subtitles_adding.Header = "6. Sous titres";
            }
            else
            {
                subtitles_adding.Header = "6. Ajouter sous titres";
            }

            // Chapters tab text changing
            if (chapters_adding.Width < 150)
            {
                chapters_adding.Header = "7. Chapitres";
            }
            else
            {
                chapters_adding.Header = "7. Ajouter chapitres";
            }
            */
        }
    }
}

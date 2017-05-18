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
using System.Windows.Media.Animation;


namespace EasyVideoEdition.View
{
    /// <summary>
    /// Logique d'interaction pour BasicUserControl.xaml
    /// </summary>
    public partial class BasicUserControlChapters : System.Windows.Controls.UserControl
    {
        private double valTrLib = 3;
        private double valTrOng = 7;

        public BasicUserControlChapters()
        {
            InitializeComponent();
        }

        // Actions when the window is resized
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double contenerSize = 800;
            Window contener = MainWindow.GetWindow(this);

            //Instructions area resizing
            instructions.Height = this.ActualHeight / 1.5;
            instructions.Width = this.ActualWidth / 2;

            //Add chapter tab resizing
            addChapterTab.Width = this.ActualWidth / 5;

            //Delete chapter tab resizing
            deleteChapterTab.Width = this.ActualWidth / 5;

            //Library area resizing
            library.Height = this.ActualHeight / 1.5;
            library.Width = this.ActualWidth / valTrLib;

            //Chapter library tab resizing
            myChaptertab.Height = this.ActualHeight / 20;
            myChaptertab.Width = this.ActualWidth / valTrOng;

            //instructionButton resizing
            instructionsButton.Height = 40;

            //Instructions texts setters
            addChapterInstrText.Text = EasyVideoEdition.Properties.Resources.addChapterInstrText;
            delChapterInstrText.Text = EasyVideoEdition.Properties.Resources.delChapterInstrText;

            //To get the width of the parent window, to resize tabs and library
            if (contener != null)
            {
                contenerSize = contener.ActualWidth;
            }

            //To adjust the size of tabs
            if ((contenerSize >= 1600) && (!instructionTab.IsVisible))
            {
                valTrLib = 1.5;
                valTrOng = 6.5;
                myChaptertab.Width = this.ActualWidth / valTrOng;
                library.Width = this.ActualWidth / valTrLib;
            }
            else if ((contenerSize < 1600) && (!instructionTab.IsVisible))
            {
                valTrLib = 1.5;
                valTrOng = 4.5;
                myChaptertab.Width = this.ActualWidth / valTrOng;
                library.Width = this.ActualWidth / valTrLib;
            }

            //To hide the instructions panel when the window is too small
            if ((contenerSize < 980) && (instructionTab.IsVisible))
            {
                Storyboard hide = this.FindResource("HideInformationPanel") as Storyboard;
                ControlTemplate t = this.FindResource("instructionsButtonHiddenTemplate") as ControlTemplate;
                hide.Begin();
                valTrLib = 1.5;
                valTrOng = 5;
                myChaptertab.Width = this.ActualWidth / valTrOng;
                library.Width = this.ActualWidth / valTrLib;
                instructionsButton.Height = 40;
                instructionsButton.Template = t;
            }

        }

        //Action when the instruction button is clicked
        private void instructionsButton_Click(object sender, RoutedEventArgs e)
        {
            if (instructionTab.IsVisible)
            {
                Storyboard hide = this.FindResource("HideInformationPanel") as Storyboard;
                ControlTemplate t = this.FindResource("instructionsButtonHiddenTemplate") as ControlTemplate;
                hide.Begin();
                valTrLib = 1.5;

                if (MainWindow.GetWindow(this).ActualWidth >= 1600)
                    valTrOng = 6.5;
                else
                    valTrOng = 4.5;

                myChaptertab.Width = this.ActualWidth / valTrOng;
                library.Width = this.ActualWidth / valTrLib;
                instructionsButton.Height = 40;
                instructionsButton.Template = t;
            }

            else
            {
                if (MainWindow.GetWindow(this).ActualWidth < 980)
                {
                    //Print the massage box when the window is too small to see the instructions part
                    MessageBox.Show(Application.Current.MainWindow, "La fenêtre est trop petite pour afficher les instructions. \nVeuillez agrandir la fenêtre pour les voir.", "Affichage des instructions", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Cancel);
                }
                else
                {
                    Storyboard show = this.FindResource("ShowInformationPanel") as Storyboard;
                    ControlTemplate t = this.FindResource("instructionsButtonShownTemplate") as ControlTemplate;
                    show.Begin();
                    valTrLib = 3;
                    valTrOng = 7;
                    myChaptertab.Width = this.ActualWidth / valTrOng;
                    library.Width = this.ActualWidth / valTrLib;
                    instructionsButton.Height = 40;
                    instructionsButton.Template = t;
                }
            }
        }
    }
}

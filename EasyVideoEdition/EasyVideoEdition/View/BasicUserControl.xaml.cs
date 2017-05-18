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
    public partial class BasicUserControl : System.Windows.Controls.UserControl
    {
        private double valTrLib = 3;
        private double valTrOng = 7;
        private double valSizeMinToHide = 1100;

        public BasicUserControl()
        {
            InitializeComponent();
        }


        //Actions when the window is resized
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double contenerSize = 800;
            Window contener = MainWindow.GetWindow(this);
            //Instructions area resizing
            instructions.Height = this.ActualHeight / 1.5;
            instructions.Width = this.ActualWidth / 2;

            //RecInstruction tab resizing
            recInstruction.Width = this.ActualWidth / 8;

            //addVidInstruction tab resizing
            addVidInstruction.Width = this.ActualWidth / 7;

            //addPicInstruction tab resizing
            addPicInstruction.Width = this.ActualWidth / 7;

            //Library area resizing
            library.Height = this.ActualHeight / 1.5;
            library.Width = this.ActualWidth / valTrLib;

            //videoTab library resizing
            hiddenTab.Height = this.ActualHeight / 20;
            videoTab.Width = this.ActualWidth / valTrOng;

            //imageTab library resizing
            imageTab.Width = this.ActualWidth / valTrOng;

            //instructionButton resizing
            instructionsButton.Height = 40;

            //Instructions texts setters
            vidInstrText.Text = EasyVideoEdition.Properties.Resources.addVisualInstrVid;
            recInstrText.Text = EasyVideoEdition.Properties.Resources.addVisualInstrRec;
            picInstrText.Text = EasyVideoEdition.Properties.Resources.addVisualInstrPic;

            //To get the width of the parent window, to resize tabs and library
            if (contener != null)
            {
                contenerSize = contener.ActualWidth;
            }

            //To adjust the size of tabs
            if ((contenerSize >= 1600)&& (!instructionTab.IsVisible))
            {
                //Margin of library panel
                library.Margin = new Thickness(-20, 0, 0, 0);

                //Size of tabs
                valTrLib = 1.5;
                valTrOng = 6.5;
                videoTab.Width = this.ActualWidth / valTrOng;
                imageTab.Width = this.ActualWidth / valTrOng;
                library.Width = this.ActualWidth / valTrLib;
            }
            else if((contenerSize < 1600) && (!instructionTab.IsVisible))
            {
                //Margin of library panel
                library.Margin = new Thickness(-20, 0, 0, 0);

                //Size of tabs
                valTrLib = 1.5;
                valTrOng = 6.5;
                videoTab.Width = this.ActualWidth / valTrOng;
                imageTab.Width = this.ActualWidth / valTrOng;
                library.Width = this.ActualWidth / valTrLib;
            }

            //To hide the instructions panel when the window is too small
            else if ((contenerSize < valSizeMinToHide) && (instructionTab.IsVisible))
            {
                //Change the margin of the library panel
                library.Margin = new Thickness(-20, 0, 0, 0);

                //Hide the panel
                Storyboard hide = this.FindResource("HideInformationPanel") as Storyboard;
                ControlTemplate t = this.FindResource("instructionsButtonHiddenTemplate") as ControlTemplate;
                hide.Begin();
                valTrLib = 1.5;
                valTrOng = 6.5;
                instructionsButton.Height = 40;
                videoTab.Width = this.ActualWidth / valTrOng;
                imageTab.Width = this.ActualWidth / valTrOng;
                library.Width = this.ActualWidth / valTrLib;
                
                instructionsButton.Template = t;
            }
            else
            {
                library.Margin = new Thickness(30, 0, 0, 0);
            }

        }

        // Action when the instruction button is clicked
        private void instructionsButton_Click(object sender, RoutedEventArgs e)
        {
            if (instructionTab.IsVisible)
            {
                Storyboard hide = this.FindResource("HideInformationPanel") as Storyboard;
                ControlTemplate t = this.FindResource("instructionsButtonHiddenTemplate") as ControlTemplate;
                hide.Begin();

                //Change tabs size
                valTrLib = 1.5;

                if (MainWindow.GetWindow(this).ActualWidth >= 1600)
                    valTrOng = 6.5;
                else
                    valTrOng = 4.5;
                
                videoTab.Width = this.ActualWidth / valTrOng;
                imageTab.Width = this.ActualWidth / valTrOng;
                library.Width = this.ActualWidth / valTrLib;
                instructionsButton.Height = 40;
                instructionsButton.Template = t;
            }

            else
            {
                if(MainWindow.GetWindow(this).ActualWidth < valSizeMinToHide)
                {
                    //Print the massage box when the window is too small to see the instructions part
                    MessageBox.Show(Application.Current.MainWindow, "La fenêtre est trop petite pour afficher les instructions. \nVeuillez agrandir la fenêtre pour les voir.", "Affichage des instructions", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Cancel);
                }
                else
                {
                    //Change the margin of the library panel
                    library.Margin = new Thickness(30, 0, 0, 0);

                    //Show the instructions panel
                    Storyboard show = this.FindResource("ShowInformationPanel") as Storyboard;
                    ControlTemplate t = this.FindResource("instructionsButtonShownTemplate") as ControlTemplate;
                    show.Begin();

                    //Change tabs size
                    valTrLib = 3;
                    valTrOng = 7;
                    videoTab.Width = this.ActualWidth / valTrOng;
                    imageTab.Width = this.ActualWidth / valTrOng;
                    library.Width = this.ActualWidth / valTrLib;
                    instructionsButton.Height = 40;
                    instructionsButton.Template = t;
                }
            }
        }
    }
}

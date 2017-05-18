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
    /// Logique d'interaction pour BasicUserControlVisualEdition.xaml
    /// </summary>
    public partial class BasicUserControlVisualEdition : UserControl
    {
        private double valTrLib = 3;
        private double valTrOng = 7;

        public BasicUserControlVisualEdition()
        {
            InitializeComponent();
        }

        // Action when the window is resized
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double contenerSize = 800;
            Window contener = MainWindow.GetWindow(this);
            //Instructions area resizing
            instructions.Height = this.ActualHeight / 1.5;
            instructions.Width = this.ActualWidth / 2;

            //Delete bloc instruction tab resizing
            delBlocInstruction.Width = this.ActualWidth / 5;

            //Delete sound instruction tab resizing
            delSoundInstruction.Width = this.ActualWidth / 5;

            //instructionButton resizing
            instructionsButton.Height = 40;

            //Instructions texts setters
            delBlocInstrText.Text = EasyVideoEdition.Properties.Resources.editVisualInstrCut;
            delSoundInstrText.Text = EasyVideoEdition.Properties.Resources.editVisualInstrMute;

            if (contener != null)
            {
                contenerSize = contener.ActualWidth;
            }
            //To hide the instructions panel when the window is too small
            if ((contenerSize < 980) && (instructionTab.IsVisible))
            {
                Storyboard hide = this.FindResource("HideInformationPanel") as Storyboard;
                ControlTemplate t = this.FindResource("instructionsButtonHiddenTemplate") as ControlTemplate;
                hide.Begin();
                valTrLib = 1.5;
                valTrOng = 5.5;
                instructionsButton.Height = 40;
                instructionsButton.Template = t;
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
                valTrLib = 1.5;

                if (MainWindow.GetWindow(this).ActualWidth >= 1600)
                    valTrOng = 6.5;
                else
                    valTrOng = 4.5;

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
                    instructionsButton.Height = 40;
                    instructionsButton.Template = t;
                }
            }
        }
    }
}

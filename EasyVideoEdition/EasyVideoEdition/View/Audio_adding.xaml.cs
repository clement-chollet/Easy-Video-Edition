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
    /// Logique d'interaction pour Audio_adding.xaml
    /// </summary>
    public partial class Audio_adding : UserControl
    {
        public Audio_adding()
        {
            InitializeComponent();
        }
        /// <summary>
        ///  Actions when the window is resized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Variables for button bottom colored line resizing
            //Blue line resizing (delBlocButton)
            var blueButtonTemplate = recButton.Template;
            Rectangle blueOuterRec = (Rectangle)blueButtonTemplate.FindName("outerRectangle", recButton);


            //Red line resizing (delSoundButton)
            var redButtonTemplate = addAudioButton.Template;
            Rectangle redOuterRec = (Rectangle)redButtonTemplate.FindName("outerRectangle", addAudioButton);

            //GroupButtons
            //Blue button resizing (record button)
            recButton.Height = this.ActualHeight / 15;
            blueOuterRec.Height = this.ActualHeight / 120;
            recButton.Width = this.ActualWidth / 5;
            recButton.FontSize = this.ActualHeight / 100;

            //Red Button resizing ( add audio button)
            addAudioButton.Height = this.ActualHeight / 15;
            redOuterRec.Height = this.ActualHeight / 120;
            addAudioButton.Width = this.ActualWidth / 5;

            //slider resizing
            sliderMusic.Width = this.ActualWidth / 1.2;
            sliderSpeech.Width = this.ActualWidth / 1.2;

            //UserControl instructionsLib resizing
            instructionsLib.Width = grid1.ColumnDefinitions[2].ActualWidth + grid1.ColumnDefinitions[3].ActualWidth + grid1.ColumnDefinitions[4].ActualWidth + grid1.ColumnDefinitions[5].ActualWidth;
            instructionsLib.Height = grid1.RowDefinitions[0].ActualHeight;

            //MediaEl resizing -> to keep 16/9 as dimensions
            if (((double)grid1.RowDefinitions[0].ActualHeight / (double)(grid1.ColumnDefinitions[0].ActualWidth + grid1.ColumnDefinitions[1].ActualWidth)) > (double)9 / (double)16)
            {
                MediaEl.Width = (grid1.ColumnDefinitions[0].ActualWidth + grid1.ColumnDefinitions[1].ActualWidth);
                MediaEl.Height = ((double)9 / (double)16 * (double)MediaEl.Width) - 30;
                MediaEl.Width -= 30;
            }
            else
            {
                MediaEl.Height = grid1.RowDefinitions[0].ActualHeight - 170;
                MediaEl.Width = ((double)16 / (double)9 * (double)MediaEl.Height) - 30;
                MediaEl.Height -= 30;
            }

            // Change text of blue Button (record button)
            if (recButton.Width < 225)
            {
                recButton.Content = "S'enr...";
            }
            else
            {
                recButton.Content = "S'enregistrer";
            }

            // Change text of red Button (add audio button)
            if (addAudioButton.Width < 225)
            {
                addAudioButton.Content = "Ajouter";
            }
           
            if (addAudioButton.Width > 225)
            {
                addAudioButton.Content = "Ajouter son";
            }

        }

        // Action when the blue button (record button) is clicked
        private void blueButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.Convert.ToString(grid1.ColumnDefinitions[2].ActualWidth));
        }

        // Action when the red button (add audio button) is clicked
        private void redButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.Convert.ToString(grid1.ColumnDefinitions[2].ActualWidth));
        }
    }
}


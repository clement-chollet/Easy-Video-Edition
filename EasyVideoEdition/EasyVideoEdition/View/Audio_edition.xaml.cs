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
    /// Logique d'interaction pour Audio_edition.xaml
    /// </summary>
    public partial class Audio_edition : UserControl
    {
        public Audio_edition()
        {
            InitializeComponent();
        }

        // Actions when the window is resized
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Variables for button bottom colored line resizing
            //Blue line resizing (Delete bloc button)
            var blueButtonTemplate = delSpeechButton.Template;
            Rectangle blueOuterRec = (Rectangle)blueButtonTemplate.FindName("outerRectangle", delSpeechButton);


            //Red line resizing (delete sound button)
            var redButtonTemplate = delMusicButton.Template;
            Rectangle redOuterRec = (Rectangle)redButtonTemplate.FindName("outerRectangle", delMusicButton);

            //GroupButtons
            //Blue button resizing (Delete bloc button)
            delSpeechButton.Height = this.ActualHeight / 15;
            blueOuterRec.Height = this.ActualHeight / 120;
            delSpeechButton.Width = this.ActualWidth / 5;
            delSpeechButton.FontSize = this.ActualHeight / 100;

            //Red Button resizing (delete sound button)
            delMusicButton.Height = this.ActualHeight / 15;
            redOuterRec.Height = this.ActualHeight / 120;
            delMusicButton.Width = this.ActualWidth / 5;

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

            // Change text of blue Button (Delete bloc button)
            if (delSpeechButton.Width < 225)
            {
                delSpeechButton.Content = "[...] parole";
            }
            else
            {
                delSpeechButton.Content = "Supprimer parole";
            }

            // Change text of red Button (delete sound button)
            if (delMusicButton.Width < 225)
            {
                delMusicButton.Content = "[...] musique";
            }

            if (delMusicButton.Width > 225)
            {
                delMusicButton.Content = "Supprimer musique";
            }
        }

        // Action when the Delete bloc button is clicked
        private void blueButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.Convert.ToString(grid1.ColumnDefinitions[2].ActualWidth));
        }

        // Action when the Delete sound button is clicked
        private void redButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.Convert.ToString(grid1.ColumnDefinitions[2].ActualWidth));
        }
    }

}


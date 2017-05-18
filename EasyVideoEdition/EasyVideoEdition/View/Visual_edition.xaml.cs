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
    /// Logique d'interaction pour Visual_edition.xaml
    /// </summary>
    public partial class Visual_edition : UserControl
    {
        public Visual_edition()
        {
            InitializeComponent();
        }

        // Actions when the window is resized
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Variables for button bottom colored line resizing
            //Blue line resizing (delete bloc)
            var blueButtonTemplate = delBlocButton.Template;
            Rectangle blueOuterRec = (Rectangle)blueButtonTemplate.FindName("outerRectangle", delBlocButton);


            //Red line resizing (delete sound)
            var redButtonTemplate = delSoundButton.Template;
            Rectangle redOuterRec = (Rectangle)redButtonTemplate.FindName("outerRectangle", delSoundButton);

            //GroupButtons
            //Blue button resizing (delete bloc)
            delBlocButton.Height = this.ActualHeight / 15;
            blueOuterRec.Height = this.ActualHeight / 120;
            delBlocButton.Width = this.ActualWidth / 5;
            delBlocButton.FontSize = this.ActualHeight / 100;

            //Red Button resizing (delete sound)
            delSoundButton.Height = this.ActualHeight / 15;
            redOuterRec.Height = this.ActualHeight / 120;
            delSoundButton.Width = this.ActualWidth / 5;

            //slider resizing
            slider.Width = this.ActualWidth / 1.2;

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

            // Change text of blue Button
            if (delBlocButton.Width < 225)
            {
                delBlocButton.Content = "Supprimer";
            }
            else
            {
                delBlocButton.Content = "Supprimer bloc";
            }

            // Change text of red Button
            if (delSoundButton.Width < 225)
            {
                delSoundButton.Content = "Couper";
            }
            if (delSoundButton.Width > 225)
            {
                delSoundButton.Content = "Supprimer son";
            }
        }


        // Action when delete bloc button is clicked
        private void blueButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.Convert.ToString(grid1.ColumnDefinitions[2].ActualWidth));
        }
        // Action when delete sound button is clicked
        private void redButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.Convert.ToString(grid1.ColumnDefinitions[2].ActualWidth));
        }
    }

}

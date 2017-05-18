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
    /// Logique d'interaction pour Subtitles_adding.xaml
    /// </summary>
    public partial class Subtitles_adding : UserControl
    {
        public Subtitles_adding()
        {
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Variables for button bottom colored line resizing
            //Blue line resizing (Add subtitle button)
            var blueButtonTemplate = addSubButton.Template;
            Rectangle blueOuterRec = (Rectangle)blueButtonTemplate.FindName("outerRectangle", addSubButton);


            //Red line resizing (Delete subtitle button)
            var redButtonTemplate = delSubButton.Template;
            Rectangle redOuterRec = (Rectangle)redButtonTemplate.FindName("outerRectangle", delSubButton);

            //GroupButtons
            //Blue button resizing (add subtitle)
            addSubButton.Height = this.ActualHeight / 15;
            blueOuterRec.Height = this.ActualHeight / 120;
            addSubButton.Width = this.ActualWidth / 5;
            addSubButton.FontSize = this.ActualHeight / 100;

            //Red Button resizing (delete subtitle)
            delSubButton.Height = this.ActualHeight / 15;
            redOuterRec.Height = this.ActualHeight / 120;
            delSubButton.Width = this.ActualWidth / 5;

            //Slider resizing
            slider.Width = this.ActualWidth / 1.2;

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

            //UserControl instructionsLib resizing
            instructionsLib.Width = grid1.ColumnDefinitions[2].ActualWidth + grid1.ColumnDefinitions[3].ActualWidth + grid1.ColumnDefinitions[4].ActualWidth + grid1.ColumnDefinitions[5].ActualWidth;
            instructionsLib.Height = grid1.RowDefinitions[0].ActualHeight;

            // Change text of addSub Button
            if (addSubButton.Width < 200)
            {
                addSubButton.Content = "Ajouter";
            }
            else
            {
                addSubButton.Content = "Ajouter Sous titres";
            }

            // Change text of delSub Button
            if (delSubButton.Width < 240 && delSubButton.Width > 175)
            {
                delSubButton.Content = "Supprimer";
            }
            if (delSubButton.Width < 175)
            {
                delSubButton.Content = "Suppr...";
            }
            if (delSubButton.Width > 240)
            {
                delSubButton.Content = "Supprimer Sous titres";
            }
        }

        // Action when add subtitle button is clicked
        private void blueButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.Convert.ToString(grid1.ColumnDefinitions[2].ActualWidth));
        }
        // Action when delete subtitle button is clicked
        private void redButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.Convert.ToString(grid1.ColumnDefinitions[2].ActualWidth));
        }
    }
}

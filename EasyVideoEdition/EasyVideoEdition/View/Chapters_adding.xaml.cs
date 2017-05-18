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
    /// Logique d'interaction pour Chapters_adding.xaml
    /// </summary>
    public partial class Chapters_adding : UserControl
    {
        public Chapters_adding()
        {
            InitializeComponent();
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Variables for button bottom colored line resizing
            //Blue line resizing (Add chapter)
            var blueButtonTemplate = addChapterButton.Template;
            Rectangle blueOuterRec = (Rectangle)blueButtonTemplate.FindName("outerRectangle", addChapterButton);


            //Red line resizing (Delete chapter)
            var redButtonTemplate = delChapterButton.Template;
            Rectangle redOuterRec = (Rectangle)redButtonTemplate.FindName("outerRectangle", delChapterButton);

            //GroupButtons
            //Blue button resizing (Add chapter)
            addChapterButton.Height = this.ActualHeight / 15;
            blueOuterRec.Height = this.ActualHeight / 120;
            addChapterButton.Width = this.ActualWidth / 5;
            addChapterButton.FontSize = this.ActualHeight / 100;

            //Red Button resizing (Delete chapter)
            delChapterButton.Height = this.ActualHeight / 15;
            redOuterRec.Height = this.ActualHeight / 120;
            delChapterButton.Width = this.ActualWidth / 5;


            //Slider resizing
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

            // Change text of addChapter Button
            if (addChapterButton.Width < 225)
            {
                addChapterButton.Content = "Ajouter";
            }
            else
            {
                addChapterButton.Content = "Ajouter Chapitres";
            }

            // Change text of delChapter Button
            if (delChapterButton.Width < 225 && delChapterButton.Width > 175)
            {
                delChapterButton.Content = "Supprimer";
            }
            if (delChapterButton.Width < 175)
            {
                delChapterButton.Content = "Suppr...";
            }
            if (delChapterButton.Width > 225)
            {
                delChapterButton.Content = "Supprimer Chapitres";
            }
        }
        // Action when add chapter button is clicked
        private void blueButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.Convert.ToString(grid1.ColumnDefinitions[2].ActualWidth));
        }
        // Action when delete chapter button is clicked
        private void redButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(System.Convert.ToString(grid1.ColumnDefinitions[2].ActualWidth));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EasyVideoEdition.Model;

namespace EasyVideoEdition.View
{
    /// <summary>
    /// Logique d'interaction pour Visual_adding.xaml
    /// </summary>
    public partial class Visual_adding : UserControl
    {
        private VideoTimer _videoTimer = VideoTimer.INSTANCE;
        public Visual_adding()
        {
            InitializeComponent();
            _videoTimer.transferMediaElt(ref MediaEl);
        }

        // Actions when the window is resized
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Variables for button bottom colored line resizing
            //Yellow line resizing (add picture button)
            var yellowButtonTemplate = addPicButton.Template;
            Rectangle yellowOuterRec = (Rectangle)yellowButtonTemplate.FindName("outerRectangle", addPicButton);

            //Blue line resizing (record button)
            var blueButtonTemplate = recButton.Template;
            Rectangle blueOuterRec = (Rectangle)blueButtonTemplate.FindName("outerRectangle", recButton);


            //Red line resizing (add video button)
            var redButtonTemplate = addVidButton.Template;
            Rectangle redOuterRec = (Rectangle)redButtonTemplate.FindName("outerRectangle", addVidButton);

            //GroupButtons
            //Record button resizing
            recButton.Height = this.ActualHeight / 15;
            redOuterRec.Height = this.ActualHeight / 120;
            recButton.Width = this.ActualWidth / 8.3;
            recButton.FontSize = this.ActualHeight / 100;

            //Add video button resizing
            addVidButton.Height = this.ActualHeight / 15;
            blueOuterRec.Height = this.ActualHeight / 120;
            addVidButton.Width = this.ActualWidth / 8.3;
            //Add picture button resizing
            addPicButton.Height = this.ActualHeight / 15;
            yellowOuterRec.Height = this.ActualHeight / 120;
            addPicButton.Width = this.ActualWidth / 8.3;


            //Slider resizing
            slider.Width = this.ActualWidth / 1.2;
            listViewStoryboard.Width = this.ActualWidth / 1.2;

            //UserControl instructionsLib resizing
            instructionsLib.Width = grid1.ColumnDefinitions[2].ActualWidth + grid1.ColumnDefinitions[3].ActualWidth + grid1.ColumnDefinitions[4].ActualWidth + grid1.ColumnDefinitions[5].ActualWidth;
            instructionsLib.Height = grid1.RowDefinitions[0].ActualHeight;


            //MediaEl resizing -> to keep 16/9 as dimensions
            if (((double)grid1.RowDefinitions[0].ActualHeight / (double)(grid1.ColumnDefinitions[0].ActualWidth + grid1.ColumnDefinitions[1].ActualWidth)) > (double)9 / (double)16)
            {
                MediaEl.Width = (grid1.ColumnDefinitions[0].ActualWidth + grid1.ColumnDefinitions[1].ActualWidth);
                MediaEl.Height = ((double)9 / (double)16 * (double)MediaEl.Width) - 30;
                MediaEl.Width -= 30;

                BlackRectangle.Width = (grid1.ColumnDefinitions[0].ActualWidth + grid1.ColumnDefinitions[1].ActualWidth);
                BlackRectangle.Height = ((double)9 / (double)16 * (double)MediaEl.Width) - 30;
                BlackRectangle.Width -= 30;
                BlackRectangle.Height += 15;
            }
            else
            {
                MediaEl.Height = grid1.RowDefinitions[0].ActualHeight - 170;
                MediaEl.Width = ((double)16 / (double)9 * (double)MediaEl.Height) - 30;
                MediaEl.Height -= 30;

                BlackRectangle.Height = grid1.RowDefinitions[0].ActualHeight - 170;
                BlackRectangle.Width = ((double)16 / (double)9 * (double)MediaEl.Height) - 30;
                BlackRectangle.Height += 15;
            }

            // Change text of blue Button
            if (recButton.Width < 205)
            {
                recButton.Content = "S'enr...";
            }
            else
            {
                recButton.Content = "S'enregistrer";
            }

            // Change text of red Button
            if ((addVidButton.Width < 205) && (addVidButton.Width > 125))
            {
                addVidButton.Content = "[...] vidéo";
            }
            else if (addVidButton.Width < 125)
            {
                addVidButton.Content = "[...] vid";
            }
            else
            {
                addVidButton.Content = "Ajouter vidéo";
            }

            // Change text of yellow Button
            if ((addPicButton.Width < 205) && (addPicButton.Width > 125))
            {
                addPicButton.Content = "[...] image";
            }
            else if (addPicButton.Width < 125)
            {
                addPicButton.Content = "[...] img";
            }
            else
            {
                addPicButton.Content = "Ajouter image";
            }
        }
        //Player methods
        public void MediaEl_Play(object sender, RoutedEventArgs e)
        {
            _videoTimer.startTimer();
        }

        public void MediaEl_Pause(object sender, RoutedEventArgs e)
        {
            _videoTimer.pauseTimer();
        }

        public void MediaEl_Stop(object sender, RoutedEventArgs e)
        {
            _videoTimer.stopTimer();
        }

    }
}

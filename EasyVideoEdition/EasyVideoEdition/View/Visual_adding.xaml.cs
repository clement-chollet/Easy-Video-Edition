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
    /// Logique d'interaction pour Visual_adding.xaml
    /// </summary>
    public partial class Visual_adding : UserControl
    {
        public Visual_adding()
        {
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //Record button resizing
            recButton.Height = this.ActualHeight / 15;
            recButton.Width = this.ActualWidth / 8.3;

            //Add video button resizing
            addVidButton.Height = this.ActualHeight / 15;
            addVidButton.Width = this.ActualWidth / 8.3;

            //Add picture button resizing
            addPicButton.Height = this.ActualHeight / 15;
            addPicButton.Width = this.ActualWidth / 8.3;

            //Slider resizing
            slider.Width = this.ActualWidth / 1.2;

            //Instructions area resizing
            instructions.Height = this.ActualHeight / 1.8;
            instructions.Width = this.ActualWidth / 4;

            //RecInstruction tab resizing
            recInstruction.Width = this.ActualWidth / 17;
            recInstruction.Height = this.ActualHeight / 20;

            //addVidInstruction tab resizing
            addVidInstruction.Width = this.ActualWidth / 17;

            //addPicInstruction tab resizing
            addPicInstruction.Width = this.ActualWidth / 17;

            //Library area resizing
            library.Height = this.ActualHeight / 1.8;
            library.Width = this.ActualWidth / 5.5;

            //videoTab resizing
            videoTab.Height = this.ActualHeight / 20;
            videoTab.Width = this.ActualWidth / 15;

            //imageTab resizing
            imageTab.Width = this.ActualWidth / 15;

            //instructionButton resizing
            instructionsButton.Height = instructionTab.Height - recInstruction.Height; 
        }

        public void MediaEl_Play(object sender, RoutedEventArgs e)
        {      
            this.MediaEl.Play();
        }

        public void MediaEl_Pause(object sender, RoutedEventArgs e)
        {
            this.MediaEl.Pause();
        }

        public void MediaEl_Stop(object sender, RoutedEventArgs e)
        {
            this.MediaEl.Stop();
        }


    }
}

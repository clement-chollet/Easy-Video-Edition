using EasyVideoEdition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using EasyVideoEdition.View;
using System.Windows;

namespace EasyVideoEdition.ViewModel
{
    class VideoPlayerViewModel : ObjectBase, BaseViewModel
    {
        #region Attributes
        public String name
        {
            get
            {
                return "Video Player";
            }
        }

        private Slider slide;
        private MediaElement MediaElementVideoPlayer;
        private static Visual_adding view;

        private VideoPlayer _videoPlayer = new VideoPlayer();

        #endregion

        #region Get/Set
        public VideoPlayer videoPlayer
        {
            get
            {
                return _videoPlayer;
            }
            set
            {
                _videoPlayer = value;
                RaisePropertyChanged("videoPlayer");
            }
        }
        #endregion

        #region CommandList
        public ICommand PlayVideoCommand
        {
            get; private set;
        }

        public ICommand PauseVideoCommand
        {
            get; private set;
        }

        public ICommand StopVideoCommand
        {
            get; private set;
        }
        #endregion


        /// <summary>
        /// Creation of the VideoPlayerViewModel. Create the command and define the videoPath
        /// </summary>
        /// <param name="videoPath">Path of the VideoToPlay</param>
        public VideoPlayerViewModel(String videoPath)
        {
            PlayVideoCommand = new RelayCommand(PlayVideo);
            PauseVideoCommand = new RelayCommand(PauseVideo);
            StopVideoCommand = new RelayCommand(StopVideo);
            videoPlayer.source = videoPath;
        }

        /// <summary>
        /// Creation of the VideoPlayerViewModel.
        /// </summary>
        public VideoPlayerViewModel()
        {

            PlayVideoCommand = new RelayCommand(PlayVideo);
            PauseVideoCommand = new RelayCommand(PauseVideo);
            StopVideoCommand = new RelayCommand(StopVideo);
        }

        /// <summary>
        /// Static method that allow the viewModel to be aware of the mediaPlayer. Needed for the player command
        /// </summary>
        /// <param name="viewVP"></param>
        static public void InitializeVM(Visual_adding viewVP)
        {
            view = viewVP;
        }

        #region CommandDefinition

        private void PlayVideo()
        {
            view.mediaElement.Play();
        }

        private void PauseVideo()
        {
           view.mediaElement.Pause();
        }

        private void StopVideo()
        {
            view.mediaElement.Stop();
        }

        #endregion
    }
}

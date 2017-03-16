using EasyVideoEdition.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyVideoEdition.ViewModel
{
    class VisualAddingViewModel
    {
        /// <summary>
        /// Get the instance of the viewModel
        /// </summary>
        public static VisualAddingViewModel INSTANCE
        {
            get
            {
                return singleton;
            }
        }

        #region Attributes
        private static VisualAddingViewModel singleton = new VisualAddingViewModel();
        private FileBrowser _filebrowser = new FileBrowser();
        private ObservableCollection<Video> _listVideo;
        private ObservableCollection<Photo> _listPhoto;
        private StoryBoard _storyBoard = StoryBoard.INSTANCE;
        #endregion

        #region Get/Set
        /// <summary>
        /// Get the list of the photo added to the project
        /// </summary>
        public ObservableCollection<Photo> listPhoto
        {
            get
            {
                return _listPhoto;
            }
        }

        /// <summary>
        /// Get the list of the video added to the project
        /// </summary>
        public ObservableCollection<Video> listVideo
        {
            get
            {
                return _listVideo;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public StoryBoard storyBoard
        {
            get
            {
                return _storyBoard;
            }

            set
            {
                _storyBoard = value;
            }
        }
        #endregion

        #region CommandList
        public ICommand addVideoCommand { get; private set; }
        public ICommand addPhotoCommand { get; private set; }


        #endregion

        private VisualAddingViewModel()
        {
            _listVideo = new ObservableCollection<Video>();
            _listPhoto = new ObservableCollection<Photo>();
            addPhotoCommand = new RelayCommand(addPhoto);
            addVideoCommand = new RelayCommand(addVideo);
        }

        /// <summary>
        /// Add a video to the video list and the storyboard
        /// </summary>
        private void addVideo()
        {
            _filebrowser.OpenFile("Toute les vidéos |*.avi; *.mkv; *.mp4|Fichier AVI (.avi)|*.avi|Fichier MKV (*.mkv)|*.mkv|Fichier MP4 (.mp4)|*.mp4");
            if(_filebrowser.filePath != null)
            {
                Video v = new Video(_filebrowser.filePath, _filebrowser.fileName, _filebrowser.fileSize);
                listVideo.Add(v);
                storyBoard.addFile(v, 0, v.duration);
            }
           
            _filebrowser.reset();
        }

        /// <summary>
        /// Add a photo to the video list and the storyboard
        /// </summary>
        private void addPhoto()
        {
            _filebrowser.OpenFile("Toute les images |*.png; *.jpeg; *.jpg|Image PNG (.png)|*.avi|Fichier JPEG (.jpeg, .jpg)|*.jpeg; *.jpg");
            if (_filebrowser.filePath != null)
            {
                Photo f = new Photo(_filebrowser.filePath, _filebrowser.fileName, _filebrowser.fileSize);
                listPhoto.Add(f);
                storyBoard.addFile(f, 0, 5);
            }
               
            _filebrowser.reset();
        }
    }
}

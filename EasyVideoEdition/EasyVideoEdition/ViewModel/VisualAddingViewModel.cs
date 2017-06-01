using EasyVideoEdition.Model;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EasyVideoEdition.ViewModel
{
    /// <summary>
    /// ViewModel corresponding to the VisualAdding View
    /// </summary>
    class VisualAddingViewModel : ObjectBase
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
        private ObservableCollection<Picture> _listPhoto;
        private StoryBoard _storyBoard = StoryBoard.INSTANCE;
        private MainVideo _mainVideo = MainVideo.INSTANCE;
        private VideoPlayer _videoPlayer = VideoPlayer.INSTANCE;
        private VideoTimer _videoTimer = VideoTimer.INSTANCE;
        private bool _firstVideo = true;
        private Visibility _isNoVideoAlert = Visibility.Visible;
        #endregion

        #region Get/Set
        /// <summary>
        /// Get the list of the photo added to the project
        /// </summary>
        public ObservableCollection<Picture> listPhoto
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
        /// Get or set the storyboard of the project
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

        /// <summary>
        /// Get and set the main video which will be playable in the ffmediaElement
        /// </summary>
        public MainVideo mainVideo
        {
            get
            {
                return _mainVideo;
            }
            set
            {
                _mainVideo = value;
                RaisePropertyChanged("mainVideo");
            }
        }

        /// <summary>
        /// Get or set the video player of the view
        /// </summary>
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

        /// <summary>
        /// Indicate if there is a video to be played or not
        /// </summary>
        public Visibility isNoVideoAlert
        {
            get
            {
                return _isNoVideoAlert;
            }
            set
            {
                _isNoVideoAlert = value;
                RaisePropertyChanged("isNoVideoAlert");
            }
        }
        #endregion

        #region CommandList
        public ICommand addVideoCommand { get; private set; }
        public ICommand addPhotoCommand { get; private set; }
        public ICommand removeFileCommand { get; private set; }
        public ICommand testCommand { get; private set; }

        #endregion

        private VisualAddingViewModel()
        {
            _listVideo = new ObservableCollection<Video>();
            _listPhoto = new ObservableCollection<Picture>();
            addPhotoCommand = new RelayCommand(addPhoto);
            addVideoCommand = new RelayCommand(addVideo);
            removeFileCommand = new RelayCommand(removeFile);
            testCommand = new RelayCommand(test);
        }

        /// <summary>
        /// Add a video to the video list and the storyboard
        /// </summary>
        private void addVideo()
        {
            _filebrowser.OpenFile("Toute les vidéos |*.avi; *.mkv; *.mp4|Fichier AVI (.avi)|*.avi|Fichier MKV (*.mkv)|*.mkv|Fichier MP4 (.mp4)|*.mp4");
            if (_filebrowser.filePath != null)
            {
                Video v = new Video(_filebrowser.filePath, _filebrowser.fileName, _filebrowser.fileSize);
                listVideo.Add(v);

                storyBoard.addFile(v, StoryBoard.INSTANCE.duration, StoryBoard.INSTANCE.duration.Add(TimeSpan.FromMilliseconds(v.duration)), "Video");
                StoryBoard.INSTANCE.duration = StoryBoard.INSTANCE.duration.Add(TimeSpan.FromMilliseconds(v.duration));

                if (_firstVideo == true)
                {
                    videoPlayer.source = v.filePath;
                    mainVideo.video = v;
                    _firstVideo = false;
                    isNoVideoAlert = Visibility.Hidden;
                }
                else
                {
                    _videoTimer.stopTimer();
                }
            }

            _filebrowser.reset();
        }

        /// <summary>
        /// Add a photo to the video list and the storyboard
        /// </summary>
        private void addPhoto()
        {
            _filebrowser.OpenFile("Toute les images |*.png; *.jpeg; *.jpg|Image PNG (.png)|*.png|Fichier JPEG (.jpeg, .jpg)|*.jpeg; *.jpg");
            if (_filebrowser.filePath != null)
            {
                Picture f = new Picture(_filebrowser.filePath, _filebrowser.fileName, _filebrowser.fileSize);
                listPhoto.Add(f);
                storyBoard.addFile(f, TimeSpan.FromMilliseconds(0), TimeSpan.FromSeconds(5), "Image");
            }

            _filebrowser.reset();
        }

        /// <summary>
        /// Remove a file from the file list. If the file also appears in the storboard the file is also removed from it.
        /// </summary>
        /// <param name="file">The file to remove</param>
        private void removeFile(Object file)
        {
            IEnumerator<StoryBoardElement> search = _storyBoard.fileList.GetEnumerator();
            StoryBoardElement fileToRemove = null;
            switch (file.GetType().Name)
            {
                case "Video":
                    {
                        Video v = (Video)file;
                        listVideo.Remove(v);

                        while (search.MoveNext())
                        {
                            if (search.Current.filePath == v.filePath)
                            {
                                fileToRemove = search.Current;
                            }
                        }

                        break;
                    }

                case "Picture":
                    {
                        Picture p = (Picture)file;
                        listPhoto.Remove(p);

                        while (search.MoveNext())
                        {
                            if (search.Current.filePath == p.filePath)
                            {
                                fileToRemove = search.Current;
                            }
                        }

                        break;
                    }
            }

            storyBoard.fileList.Remove(fileToRemove);
            _videoTimer.stopTimer();
            if(storyBoard.fileList.Count == 0)
            {
                isNoVideoAlert = Visibility.Visible;
            }
        }

        private void test()
        {
            storyBoard.printAllElements();
            TimeSpan newDuration = TimeSpan.FromMilliseconds(2000);
            //storyBoard.cutFromTo(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(6));
            Console.WriteLine("/////////////////////////");
            storyBoard.insertInto(storyBoard.fileList[2], newDuration);
            //storyBoard.printAllElements();
            //storyBoard.sortFileList();
            Console.WriteLine("/////////////////////////");
            //storyBoard.printAllElements();
        }

    }
}

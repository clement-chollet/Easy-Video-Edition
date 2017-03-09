using EasyVideoEdition.Model;
using System;
using System.Windows.Input;

namespace EasyVideoEdition.ViewModel
{
    /// <summary>
    /// View Model of the File Opening. Define the command needed to open a video file.
    /// SINGLETON
    /// </summary>
    class OpeningViewModel : ObjectBase, IBaseViewModel
    {

        #region Attributes
        private static OpeningViewModel singleton = new OpeningViewModel();
        private FileBrowser _browser;
        /// <summary>
        /// Name of the ViewModel
        /// </summary>
        public String name
        {
            get
            {
                return ("Open File");
            }
        }
        #endregion

        #region Get/Set
        /// <summary>
        /// Model of the fileBrower. 
        /// </summary>
        public FileBrowser browser
        {
            get
            {
                return _browser;
            }
            set
            {
                _browser = value;
                RaisePropertyChanged("browser");
            }
        }

        /// <summary>
        /// Get the instance of the viewModel
        /// </summary>
        public static OpeningViewModel INSTANCE
        {
            get
            {
                return singleton;
            }
        }
        #endregion

        #region CommandList
        /// <summary>
        /// Getter and Setter for the OpenFileCommand. This Command launch the method OpenFile. 
        /// </summary>
        public ICommand OpenFileCommand
        {
            get; private set;
        }

        /// <summary>
        /// Getter and Setter for the OpenFileCommand. This Command launch the method NewProject. 
        /// </summary>
        public ICommand NewProjectCommand {
            get; private set;
        }

        #endregion

        /// <summary>
        /// Creation of the MainViewModel. Create the commands OpenFile and init the Model.
        /// </summary>
        private OpeningViewModel()
        {
            browser = new FileBrowser();
            OpenFileCommand = new RelayCommand(OpenFile);
            NewProjectCommand = new RelayCommand(NewProject);
        }

        #region CommandDefinition
        /// <summary>
        /// Method that launch the OpenFile method of the Filebrowser. 
        /// </summary>
        private void OpenFile()
        {
            browser.OpenFile();
            if(browser.filePath != null)
                SubtitlesViewModel.INSTANCE.InitSRTFile(browser.filePath);
            browser.reset();
        }

        /// <summary>
        /// Method that switch the user to the new view for creating an empty project
        /// </summary>
        private void NewProject()
        {
            MainViewModel.INSTANCE.actualViewIndex = 1;
        }

        #endregion
    }
}

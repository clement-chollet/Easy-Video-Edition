using EasyVideoEdition.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EasyVideoEdition.ViewModel
{
    /// <summary>
    /// View Model of the File Opening. Define the command needed to open a video file.
    /// </summary>
    class FileOpeningViewModel : ObjectBase, BaseViewModel
    {

        #region Attributes

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
        #endregion

        #region CommandList
        /// <summary>
        /// Getter and Setter for the OpenFileCommand. This Command launch the method OpenFile. 
        /// </summary>
        public ICommand OpenFileCommand
        {
            get; private set;
        }
        #endregion

        /// <summary>
        /// Creation of the MainViewModel. Create the commands OpenFile and init the Model.
        /// </summary>
        public FileOpeningViewModel()
        {
            browser = new FileBrowser();
            OpenFileCommand = new RelayCommand(OpenFile);
        }

        #region CommandDefinition
        /// <summary>
        /// Method that launch the OpenFile method of the Filebrowser. 
        /// </summary>
        private void OpenFile()
        {
            browser.OpenFile();
            MessageBox.Show(MainViewModel.viewModelList.ElementAt(2)+ "");
            SubtitlesViewModel sb = (SubtitlesViewModel) MainViewModel.viewModelList.ElementAt(2);
            sb.InitSRTFile(browser.filePath);

        }


        #endregion
    }
}

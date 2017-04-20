using EasyVideoEdition.Model;
using Newtonsoft.Json;
using NReco.VideoConverter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EasyVideoEdition.ViewModel
{
    class SaveFileViewModel : ObjectBase, IBaseViewModel
    {
        #region Attributes
        private static SaveFileViewModel singleton = new SaveFileViewModel();
        private FileBrowser _browser = new FileBrowser();

        private MainVideo _mainVideo = MainVideo.INSTANCE;
        private VideoConverter _converter = VideoConverter.INSTANCE;
        #endregion

        #region Get/Set
        /// <summary>
        /// Get the instance of the viewModel
        /// </summary>
        public static SaveFileViewModel INSTANCE
        {
            get
            {
                return singleton;
            }
        }

        /// <summary>
        /// Get the name of the viewModel
        /// </summary>
        public String name
        {
            get
            {
                return "Save File/Project";
            }
        }

        public VideoConverter converter
        {
            get
            {
                return _converter;
            }
        }
        //--------------------------------------------------------------------------------------------\\
        //----------------------------------------COMMAND LIST----------------------------------------\\
        //--------------------------------------------------------------------------------------------\\


        /// <summary>
        /// Command to save the project into a file to future modification
        /// </summary>
        public ICommand SaveProjectCommand { get; private set; }

        /// <summary>
        /// Command that launch the export of the project into a readable videofile.
        /// </summary>
        public ICommand ExportProjectCommand { get; private set; }




        #endregion

        private SaveFileViewModel()
        {
            SaveProjectCommand = new RelayCommand(SaveProject);
            ExportProjectCommand = new RelayCommand(ExportProject);
        }

        private void ExportProject()
        {
            StoryBoard st = StoryBoard.INSTANCE;

            String json = JsonConvert.SerializeObject(st);
            String savePath = null;
            _browser.reset();
            savePath = _browser.SaveFile("Fichier JSON (.json)|*.json");
            if (savePath != "")
                File.WriteAllText(savePath, json);
        }

        private void SaveProject()
        {
            _browser.reset();
            String savePath = null;
            savePath = _browser.SaveFile("Fichier AVI (.avi)|*.avi");
            IEnumerator<StoryBoardElement> fileToExport = StoryBoard.INSTANCE.fileList.GetEnumerator();

            if (savePath != "")
                VideoConverter.INSTANCE.exportVideoStart(1280, 720, 30, "h264", "ac3", fileToExport, savePath);
            
            
        }




    }
}

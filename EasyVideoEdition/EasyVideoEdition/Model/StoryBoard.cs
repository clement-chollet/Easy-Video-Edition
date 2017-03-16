using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyVideoEdition.Model
{
    /// <summary>
    /// Class that define the storyboard for the video editor
    /// THE ONLY STORYBOARD INSTANCE THAT SHOULD BY USE IS THE SINGLETON. DO NOT USE THIS CONSTRUCTOR !!! 
    /// HE IS PUBLIC DUE TO THE JSON PARSER !!!
    /// </summary>
    class StoryBoard
    {

        /// <summary>
        /// Get the instance of the viewModel
        /// </summary>
        public static StoryBoard INSTANCE
        {
            get
            {
                return singleton;
            }
        }

        #region Attributes
        private ObservableCollection<StoryBoardElement> _fileList = new ObservableCollection<StoryBoardElement>();
        private static StoryBoard singleton = new StoryBoard();
        #endregion

        #region Get/Set
        /// <summary>
        /// List of the file (photo or video) within the storyBoard
        /// </summary>
        public ObservableCollection<StoryBoardElement> fileList
        {
            get
            {
                return _fileList;
            }
        }
        #endregion

        /// <summary>
        /// Add a file to the storyboard
        /// </summary>
        /// <param name="fileToAdd"></param>
        public void addFile(IFile fileToAdd, int startTime, int endTime, string type)
        {
            _fileList.Add(new StoryBoardElement(fileToAdd, startTime, endTime, type, fileToAdd.fileName, fileToAdd.fileSize));
        }

        /// <summary>
        /// /!\ Clean the storyboard /!\
        /// </summary>
        public void purge()
        {
            _fileList.Clear();
        }
    }
}

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
        ObservableCollection<IFile> _fileList = new ObservableCollection<IFile>();
        private static StoryBoard singleton = new StoryBoard();
        #endregion

        #region Get/Set
        /// <summary>
        /// List of the file (photo or video) within the storyBoard
        /// </summary>
        public ObservableCollection<IFile> fileList
        {
            get
            {
                return _fileList;
            }
        }
        #endregion

        private StoryBoard()
        {

        }
        /// <summary>
        /// Add a file to the storyboard
        /// </summary>
        /// <param name="fileToAdd"></param>
        public void addFile(IFile fileToAdd)
        {
            _fileList.Add(fileToAdd);
        }
    }
}

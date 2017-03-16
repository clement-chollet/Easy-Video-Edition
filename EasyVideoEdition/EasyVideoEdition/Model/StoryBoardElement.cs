using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVideoEdition.Model
{
    class StoryBoardElement
    {
        #region Attributes
        
        private IFile _file;
        private string _filePath;
        private string _fileType;
        private int _startTime;
        private int _endTime;
        private string _fileName;
        private long _fileSize;

        #endregion

        #region Get/Set
        /// <summary>
        /// Return or set the file within the element
        /// </summary>
        [JsonIgnore]
        public IFile file
        {
            get
            {
                return _file;
            }

            set
            {
                _file = value;
            }
        }

        /// <summary>
        /// Return or set the startTime of the video
        /// </summary>
        public int startTime
        {
            get
            {
                return _startTime;
            }

            set
            {
                _startTime = value;
            }
        }

        /// <summary>
        /// Return or set the endTime of the video
        /// </summary>
        public int endTime
        {
            get
            {
                return _endTime;
            }

            set
            {
                _endTime = value;
            }
        }

        /// <summary>
        /// Path to the file
        /// </summary>
        public string filePath
        {
            get
            {
                return _filePath;
            }

            set
            {
                _filePath = value;
            }
        }

        /// <summary>
        /// Type of the file. Used for saving and retreive the project
        /// </summary>
        public string fileType
        {
            get
            {
                return _fileType;
            }

            set
            {
                _fileType = value;
            }
        }

        /// <summary>
        /// Size of the file
        /// </summary>
        public long fileSize
        {
            get
            {
                return _fileSize;
            }

            set
            {
                _fileSize = value;
            }
        }

        /// <summary>
        /// Name of the file
        /// </summary>
        public string fileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
            }
        }
        #endregion

        public StoryBoardElement()
        {

        }
        /// <summary>
        /// Create a storyboard element.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="fileType"></param>
        /// <param name="fileName"></param>
        /// <param name="fileSize"></param>
        public StoryBoardElement(IFile file, int start, int end, string fileType, string fileName, long fileSize)
        {
            this.file = file;
            this.filePath = file.filePath;
            this.startTime = start;
            this.endTime = end;
            this.fileType = fileType;
            this.fileName = fileName;
            this.fileSize = fileSize;
        }
    }
}

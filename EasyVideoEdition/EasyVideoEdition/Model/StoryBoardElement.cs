using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVideoEdition.Model
{
    /// <summary>
    /// Describe a element of the storyboard, without beeing dependant of the file type.
    /// </summary>
    class StoryBoardElement
    {
        #region Attributes
        
        private IFile _file;
        private string _filePath;
        private string _fileType;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
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
        /// Return or set the startTime of the element
        /// </summary>
        public TimeSpan startTime
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
        /// Return or set the endTime of the element
        /// </summary>
        public TimeSpan endTime
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
        /// Path to the element
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
        /// Type of the element. Used for saving and retreive the project
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
        /// Size of the element
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
        /// Name of the element
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
        /// <param name="file">The file that will be contained in the element</param>
        /// <param name="start">Start time of the file</param>
        /// <param name="end">End time of the file/param>
        /// <param name="fileType">Type of file like Video / Picture. Used for retreive and save the project </param>
        /// <param name="fileName">Name of the file within the element</param>
        /// <param name="fileSize">The size of the file in octet</param>
        public StoryBoardElement(IFile file, TimeSpan start, TimeSpan end, string fileType, string fileName, long fileSize)
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

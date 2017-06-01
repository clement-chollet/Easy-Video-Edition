using Newtonsoft.Json;
using System;


namespace EasyVideoEdition.Model
{
    /// <summary>
    /// Describe a element of the storyboard, without beeing dependant of the file type.
    /// </summary>
    class StoryBoardElement : ObjectBase
    {
        #region Attributes

        private IFile _file;
        private string _filePath;
        private string _fileType;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        private TimeSpan _startTimeInSource;
        private TimeSpan _endTimeInSource;
        private string _fileName;
        private long _fileSize;
        private int _numPart;
        private double _duration;
        private Boolean _hasToBeSplit = false;
        private Subtitles _subtitles;

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
        public TimeSpan startTime
        {
            get
            {
                return _startTime;
            }

            set
            {
                _startTime = value;
                RaisePropertyChanged("startTime");
            }
        }

        /// <summary>
        /// Return or set the endTime of the video
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
                RaisePropertyChanged("endTime");
            }
        }

        /// <summary>
        /// Return or set the start time in the source
        /// </summary>
        public TimeSpan startTimeInSource
        {
            get
            {
                return _startTimeInSource;
            }

            set
            {
                _startTimeInSource = value;
                RaisePropertyChanged("startTimeInSource");
            }
        }

        /// <summary>
        /// Return or set the end time in the source
        /// </summary>
        public TimeSpan endTimeInSource
        {
            get
            {
                return _endTimeInSource;
            }

            set
            {
                _endTimeInSource = value;
                RaisePropertyChanged("endTimeInSource");
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

        /// <summary>
        /// num of the part used if splitted video
        /// </summary>
        public int numPart
        {
            get
            {
                return _numPart;
            }

            set
            {
                _numPart = value;
            }
        }

        /// <summary>
        /// Necessity to be split in the process
        /// </summary>
        public Boolean hasToBeSplit
        {
            get
            {
                return _hasToBeSplit;
            }

            set
            {
                _hasToBeSplit = value;
            }
        }

        /// <summary>
        /// duration of the element
        /// </summary>
        public double duration
        {
            get
            {
                return _duration;
            }

            set
            {
                _duration = value;
            }
        }

        /// <summary>
        /// subtitles of this element
        /// </summary>
        public Subtitles subtitles
        {
            get
            {
                return _subtitles;
            }

            set
            {
                _subtitles = value;
            }
        }
        #endregion

        /// <summary>
        /// Default constructor (used for JSON parsing)
        /// </summary>
        public StoryBoardElement()
        {

        }

        /// <summary>
        /// Create a storyboard element.
        /// </summary>
        /// <param name="file">the file to integrate in this storyboard element</param>
        /// <param name="start">start time in the entire storyboard</param>
        /// <param name="end">end time in the entire storyboard</param>
        /// <param name="fileType">type of the storyboard element (video or image)</param>
        /// <param name="fileName">name of the file corresponding to the storyboard element</param>
        /// <param name="fileSize">size of the file corresponding to the storyboard element</param>
        public StoryBoardElement(IFile file, TimeSpan start, TimeSpan end, string fileType, string fileName, long fileSize)
        {
            this.file = file;
            this.filePath = file.filePath;
            this.startTime = start;
            this.endTime = end;
            this.startTimeInSource = TimeSpan.Parse("00:00:00");
            this.endTimeInSource = calcEndInSource();
            this.fileType = fileType;
            this.fileName = fileName;
            this.fileSize = fileSize;
            this.numPart = 1;
            this.subtitles = new Subtitles(this.filePath);
            calcDuration();
        }

        /// <summary>
        /// Create a storyboard element.
        /// </summary>
        /// <param name="file">the file to integrate in this storyboard element</param>
        /// <param name="start">start time in the entire storyboard</param>
        /// <param name="end">end time in the entire storyboard</param>
        /// <param name="startSource">start time in the source of the file</param>
        /// <param name="endSource">end time in the source of the file</param>
        /// <param name="fileType">type of the storyboard element (video or image)</param>
        /// <param name="fileName">name of the file corresponding to the storyboard element</param>
        /// <param name="fileSize">size of the file corresponding to the storyboard element</param>
        public StoryBoardElement(IFile file, TimeSpan start, TimeSpan end, TimeSpan startSource, TimeSpan endSource, string fileType, string fileName, long fileSize)
        {
            this.file = file;
            this.filePath = file.filePath;
            this.startTime = start;
            this.endTime = end;
            this.startTimeInSource = startSource;
            this.endTimeInSource = endSource;
            this.fileType = fileType;
            this.fileName = fileName;
            this.fileSize = fileSize;
            this.numPart = 1;
            this.subtitles = new Subtitles(this.filePath);
        }

        /// <summary>
        /// Edit the times of the storyboard element.
        /// </summary>
        /// <param name="start">start in the storyboard</param>
        /// <param name="end">end in the storyboard</param>
        /// <param name="startSource">start in the source</param>
        /// <param name="endSource">end in the source</param>
        public void editTimes(TimeSpan start, TimeSpan end, TimeSpan startSource, TimeSpan endSource)
        {
            this.startTime = start;
            this.endTime = end;
            this.startTimeInSource = startSource;
            this.endTimeInSource = endSource;
        }

        /// <summary>
        /// calculates the end time in the source
        /// </summary>
        /// <returns></returns>
        public TimeSpan calcEndInSource()
        {
            double seconds;
            seconds = this.endTime.TotalSeconds - this.startTime.TotalSeconds;
            TimeSpan endSource = TimeSpan.FromSeconds(this.startTimeInSource.TotalSeconds + seconds);
            return endSource;
        }

        /// <summary>
        /// Calculs the duration of the element
        /// </summary>
        public void calcDuration()
        {
            this.duration = this.endTimeInSource.TotalMilliseconds - startTimeInSource.TotalMilliseconds;
            Console.WriteLine("duration elt : " + this.duration);
        }
    }
}

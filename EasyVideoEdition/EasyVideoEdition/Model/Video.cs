using NReco.VideoConverter;
using NReco.VideoInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EasyVideoEdition.Model
{
    /// <summary>
    /// Describe a Video. Implement the interface IFile
    /// </summary>
    class Video : ObjectBase, IFile
    {

        #region Attributes
        private String _filePath;
        private String _fileName;
        private String _sizeLabel;
        private String _durationLabel;

        private long _fileSize;
        private double _duration;
        private string _miniatPath;
        private String _extension;
        private String _directory;

        #endregion

        #region Get/Set
        /// <summary>
        /// Get of set the path of the file
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
        /// Get or Set the name of the file
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
        /// Size of the file in octet
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
        /// Duration of the video in sec
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
        /// Contains the label of the file size in the format "size + unit"
        /// </summary>
        public string sizeLabel
        {
            get
            {
                return _sizeLabel;
            }

            set
            {
                _sizeLabel = value;
            }
        }

        /// <summary>
        /// Contains the label of the file duration in the format "duration + unit"
        /// </summary>
        public string durationLabel
        {
            get
            {
                return _durationLabel;
            }

            set
            {
                _durationLabel = value;
            }
        }

        /// <summary>
        /// Gets or sets the miniat path.
        /// </summary>
        /// <value>
        /// The miniat path.
        /// </value>
        public string miniatPath
        {
            get
            {
                return _miniatPath;
            }

            set
            {
                _miniatPath = value;
                RaisePropertyChanged("miniatPath");
            }
        }

        /// <summary>
        /// Gets or sets the extension of the video
        /// </summary>
        public string extension
        {
            get
            {
                return _extension;
            }

            set
            {
                _extension = value;
            }
        }

        /// <summary>
        /// Gets or sets the directory of the video
        /// </summary>
        public string directory
        {
            get
            {
                return _directory;
            }

            set
            {
                _directory = value;
            }
        }


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Video"/> class.
        /// </summary>
        /// <param name="path">The path of the video</param>
        /// <param name="name">The name of the video with the extension</param>
        /// <param name="size">The size of the video </param>
        public Video(String path, String name, long size)
        {
            var ffProbe = new FFProbe();
            var videoInfo = ffProbe.GetMediaInfo(path);

            this.filePath = path;
            this.fileName = name;

            videoScreenshotCreator vsc = new videoScreenshotCreator(filePath, fileName);
            this.fileSize = size;
            this.duration = videoInfo.Duration.TotalMilliseconds;
            this.durationLabel = calcDuration(videoInfo.Duration);
        }


        /// <summary>
        /// Calculates the size with the appropriate unit.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>A string with the form of size + unit</returns>
        protected string calcSize(long size)
        {
            String unit = "";
            double div;

            if (size > 1000000000)
            {
                unit = "Go";
                div = 1000000000;
            }
            else
            {
                if (size > 1000000)
                {
                    unit = "Mo";
                    div = 1000000;
                }
                else
                {
                    if (size > 1000)
                    {
                        unit = "Ko";
                        div = 1000;
                    }
                    else
                    {
                        unit = "octet(s)";
                        div = 1;
                    }
                }

            }

            return Math.Round(size / div, 1) + unit;
        }

        /// <summary>
        /// Calculates the duration with an appropriate unit.
        /// </summary>
        /// <param name="duration"></param>
        /// <returns>A string with the form of duration + unit</returns>
        protected string calcDuration(TimeSpan duration)
        {
            String unit = "";
            double dur;
           
            if(duration.TotalSeconds < 60)
            {
                dur = duration.TotalSeconds;
                unit = " sec";
            }
            else
            {
                dur = duration.TotalMinutes;
                unit = " min";
            }
            return Math.Round(dur, 1) + unit;
        }

        /// <summary>
        /// Find the extension of the video
        /// </summary>
        public void getExtension()
        {
            String extensRev = "";
            String extens = "";
            int i = this.filePath.Length - 1;
            while (this.filePath[i] != '.')
            {
                extensRev += filePath[i];
                i--;
            }

            for (int k = extensRev.Length - 1; k >= 0; k--)
            {
                extens += extensRev[k];
            }

            this.extension = extens;
        }

        /// <summary>
        /// Find the name of the video
        /// </summary>
        public void getFileName()
        {
            String nameRev = "";
            String name = "";
            int i = this.filePath.Length - 1;
            while (this.filePath[i] != '/' && this.filePath[i] != '\\')
            {
                nameRev += filePath[i];
                i--;
            }
            //MessageBox.Show(nameRev);
            for (int k = nameRev.Length - 1; k >= 0; k--)
            {
                name += nameRev[k];
            }

            this.fileName = name;

        }

        /// <summary>
        /// Find the directory of the video
        /// </summary>
        public void getDirectory()
        {

            String[] parts = Regex.Split(filePath, this.fileName);

            this.directory = parts[0];
        }
    }

}

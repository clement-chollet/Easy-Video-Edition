using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyVideoEdition.Model
{
    class Video : ObjectBase, IFile
    {

        #region Attributes
        private String _filePath;
        private String _fileName;
        private String _sizeLabel;
        private long _fileSize;
        private int _duration;
        private string _miniatPath;
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
        public int duration
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
        ///  Contains the label of the file size in the format "size + unit"
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
        /// 
        /// </summary>
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


        #endregion


        public Video(String path, String name, long size)
        {
            this.filePath = path;
            this.fileName = name;
            videoScreenshotCreator vsc = new videoScreenshotCreator(filePath, fileName);
            this.fileSize = size;
            this.duration = 2017;
            this.miniatPath = "D:\\EVE\\loading.png";
            sizeLabel = calcSize(size);
            Task.Delay(2000).ContinueWith(_ =>
            {
                this.miniatPath = "D:\\Eve\\Temp\\Screenshot\\" + fileName.Split('.')[0] + ".jpeg";
            });
           
           
        }

        protected string calcSize(long size)
        {
            String unit = "YOLO";
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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyVideoEdition.Model
{
    class Photo : IFile
    {
        #region Attributes
        private String _filePath;
        private String _fileName;
        private long _fileSize;
        private string _sizeLabel;
        private String _miniatPath;
        private int _duration;
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
            }
        }

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
        #endregion


        public Photo(String path, String name, long size)
        {
            this.filePath = path;
            this.fileName = name;
            this.fileSize = size;
            this.miniatPath = path;
            sizeLabel = calcSize(size);
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

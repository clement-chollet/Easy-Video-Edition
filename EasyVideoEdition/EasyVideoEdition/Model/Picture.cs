using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyVideoEdition.Model
{
    /// <summary>
    /// Describe a picture. Implement the interface IFile
    /// </summary>
    class Picture : IFile
    {
        #region Attributes
        private String _filePath;
        private String _fileName;
        private long _fileSize;
        private string _sizeLabel;
        private String _miniatPath;
        private double _duration;
        #endregion

        #region Get/Set
        /// <summary>
        /// Get of set the path of the picture
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
        /// Get or Set the name of the picture
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
        /// Size of the picture in octet
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
        /// Contain the path of the thumbnail used for preview 
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

        /// <summary>
        /// Contains the duration of the picture in sec. It's how many time it will appears (5 sec by default)
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
        #endregion

        /// <summary>
        /// Base constructor of a picture. Init it's path, it's name, it's size and size Label, the thumbnail path.
        /// </summary>
        /// <param name="path">Path of the picture</param>
        /// <param name="name">Name of the picture with the extension</param>
        /// <param name="size">Size of the picture</param>
        public Picture(String path, String name, long size)
        {
            this.filePath = path;
            this.fileName = name;
            this.fileSize = size;
            this.miniatPath = path;
            sizeLabel = calcSize(size);
        }

        /// <summary>
        /// Method that calc the size of the picture in a readable size and change it's unit depending of the size
        /// </summary>
        /// <param name="size">Size to proceed</param>
        /// <returns>The new size + the new unit</returns>
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
    }
}

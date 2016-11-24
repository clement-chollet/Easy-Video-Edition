﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// namespace: EasyVideoEdition.Model
//
// summary:	.
namespace EasyVideoEdition.Model
{
    /// <summary>   A file browser. </summary>
    ///
    /// <remarks>   Théo, 10/11/2016. </remarks>
    
    class FileBrowser : ObjectBase
    {
        #region Attributes
        private string _filePath;
        private string _fileTempPath;
        private string _fileName;
        private byte[] _fileData;
        private long _fileSize;
        private bool _isFileOpened;
        private FileStream _stream;

        /// <summary>
        /// Contains the stream of the file.
        /// </summary>
        public FileStream stream
        {
            get
            {
                return _stream;
            }
            set
            {
                _stream = value;
                RaisePropertyChanged("stream");
            }
        }

        /// <summary>
        /// Contains the filePath of the file.
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
                RaisePropertyChanged("filePath");
            }
        }

        /// <summary>
        /// Contains the temporary filePath of the file. It's where the soft clone the original file. 
        /// </summary>
        public string fileTempPath
        {
            get
            {
                return _fileTempPath;
            }
            set
            {
                _fileTempPath = value;
                RaisePropertyChanged("fileTempPath");
            }
        }

        /// <summary>
        /// Indicate that a file is opened. Or not. 
        /// </summary>
        public bool isFileOpened
        {
            get
            {
                return _isFileOpened;
            }
            set
            {
                _isFileOpened = value;
                RaisePropertyChanged("isFileOpened");
            }
        }

        /// <summary>
        /// Contains the file name.
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
                RaisePropertyChanged("fileName");
            }
        }

        /// <summary>
        /// Contains the file data.
        /// </summary>
        public byte[] fileData
        {
            get
            {
                return _fileData;
            }
            set
            {
                _fileData = value;
                RaisePropertyChanged("fileData");
            }
        }

        /// <summary>
        /// Contains the size of the file
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
                RaisePropertyChanged("fileSize");
            }
        }

        #endregion

        public FileBrowser()
        {
            isFileOpened = true;
        }
        /// <summary>
        /// This method allow the user to open a file brower. The file path is stocked in the attribute filePath.
        /// </summary>
        public void OpenFile()
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Multiselect = false;

            if (opf.ShowDialog() != false)
            {
                filePath = opf.FileName;
                fileName = opf.SafeFileName;
                fileTempPath = "D:\\EVE\\Temp\\vid" + fileName;

                File.Copy(filePath, fileTempPath, true);

                stream = File.OpenRead(fileTempPath);
                fileSize = stream.Length;
                isFileOpened = false;

            }

        }

        public void SaveFile()
        {
            SaveFileDialog svf = new SaveFileDialog();
           
            if (svf.ShowDialog() != false)
            {
                FileStream fs = new FileStream(svf.FileName, FileMode.Create);
                fs.Write(fileData, 0, fileData.Length);
            }
          
        }

    }
}

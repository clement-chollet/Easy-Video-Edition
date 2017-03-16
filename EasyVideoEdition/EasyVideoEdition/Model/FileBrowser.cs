using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private long _fileSize;
        private bool _canOpenFile;
        private FileStream _stream;
        #endregion

        #region Get/Set
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
        /// Indicate that a file can be opened. Or not. 
        /// </summary>
        public bool canOpenFile
        {
            get
            {
                return _canOpenFile;
            }
            set
            {
                _canOpenFile = value;
                RaisePropertyChanged("canOpenFile");
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
            canOpenFile = true;
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
                stream = File.OpenRead(filePath);

                fileSize = stream.Length;
                canOpenFile = false;
            }
        }

        /// <summary>
        /// This method allow the user to open a file brower. The file path is stocked in the attribute filePath.
        /// </summary>
        public void OpenFile(String filter)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = filter;
            opf.Multiselect = false;

            if (opf.ShowDialog() != false)
            {
                filePath = opf.FileName;
                fileName = opf.SafeFileName;
                stream = File.OpenRead(filePath);

                fileSize = stream.Length;
                canOpenFile = false;
            }
        }

        public string SaveFile(String filter)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = filter;
            sfd.AddExtension = true;
            sfd.ShowDialog();
            return sfd.FileName;
        }


        /// <summary>
        /// Reset the description of the file
        /// </summary>
        public void reset()
        {
            filePath = null;
            fileName = null;
        }
    }
}

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
        private int _startTime;
        private int _endTime;

        #endregion

        #region Get/Set
        /// <summary>
        /// Return or set the file within the element
        /// </summary>
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
        /// 
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
        /// 
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
        #endregion

        public StoryBoardElement()
        {

        }

        public StoryBoardElement(IFile file, int start, int end)
        {
            this.file = file;
            this.startTime = start;
            this.endTime = end;
        }
    }
}

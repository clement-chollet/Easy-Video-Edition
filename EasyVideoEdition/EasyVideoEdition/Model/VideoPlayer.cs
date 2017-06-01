using System;

namespace EasyVideoEdition.Model
{
    /// <summary>
    /// Class that is used to tell the source to the FFMediaElement
    /// </summary>
    class VideoPlayer : ObjectBase
    {
        public static VideoPlayer INSTANCE
        {
            get
            {
                return singleton;
            }
        }

        #region Attributes
        private static VideoPlayer singleton = new VideoPlayer();
        private String _source = "";
        #endregion

        #region Get/Set
        /// <summary>
        /// Source of the video which will be showed
        /// </summary>
        public String source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
                RaisePropertyChanged("source");
            }
        }
        #endregion

        /// <summary>
        /// Create a video player
        /// </summary>
        public VideoPlayer()
        {
        }
    }
}

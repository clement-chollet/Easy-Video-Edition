using NReco.VideoConverter;
using NReco.VideoInfo;
using System;
using System.IO;
using System.Threading;
using System.Xml.XPath;

namespace EasyVideoEdition.Model
{
    /// <summary>
    /// Class that allow the video to be notified on the player
    /// </summary>
    class MainVideo : ObjectBase
    {

        #region Attributes
        private static MainVideo singleton = new MainVideo();
        private Video _video;
        #endregion

        #region Get/Set
        /// <summary>
        /// Get the instance of the mainVideo
        /// </summary>
        public static MainVideo INSTANCE
        {
            get
            {
                return singleton;
            }
        }

        /// <summary>
        /// Gets or sets the video that is the mainVideo of the project.
        /// </summary>
        /// <value>
        /// The new video.
        /// </value>
        public Video video
        {
            get
            {
                return _video;
            }
            set
            {
                _video = value;
                RaisePropertyChanged("video");
            }
        }

        #endregion

        /// <summary>
        /// Prevents a default instance of the <see cref="MainVideo"/> class from being created.
        /// </summary>
        private MainVideo()
        {
        }

    }
}

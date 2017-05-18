using System.Windows.Threading;
using System;
using System.Collections.Generic;

namespace EasyVideoEdition.Model
{
    /// <summary>
    /// The timer used to switch between the video contained in the storyboard on the video player
    /// </summary>
    class VideoTimer
    {
        public static VideoTimer INSTANCE
        {
            get
            {
                return singleton;
            }
        }

        #region Attributes
        private static VideoTimer singleton = new VideoTimer();
        private static bool _timerIsAtStart = true;
        private DispatcherTimer myTimer = new DispatcherTimer();
        private double time = 0;
        private StoryBoard _storyBoard = StoryBoard.INSTANCE;
        private IEnumerator<StoryBoardElement> _videoList;
        private VideoPlayer _videoPlayer = VideoPlayer.INSTANCE;
        private Unosquare.FFmpegMediaElement.MediaElement _mediaEl;
        private int _timerTick = 150;
        #endregion


        #region Get/Set
        /// <summary>
        /// Get and set the storyBoard wich contain le list of video 
        /// </summary>
        public StoryBoard storyBoard
        {
            get
            {
                return _storyBoard;
            }

            set
            {
                _storyBoard = value;
            }
        }

        /// <summary>
        /// MediaElement used for the playing
        /// </summary>
        public Unosquare.FFmpegMediaElement.MediaElement mediaEl
        {
            get
            {
                return _mediaEl;
            }

            set
            {
                _mediaEl = value;
            }
        }

        /// <summary>
        /// Get and set the videoPlayer 
        /// </summary>
        public VideoPlayer videoPlayer
        {
            get
            {
                return _videoPlayer;
            }

            set
            {
                _videoPlayer = value;
            }
        }
        #endregion

        /// <summary>
        /// Method that init and start the timer. Also launch the video playing
        /// </summary>
        public void startTimer()
        {
            if (_timerIsAtStart)
            {
                Console.WriteLine("INITIALISATION");
                // Sets the timer interval.
                myTimer.Interval = TimeSpan.FromMilliseconds(_timerTick);
                myTimer.Tick += timer_tick;
                if (storyBoard.fileList.Count > 0)
                {
                    _timerIsAtStart = false;
                    _videoList = storyBoard.fileList.GetEnumerator();
                    _videoList.MoveNext();
                    myTimer.Start();
                }
            }
            else
            {
                myTimer.IsEnabled = true;
            }
            if (storyBoard.fileList.Count > 0)
                mediaEl.Play();

        }

        /// <summary>
        /// Pause the video and the timer
        /// </summary>
        public void pauseTimer()
        {
            Console.WriteLine("pause");
            myTimer.Stop();
            mediaEl.Pause();
        }

        /// <summary>
        /// Stop the timer and the video. The video return to the start
        /// </summary>
        public void stopTimer()
        {
            Console.WriteLine("stop");
            time = 0;
            myTimer.Stop();
            _timerIsAtStart = true;
            myTimer = new DispatcherTimer();
            mediaEl.Stop();
            _videoList = storyBoard.fileList.GetEnumerator();

            if (_videoList.MoveNext())
            {
                videoPlayer.source = _videoList.Current.filePath;
                Console.WriteLine("TEST-------------------------------------------" + _videoList.Current.fileName);
            }


        }
        
        /// <summary>
        /// Actualize the source of the mediaPlayer each 10 milliseconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_tick(object sender, EventArgs e)
        {
            time += _timerTick;
            if (storyBoard.fileList.Count > 0)
            {
                Console.WriteLine("Temps actuel du timer" + time.ToString());
                Console.WriteLine(_videoList.Current.fileName);
                Console.WriteLine("Temps total video : " + _videoList.Current.endTime.TotalMilliseconds.ToString());
                if (time > _videoList.Current.endTime.TotalMilliseconds)
                {
                    Console.WriteLine("Changement de source de la video");
                    if (_videoList.MoveNext())
                    {
                        videoPlayer.source = _videoList.Current.filePath;
                        mediaEl.Play();
                    }
                    else
                    {
                        Console.WriteLine("Fin de la vidéo");
                        stopTimer();
                        myTimer = new DispatcherTimer();
                        Console.WriteLine("stop; TIME : " + time);
                    }
                }
            }
        }

        /// <summary>
        /// Tranfert the mediaElement 
        /// </summary>
        /// <param name="mediaElt"></param>
        public void transferMediaElt(ref Unosquare.FFmpegMediaElement.MediaElement mediaElt)
        {
            this.mediaEl = mediaElt;
        }
    }
}
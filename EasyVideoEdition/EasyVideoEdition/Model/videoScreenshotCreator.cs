using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EasyVideoEdition.Model
{
    /// <summary>
    /// Class to create a thumbnail for the video added in the project.
    /// It's actualy broken with some codec of video due to the MediaPlayer. TO BE REWORKED
    /// </summary>
    class videoScreenshotCreator
    {
        // Create a screenshot of the video
        #region Attributes
        VideoDrawing vd = new VideoDrawing();
        MediaPlayer m_Player;
        private static byte[] _byteArrayImage;
        private string videoName;
        #endregion

        #region Get/Set
        /// <summary>
        /// Return the screenshot of the last video passed to the class
        /// </summary>
        public static byte[] byteArrayImage
        {
            get
            {
                return _byteArrayImage;
            }
            set
            {
                _byteArrayImage = value;
            }
        }
        #endregion

        /// <summary>
        /// Create a thumbmail of a video.
        /// </summary>
        /// <param name="videoFilePath">Path of the video</param>
        /// <param name="videoName">Name of the video. Will be used to name the Thumbmail</param>
        public videoScreenshotCreator(String videoFilePath, String videoName)
        {
            this.videoName = videoName;
            m_Player = new MediaPlayer();
            m_Player.Open(new Uri(videoFilePath));

            vd.Rect = new Rect(0, 0, 120, 100);
            vd.Player = m_Player;

            m_Player.MediaOpened += new EventHandler(player_MediaOpened);

            m_Player.Play();
            m_Player.Position = TimeSpan.FromSeconds(50);

        }

        /// <summary>
        /// Method that creates the Screenshot of the video.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player_MediaOpened(object sender, EventArgs e)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            drawingContext.DrawVideo(m_Player, new Rect(new Point(0, 0), new Point(120, 100)));
            drawingContext.Close();

            RenderTargetBitmap rtb = new RenderTargetBitmap(120, 100, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(drawingVisual);

            m_Player.Stop();

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            MemoryStream ms = new MemoryStream();
            encoder.Save(ms);

            var byteArrayImage = ms.ToArray();

            //File.WriteAllBytes("D:\\Eve\\Temp\\Screenshot\\" + videoName.Split('.')[0] + ".jpeg", byteArrayImage);
        }
    }
}

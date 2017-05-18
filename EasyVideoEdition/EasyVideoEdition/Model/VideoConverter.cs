using NReco.VideoConverter;
using NReco.VideoInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.XPath;

namespace EasyVideoEdition.Model
{
    /// <summary>
    /// Class that contains the method to convert and export the video
    /// Singleton
    /// </summary>
    class VideoConverter : ObjectBase
    {
        #region Attributes
        private static VideoConverter singleton = new VideoConverter();

        private FFMpegConverter ffMpegConverter = new FFMpegConverter();

        private double _processedCalc;
        private String _processedCalcLabel;
        private double _newDuration = 0;

        //Used for conversion progress
        private double _totalConvertDuration = 0;
        private double _offsetConvertProgress = 0;
        #endregion

        #region Get/Set
        /// <summary>
        /// Get the instance of the class
        /// </summary>
        public static VideoConverter INSTANCE
        {
            get
            {
                return singleton;
            }

            set
            {
                singleton = value;
            }
        }

        /// <summary>
        /// Progression of the concat operation in percentage. Used in progressBar
        /// </summary>
        public double processedCalc
        {
            get
            {
                return _processedCalc;
            }
            set
            {
                _processedCalc = value;
                RaisePropertyChanged("processedCalc");
            }
        }

        /// <summary>
        /// Progression of the concat operation, in percentage with the % after. Used in TextBloc
        /// </summary>
        public String processedCalcLabel
        {
            get
            {
                return _processedCalcLabel;
            }
            set
            {
                _processedCalcLabel = value;
                RaisePropertyChanged("processedCalcLabel");
            }
        }

        #endregion

        /// <summary>
        /// Export a list of video into one video file
        /// </summary>
        /// <param name="width">Output video width</param>
        /// <param name="height">Output video framerate</param>
        /// <param name="frameRate">Output framerate</param>
        /// <param name="outputVideoCodec">output video codec for the new video</param>
        /// <param name="outputAudioCoedc">output output codec for the new video</param>
        /// <param name="videoList">list of video to process</param>
        /// <param name="savePath">Where to save the new video</param>
        public void exportVideoStart(int width, int height, int frameRate, string outputVideoCodec, string outputAudioCodec, IEnumerator<StoryBoardElement> videoList, String savePath)
        {
            int nbVideo = 0;
            while (videoList.MoveNext())
            {
                nbVideo++;
                StoryBoardElement e = (StoryBoardElement)videoList.Current;
                _totalConvertDuration += e.file.duration;
            }
            
            if(nbVideo > 1)
                _totalConvertDuration *= 2;
            videoList.Reset();

            Thread convertThread = new Thread(() =>
            {
                List<Video> videoArray = new List<Video>();
                //-------------------------
                //Convertion of the video file 
                while (videoList.MoveNext())
                {
                    StoryBoardElement ele = videoList.Current;
                    if (ele.fileType.Equals("Video"))
                    {
                        Video v = (Video)ele.file;
                        convertVideo(width, height, frameRate, outputVideoCodec, outputAudioCodec, v);
                        //update of the path in the file list
                        ele.filePath = v.filePath;
                        _offsetConvertProgress += v.duration;
                        videoArray.Add(v);
                    }
                }
                videoList.Reset();
                //End of convertion
                //-------------------------
                if (nbVideo > 1)
                {
                    Video FINAL = concatVideoArray(videoArray, width, height, frameRate, outputVideoCodec, savePath);
                }
                MessageBox.Show("Export de la vidéo termniné !");
            });
            convertThread.Start();
        }

        /// <summary>
        /// Convert the video to a new format.
        /// </summary>
        /// <param name="width">Output video width</param>
        /// <param name="height">Output video framerate</param>
        /// <param name="frameRate">Output framerate</param>
        /// <param name="outputVideoCodec">output video codec for the new video</param>
        /// <param name="outputAudioCoedc">output output codec for the new video</param>
        private void convertVideo(int width, int height, int frameRate, string outputVideoCodec, string outputAudioCodec, Video videoToConvert)
        {
            String dir = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            String newPath = dir + "/" + videoToConvert.fileName + ".mp4";
            var setting = new ConvertSettings();

            setting.SetVideoFrameSize(width, height);
            setting.VideoFrameRate = frameRate;
            setting.VideoCodec = outputVideoCodec;
            setting.AudioCodec = outputAudioCodec;

            var converter = new FFMpegConverter();
            ffMpegConverter.ConvertProgress += updateProgress;
            ffMpegConverter.ConvertMedia(videoToConvert.filePath,
            null, // autodetect by input file extension 
            newPath,
            null, // autodetect by output file extension 
            setting
            );

            videoToConvert.filePath = newPath;
        }

        /// <summary>
        /// Concat an array of video into one video
        /// </summary>
        /// <param name="videoArray"> Array to concat</param>
        /// <param name="width">Output video width</param>
        /// <param name="height">Output video framerate</param>
        /// <param name="frameRate">Output framerate</param>
        /// <param name="outputVideoCodec">output video codec for the new video</param>
        /// <param name="savePath">Where to save the new video</param>
        /// <returns></returns>
        private Video concatVideoArray(List<Video> videoArray, int width, int height, int frameRate, string outputVideoCodec, string savePath)
        {
            IEnumerator<Video> videoEnum = videoArray.GetEnumerator();
            String[] inputVideoPaths = new String[videoArray.Count];
            String outputVideoPath = savePath;

            long outputSize = 0;
            int i = 0;
            while (videoEnum.MoveNext())
            {
                inputVideoPaths.SetValue(videoEnum.Current.filePath, i);
                outputSize += videoEnum.Current.fileSize;
                _newDuration += videoEnum.Current.duration;
                i++;
            }

            var concatSetting = new ConcatSettings();
            concatSetting.ConcatVideoStream = true;
            concatSetting.ConcatAudioStream = false;
            concatSetting.VideoCodec = outputVideoCodec;
            concatSetting.VideoFrameRate = frameRate;
            concatSetting.SetVideoFrameSize(width, height);

            ffMpegConverter.ConvertProgress += updateProgress;
            ffMpegConverter.ConcatMedia(inputVideoPaths, outputVideoPath, Format.mp4, concatSetting);
            return new Video(outputVideoPath, "final", outputSize);

        }

        /// <summary>
        /// Concat a second video at the end of the main video. 
        /// (HERE FOR EXEMPLE) NOT USED
        /// </summary>
        /// <param name="secondVideo">The second video to add at the end of the main one.</param>
        private Video concatTwoVideos(Video firstVideo, Video secondVideo)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            /* Settings needed to concatenate the videos */
            String[] inputVideoPaths = new String[2];
            String outputVideoPath = firstVideo.filePath;
            String outputName = firstVideo.fileName;
            String newPath = firstVideo.filePath + "_finalH264.avi";
            String commandConcat = "- i \"concat:" + firstVideo.filePath + "|" + secondVideo.filePath + "\" - codec copy " + outputVideoPath;
            long outputSize = firstVideo.fileSize + secondVideo.fileSize;

            inputVideoPaths[0] = firstVideo.filePath;
            inputVideoPaths[1] = secondVideo.filePath;

            /* Calcul of the new video duration in seconds */
            var ffProbe = new FFProbe();
            var videoInfo_1 = ffProbe.GetMediaInfo(firstVideo.filePath);
            var videoInfo_2 = ffProbe.GetMediaInfo(secondVideo.filePath);

            XPathNavigator nav = videoInfo_1.Result.CreateNavigator();
            System.IO.File.WriteAllText("/text.xml", nav.InnerXml);

            _newDuration = videoInfo_1.Duration.TotalSeconds + videoInfo_2.Duration.TotalSeconds;

            var concatSetting = new NReco.VideoConverter.ConcatSettings();

            concatSetting.ConcatVideoStream = true;
            concatSetting.ConcatAudioStream = false;
            concatSetting.VideoCodec = "h264";
            concatSetting.VideoFrameRate = 24;
            concatSetting.SetVideoFrameSize(1280, 720);

            ffMpegConverter.ConvertProgress += updateProgress;
            ffMpegConverter.ConcatMedia(inputVideoPaths, newPath, Format.avi, concatSetting);
            watch.Stop();
            var el = watch.ElapsedMilliseconds;
            Directory.GetCurrentDirectory();
            System.IO.File.AppendAllText("/duration.txt", "\nFPS: " + concatSetting.VideoFrameRate + " || Codec: " + concatSetting.VideoCodec + " || Time : " + el + " millis. No Conv");

            return new Video(newPath, "final", outputSize);

        }

        /// <summary>
        /// Updates the progress of the process (use for the progress bar).
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ConvertProgressEventArgs"/> instance containing the event data.</param>
        private void updateProgress(object sender, ConvertProgressEventArgs e)
        {
            processedCalc = Math.Round(((e.Processed.TotalMilliseconds + _offsetConvertProgress) * 100) / _totalConvertDuration);
            processedCalcLabel = processedCalc + " %";
        }
    }
}

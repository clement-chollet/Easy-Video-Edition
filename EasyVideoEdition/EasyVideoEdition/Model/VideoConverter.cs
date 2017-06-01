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
using System.Diagnostics;

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

        private FFProbe ffMpegInfo = new FFProbe();

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
                        if (ele.hasToBeSplit == true)
                        {
                            v.getDirectory();
                            v.getExtension();
                            v.getFileName();
                            String videoPartName = v.fileName + "_part" + ele.numPart + "." + v.extension;
                            String videoPartPath = v.directory + videoPartName;
                            if (File.Exists(videoPartPath))
                            {
                                File.Delete(videoPartPath);
                            }
                            v = splitVideo((Video)ele.file, ele.numPart, ele.startTimeInSource, ele.endTimeInSource); //extract the part from the original video

                        }
                        else
                        {
                            v = (Video)ele.file;
                        }


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

                
                StoryBoard.INSTANCE.subtitles.srtPath = StoryBoard.INSTANCE.subtitles.CreateSrtFileName(savePath);

                StoryBoard.INSTANCE.addSubtitleToStory(TimeSpan.FromMilliseconds(1000), TimeSpan.FromMilliseconds(5000), "sous-titre test");
                StoryBoard.INSTANCE.subtitles.CreateSrtFile();

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
        /// (not used in final processing. Can be easily use to test concatenation with fixed settings)
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
        /// Split the video into a smaller video
        /// </summary>
        /// <param name="originalVideo">original video to split</param>
        /// <param name="nbPart">number of the part</param>
        /// <param name="startH">start of splitted part</param>
        /// <param name="end">end of the splitted part</param>
        /// <returns>the part of the video contained into the start time and the end time</returns>
        public Video splitVideo(Video originalVideo, int nbPart, TimeSpan start, TimeSpan end)
        {
            Video splittedVideo;

            String startMilli = "";
            String endMilli = "";

            if(start.Milliseconds < 10)
            {
                startMilli = start.Milliseconds + "00";
            }
            else if(start.Milliseconds < 100)
            {
                startMilli = start.Milliseconds + "0";
            }
            else if(start.Milliseconds >= 100)
            {
                startMilli = start.Milliseconds.ToString();
            }

            if (end.Milliseconds < 10)
            {
                endMilli = end.Milliseconds + "00";
            }
            else if (end.Milliseconds < 100)
            {
                endMilli = end.Milliseconds + "0";
            }
            else if (end.Milliseconds >= 100)
            {
                endMilli = end.Milliseconds.ToString();
            }

            String startH = start.Hours.ToString();
            if (start.Hours < 10) { startH = "0" + start.Hours.ToString(); }
            String startM = start.Minutes.ToString();
            if (start.Minutes < 10) { startM = "0" + start.Minutes.ToString(); }
            String startS = start.Seconds.ToString() + "." + startMilli;
            if (start.Seconds < 10) { startS = "0" + start.Seconds.ToString() + "." + startMilli; }
            String endH = end.Hours.ToString();
            if (end.Hours < 10) { endH = "0" + end.Hours.ToString(); }
            String endM = end.Minutes.ToString();
            if (end.Minutes < 10) { endM = "0" + end.Minutes.ToString(); }
            String endS = end.Seconds.ToString() + "." + endMilli;
            if (end.Seconds < 10) { endS = "0" + end.Seconds.ToString() + "." + endMilli; }


            Console.WriteLine(start);
            Console.WriteLine(end);
            Console.WriteLine("split from => " + startH + " : " + startM + " : " + startS);
            Console.WriteLine(" to =>" + endH + " : " + endM + " : " + endS);

            originalVideo.getDirectory();
            originalVideo.getExtension();
            originalVideo.getFileName();

            var videoInfo = ffMpegInfo.GetMediaInfo(originalVideo.filePath);

            String videoPartName = originalVideo.fileName + "_part" + nbPart + "." + originalVideo.extension;
            String videoPartPath = originalVideo.directory + videoPartName;

            Thread threadPart = new Thread(() =>
                videoSpliterThreaded(originalVideo, startH, startM, startS, endH, endM, endS, videoPartPath)
            );

            threadPart.Start();
            threadPart.Join();

            splittedVideo = new Video(videoPartPath, videoPartName, 0);

            return splittedVideo;
        }

        /// <summary>
        /// split a part of a video (use in threads)
        /// </summary>
        /// <param name="originalVideo">original video to split</param>
        /// <param name="startH">start of the video (hours)</param>
        /// <param name="startM">start of the video (minutes)</param>
        /// <param name="startS">start of the video (seconds)</param>
        /// <param name="endH">end of the video (hours)</param>
        /// <param name="endM">end of the video (minutes)</param>
        /// <param name="endS">end of the video (seconds)</param>
        /// <param name="videoPartPath">path of the splitted video</param>
        private void videoSpliterThreaded(Video originalVideo, String startH, String startM, String startS, String endH, String endM, String endS, String videoPartPath)
        {
            /* Initializing the process */
            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = "ffmpeg";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;


            process.StartInfo.Arguments = "-i \"" + originalVideo.filePath + "\" -vcodec copy -acodec copy -ss " + startH + ":" + startM + ":" + startS + " -t " + endH + ":" + endM + ":" + endS + " \"" + videoPartPath + "\"";

            if (!process.Start())
            {
                Console.WriteLine("Error starting");
            }

            StreamReader reader = process.StandardError;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(videoPartPath + " | " + line);
            }
            process.Close();
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

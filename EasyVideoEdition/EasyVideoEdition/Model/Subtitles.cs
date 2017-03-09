using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;

namespace EasyVideoEdition.Model
{
    /// <summary>
    /// Every subtitles of a video in srt format
    /// </summary>
    class Subtitles : ObjectBase
    {
        #region Attributes
        private ObservableCollection<Subtitle> _subtitlesCollection;
        private String _srtPath;
        private static readonly Regex TIME_FORMAT = new Regex(@"^\d{2}:\d{2}:\d{2},\d{3}$");                                     //Format of a time (hh:mm:ss,msmsms)

        #endregion

        #region Get/Set
        /// <summary>
        /// Path of the srt file
        /// </summary>
        public String srtPath
        {
            get
            {
                return _srtPath;
            }
            set
            {
                _srtPath = value;
                RaisePropertyChanged("srtPath");
            }
        }
        /// <summary>
        /// List of Subtitles of a video
        /// </summary>
        public ObservableCollection<Subtitle> subtitlesCollection
        {
            get
            {
                return _subtitlesCollection;
            }
            set
            {
                _subtitlesCollection = value;
                RaisePropertyChanged("subtitlesCollection");
            }
        }
        #endregion

        /// <summary>
        /// Creates the subtitles of the video
        /// </summary>
        /// <param name="srtPath">Path of the video</param>
        public Subtitles(String srtPath)
        {
            this.srtPath = CreateSrtFileName(srtPath);
            subtitlesCollection = new ObservableCollection<Subtitle>();
        }

        /// <summary>
        /// Create the file name with the path
        /// </summary>
        /// <param name="originalPath">Path of the video file</param>
        /// <returns>Path of the srt file</returns>
        public String CreateSrtFileName(String originalPath)
        {
            String filePath;
            int endIndex = originalPath.Length - 1;
            /* Remove the extension of the pah searching the '.' at the end */
            while (originalPath[endIndex] != '.')
            {
                endIndex--;
            }
            filePath = originalPath.Remove(endIndex + 1);                                                                        //Remove chars from the '.' to the end
            filePath = filePath + "srt";                                                                                         //Write srt after the '.' in the file path

            return filePath;
        }

        /// <summary>
        /// Add a subtitle to the video
        /// </summary>
        /// <param name="startTime">Time when the subtitle will start</param>
        /// <param name="endTime">Time when the subtitle will end</param>
        /// <param name="text">Text that the subtitle will contain</param>
        public void AddSubtitle(String startTime, String endTime, String text)
        {
            /* Check if the format of the strings are good */
            if (TIME_FORMAT.IsMatch(startTime) && TIME_FORMAT.IsMatch(endTime))
            {
                Subtitle sub = new Subtitle(startTime, endTime, text);
                subtitlesCollection.Add(sub);
            }
            else
            {
                MessageBox.Show("Syntaxe des temps incorrecte");
            }
        }

        /// <summary>
        /// Edit a subtitle of the video
        /// </summary>
        /// <param name="index">Index of the subtitle in the list</param>
        /// <param name="startTime">Time when the subtitle will start</param>
        /// <param name="endTime">Time when the subtitle will end</param>
        /// <param name="text">Text that the subtitle will contain</param>
        public void EditSubtitle(int index, String startTime, String endTime, String text)
        {
            if (subtitlesCollection.Count > index)
            {
                subtitlesCollection[index].EditSubtile(startTime, endTime, text);
            }

        }

        /// <summary>
        /// Delete a subtitle
        /// </summary>
        /// <param name="index">Index of the subtitle in the list</param>
        public void DeleteSubtitle(int index)
        {
            if (subtitlesCollection.Count > index)
            {
                subtitlesCollection.RemoveAt(index);
            }
        }

        /// <summary>
        /// Create the srt file corresponding to the video
        /// </summary>
        public void CreateSrtFile()
        {
            int i = 0;
            int indexSubtitle = 1;
            String[] subtitleFormat = new String[subtitlesCollection.Count * 3];

            /* Write all subtitles in the format of srt file */
            foreach (Subtitle sub in subtitlesCollection)
            {
                subtitleFormat[i + 0] = indexSubtitle.ToString();                                                                  //Write the index (number) of the subtitle
                subtitleFormat[i + 1] = sub.startTime + " --> " + sub.endTime;                                                     //Write the both start and end time with the good syntaxe
                subtitleFormat[i + 2] = sub.subtitleText + "\n";                                                                   //Write the content of the subtitle
                i += 3;
                indexSubtitle++;


            }
            File.WriteAllLines(srtPath, subtitleFormat);
        }

        /// <summary>
        /// Read a srt file and put all subtitles in the collection of subtitles
        /// </summary>
        public void ReadSrtFile()
        {
            String line;
            String textRead;
            String[] times = new String[2];                                                                                      //Both start time and end time for each subtitles
            Subtitle sub;
            int index = 1;
            int ret = 0;

            subtitlesCollection.Clear();                                                                                         //Clear the collection of subtitles
           

            if (!File.Exists(this.srtPath))
                File.Create(this.srtPath).Close();

            StreamReader file = new StreamReader(this.srtPath);
            while ((line = file.ReadLine()) != null)
            {
                if (int.TryParse(line, out ret))
                {
                    sub = new Subtitle();
                    times = new String[2];

                    /* Read both start and end times */
                    if ((line = file.ReadLine()) != null)
                    {
                        times = Regex.Split(line, " --> ");                                                                      //Split the line containing the times
                    }

                    /* Read the text of the subtitle and add the subtitle to the collection of subtitles*/
                    if ((line = file.ReadLine()) != null)
                    {
                        textRead = line;
                        sub.EditSubtile(times[0], times[1], textRead);
                        subtitlesCollection.Add(sub);
                        index++;
                    }
                }

            }
        }

        /// <summary>
        /// Print all subtitles in MessageBox
        /// </summary>
        public void PrintAllSubtitles()
        {
            MessageBox.Show("---------------------------");
            foreach (Subtitle sub in subtitlesCollection)
            {
                MessageBox.Show(sub.startTime + sub.endTime + sub.subtitleText);
            }

        }



    }
}

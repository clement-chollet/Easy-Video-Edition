using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EasyVideoEdition.Model
{
    /// <summary>
    /// Class that define the storyboard for the video editor
    /// THE ONLY STORYBOARD INSTANCE THAT SHOULD BE USE IS THE SINGLETON. DO NOT USE THIS CONSTRUCTOR !!! 
    /// IT IS PUBLIC DUE TO THE JSON PARSER !!!
    /// </summary>
    class StoryBoard : ObjectBase
    {

        /// <summary>
        /// Get the instance of the viewModel
        /// </summary>
        public static StoryBoard INSTANCE
        {
            get
            {
                return singleton;
            }
        }

        #region Attributes
        private ObservableCollection<StoryBoardElement> _fileList = new ObservableCollection<StoryBoardElement>();
        private static StoryBoard singleton = new StoryBoard();
        private TimeSpan _duration = TimeSpan.FromMilliseconds(0);
        private Subtitles _subtitles = new Subtitles(null);
        #endregion

        #region Get/Set
        /// <summary>
        /// List of the file (photo or video) within the storyBoard (sorted)
        /// </summary>
        public ObservableCollection<StoryBoardElement> fileList
        {
            get
            {
                return _fileList;
            }
            set
            {
                RaisePropertyChanged("fileList");
                _fileList = value;
            }
        }

        /// <summary>
        /// Duration of the entire storyboard
        /// </summary>
        public TimeSpan duration
        {
            get
            {
                return _duration;
            }

            set
            {
                _duration = value;
            }
        }

        /// <summary>
        /// Subtitles of the video
        /// </summary>
        public Subtitles subtitles
        {
            get
            {
                return _subtitles;
            }

            set
            {
                _subtitles = value;
            }
        }

        #endregion

        /// <summary>
        /// Add a file to the storyboard
        /// </summary>
        /// <param name="fileToAdd"></param>
        public void addFile(IFile fileToAdd, TimeSpan startTime, TimeSpan endTime, string type)
        {
            _fileList.Add(new StoryBoardElement(fileToAdd, startTime, endTime, type, fileToAdd.fileName, fileToAdd.fileSize));
        }

        /// <summary>
        /// Add a file to the storyboard
        /// </summary>
        /// <param name="fileToAdd"></param>
        public void addFile(IFile fileToAdd, TimeSpan startTime, TimeSpan endTime, TimeSpan startTimeSource, TimeSpan endTImeSource, string type)
        {
            _fileList.Add(new StoryBoardElement(fileToAdd, startTime, endTime, startTimeSource, endTImeSource, type, fileToAdd.fileName, fileToAdd.fileSize));
        }



        /// <summary>
        /// Insert the given storyboard element into another one
        /// </summary>
        /// <param name="eltToInsert"></param>
        /// <param name="insertTime"></param>
        public void insertInto(StoryBoardElement eltToInsert, TimeSpan insertTime)
        {
            StoryBoardElement elt;
            TimeSpan st = insertTime;
            TimeSpan et = TimeSpan.FromMilliseconds(insertTime.TotalMilliseconds + (eltToInsert.endTimeInSource.TotalMilliseconds - eltToInsert.startTimeInSource.TotalMilliseconds));
            TimeSpan stSource = eltToInsert.startTimeInSource;
            TimeSpan etSource = eltToInsert.endTimeInSource;
            TimeSpan durationUpdate;
            IEnumerator<StoryBoardElement> videoList = fileList.GetEnumerator();
            Boolean isSame = false;
            int indexInsert = -1;
            int indexSplit = -1;

            int index = 0;
            while (videoList.MoveNext())
            {
                elt = videoList.Current;
                if (insertTime > elt.startTime && insertTime < elt.endTime)
                {
                    if (elt != eltToInsert)
                    {
                        indexSplit = getIndex(elt);
                        indexInsert = getIndex(eltToInsert);

                        fileToSplit(elt, elt.startTime, insertTime, insertTime, elt.endTime);
                        sortFileList();

                        /* the storyboard element if inserting after its old position */
                        if ((indexSplit != -1 && indexInsert != -1) && (indexSplit < indexInsert))
                        {
                            Console.WriteLine("split < insert");
                            durationUpdate = TimeSpan.FromMilliseconds((eltToInsert.endTimeInSource.TotalMilliseconds - eltToInsert.startTimeInSource.TotalMilliseconds));
                            updateTimesBetween(indexSplit + 1, indexInsert + 1, durationUpdate, false);
                            eltToInsert.editTimes(st, et, stSource, etSource);
                        }
                        /* the storyboard element if inserting before its old position */
                        else if ((indexSplit != -1 && indexInsert != -1) && (indexSplit > indexInsert))
                        {
                            Console.WriteLine("split > insert");
                            eltToInsert.editTimes(st, et, stSource, etSource);
                            durationUpdate = TimeSpan.FromMilliseconds((eltToInsert.endTimeInSource.TotalMilliseconds - eltToInsert.startTimeInSource.TotalMilliseconds));
                            updateTimesBetween(indexInsert, indexSplit, durationUpdate, true);
                        }


                        Console.WriteLine("!!!INSERT!!!");
                        sortFileList();
                    }
                    else
                    {
                        isSame = true;
                    }
                    break;
                }
                index++;
            }

            if (isSame == false)
            {
                Console.WriteLine("!!!INSERT!!!");
            }


        }

        /// <summary>
        /// split a storyBoard element in two parts
        /// </summary>
        /// <param name="elt">storyboard element to split</param>
        /// <param name="stPart1">start time of the first part</param>
        /// <param name="etPart1">end time of the first part</param>
        /// <param name="stPart2">start time of the second part</param>
        /// <param name="etPart2">end time of the second part</param>
        public void fileToSplit(StoryBoardElement elt, TimeSpan stPart1, TimeSpan etPart1, TimeSpan stPart2, TimeSpan etPart2)
        {
            StoryBoardElement elt2;

            /* First part */
            TimeSpan stSource1;

            stSource1 = elt.startTimeInSource;
            TimeSpan etSource1 = calcEndInSource(stPart1, etPart1, stSource1);
            elt.editTimes(stPart1, etPart1, stSource1, etSource1);
            elt.hasToBeSplit = true;
            //elt.calcDuration();

            /* Second part */
            TimeSpan stSource2 = etSource1;
            TimeSpan etSource2 = calcEndInSource(stPart2, etPart2, stSource2);
            elt2 = new StoryBoardElement(elt.file, stPart2, etPart2, stSource2, etSource2, elt.fileType, elt.fileName, elt.fileSize);
            elt2.hasToBeSplit = true;
            duration = duration.Add(TimeSpan.FromMilliseconds(etPart2.TotalMilliseconds - stPart2.TotalMilliseconds));
            elt2.numPart = elt2.numPart + 1;
            //elt2.calcDuration();
            fileList.Add(elt2);


        }

        /// <summary>
        /// Cut a video a part from timeFrom to timeTo
        /// </summary>
        /// <param name="timeFrom">cut start time</param>
        /// <param name="timeTo">cut end time</param>
        public void cutFromTo(TimeSpan timeFrom, TimeSpan timeTo)
        {
            Console.WriteLine("CUT FROM TO");
            StoryBoardElement elt;
            TimeSpan durationUpdate;
            IEnumerator<StoryBoardElement> videoList = fileList.GetEnumerator();
            List<StoryBoardElement> eltToAdd = new List<StoryBoardElement>();

            int index = 0;
            while (videoList.MoveNext())
            {
                if (timeTo == timeFrom)
                {
                    break;
                }
                elt = videoList.Current;
                Console.WriteLine("__ elt : " + elt.startTime + " -> " + elt.endTime);
                /* Cut the end of the storyboard element */
                if ((timeFrom > elt.startTime && timeFrom < elt.endTime) && (timeTo >= elt.endTime))
                {
                    Console.WriteLine("___ cut end (" + timeFrom + " -> " + timeTo + ") index : " + index + "___");
                    durationUpdate = TimeSpan.FromMilliseconds(elt.endTime.TotalMilliseconds - timeFrom.TotalMilliseconds);
                    cutEnd(elt, timeFrom);
                    updateTimesAfter(index + 1, durationUpdate, true);
                    timeTo = TimeSpan.FromMilliseconds(timeTo.TotalMilliseconds - durationUpdate.TotalMilliseconds);
                    duration = duration - durationUpdate;

                }
                /* Cut the middle of the storyboard element */
                else if ((timeFrom > elt.startTime && timeFrom < elt.endTime) && (timeTo > elt.startTime && timeTo < elt.endTime))
                {
                    Console.WriteLine("___ cut middle (" + timeFrom + " -> " + timeTo + ") index : " + index + "___");
                    durationUpdate = TimeSpan.FromMilliseconds(timeTo.TotalMilliseconds - timeFrom.TotalMilliseconds);
                    StoryBoardElement[] lst = cutMiddle(elt, timeFrom, timeTo);
                    elt = lst[0];
                    fileList.Add(lst[1]);
                    sortFileList();
                    updateTimesAfter(index + 2, durationUpdate, true);
                    timeTo = TimeSpan.FromMilliseconds(timeTo.TotalMilliseconds - durationUpdate.TotalMilliseconds);
                    duration = duration - durationUpdate;
                    cutFromTo(timeFrom, timeTo);
                    break;

                }
                /* Cut the entire storyboard element */
                else if ((timeFrom <= elt.startTime) && (timeTo >= elt.endTime))
                {
                    Console.WriteLine("___ cut entire (" + timeFrom + " -> " + timeTo + ") index : " + index + "___");
                    durationUpdate = TimeSpan.FromMilliseconds(elt.endTime.TotalMilliseconds - elt.startTime.TotalMilliseconds);
                    fileList.RemoveAt(index);
                    index--;
                    updateTimesAfter(index + 1, durationUpdate, true);
                    timeTo = TimeSpan.FromMilliseconds(timeTo.TotalMilliseconds - durationUpdate.TotalMilliseconds);
                    duration = duration - durationUpdate;
                    cutFromTo(timeFrom, timeTo);
                    break;

                }
                /* Cut the begining of the storyboard element */
                else if ((timeFrom <= elt.startTime) && (timeTo > elt.startTime && timeTo < elt.endTime))
                {
                    Console.WriteLine("___ cut begining (" + timeFrom + " -> " + timeTo + ") index : " + index + "___");
                    durationUpdate = TimeSpan.FromMilliseconds(timeTo.TotalMilliseconds - elt.startTime.TotalMilliseconds);
                    cutBegin(elt, timeFrom, timeTo);
                    timeTo = TimeSpan.FromMilliseconds(timeTo.TotalMilliseconds - durationUpdate.TotalMilliseconds);
                    updateTimesAfter(index + 1, durationUpdate, true);
                    duration = duration - durationUpdate;
                }
                Console.WriteLine("__ updated elt : " + elt.startTime + " -> " + elt.endTime);

                index++;
            }

        }

        /// <summary>
        /// Cut the end of the storyboard element from a given time 
        /// </summary>
        /// <param name="index">the storyBoard element to cut</param>
        /// <param name="timeFrom">cut start time</param>
        public void cutEnd(StoryBoardElement elt, TimeSpan timeFrom)
        {
            TimeSpan durationToKeep = TimeSpan.FromMilliseconds(timeFrom.TotalMilliseconds - elt.startTime.TotalMilliseconds);
            TimeSpan etSource = TimeSpan.FromMilliseconds(elt.startTimeInSource.TotalMilliseconds + durationToKeep.TotalMilliseconds);
            elt.editTimes(elt.startTime, timeFrom, elt.startTimeInSource, etSource);

        }

        /// <summary>
        /// Cut the beginning of the storyboard element to a given time 
        /// </summary>
        /// <param name="index">the storyBoard element to cut</param>
        /// <param name="timeFrom">time to start the updated element</param>
        /// <param name="timeTo">cut end time</param>
        public void cutBegin(StoryBoardElement elt, TimeSpan timeFrom, TimeSpan timeTo)
        {
            Console.WriteLine("time begin : ");
            TimeSpan durationToRemove = TimeSpan.FromMilliseconds(timeTo.TotalMilliseconds - elt.startTime.TotalMilliseconds);
            TimeSpan et = TimeSpan.FromMilliseconds(elt.endTime.TotalMilliseconds - durationToRemove.TotalMilliseconds);
            TimeSpan stSource = TimeSpan.FromMilliseconds(elt.startTimeInSource.TotalMilliseconds + durationToRemove.TotalMilliseconds);
            TimeSpan etSource = elt.endTimeInSource;

            elt.editTimes(timeFrom, et, stSource, etSource);

        }

        /// <summary>
        /// Cut the middle of the storyboard element from a given time to another given time
        /// </summary>
        /// <param name="index">the storyBoard element to cut</param>
        /// <param name="timeFrom">cut start time</param>
        /// <param name="timeTo">cut end time</param>
        public StoryBoardElement[] cutMiddle(StoryBoardElement elt, TimeSpan timeFrom, TimeSpan timeTo)
        {
            TimeSpan stSource1; //start in source part 1
            TimeSpan etSource1; //end int source part 1
            TimeSpan st2; //start in storyboard part 2
            TimeSpan et2; //end in storyboard part 2
            TimeSpan stSource2; //start in source part 2
            TimeSpan etSource2; //end in source part 2

            StoryBoardElement elt1;
            StoryBoardElement elt2;

            StoryBoardElement[] updatedElts = new StoryBoardElement[2];
            TimeSpan durationCut = TimeSpan.FromMilliseconds(timeTo.TotalMilliseconds - timeFrom.TotalMilliseconds);

            stSource1 = elt.startTimeInSource;
            etSource1 = calcEndInSource(elt.startTime, timeFrom, stSource1); //calculates the endSource time of the first element

            elt1 = new StoryBoardElement(elt.file, elt.startTime, timeFrom, elt.startTimeInSource, etSource1, elt.fileType, elt.fileName, elt.fileSize);
            elt1.hasToBeSplit = true;

            st2 = elt1.endTime;
            et2 = TimeSpan.FromMilliseconds(elt.endTime.TotalMilliseconds - durationCut.TotalMilliseconds);
            stSource2 = TimeSpan.FromMilliseconds(etSource1.TotalMilliseconds + durationCut.TotalMilliseconds);
            etSource2 = elt.endTimeInSource;

            elt2 = new StoryBoardElement(elt.file, timeFrom, et2, stSource2, etSource2, elt.fileType, elt.fileName, elt.fileSize);
            elt2.hasToBeSplit = true;
            elt2.numPart = elt2.numPart + 1;

            elt.editTimes(elt.startTime, timeFrom, elt.startTimeInSource, etSource1);
            elt.hasToBeSplit = true;

            updatedElts[0] = elt1;
            updatedElts[1] = elt2;

            return updatedElts;

        }

        /// <summary>
        /// update the startTime and endTime of every storyboard elements after the given index
        /// </summary>
        /// <param name="index">index of the first storyBoard element to update</param>
        /// <param name="updateTime">time which will be remove of the startTime and endTime</param>
        /// <param name="negativ">true if the time is to remove, false if the time is to add</param>
        public void updateTimesAfter(int index, TimeSpan updateTime, Boolean negativ)
        {
            IEnumerator<StoryBoardElement> videoList = fileList.GetEnumerator();
            int i;
            for (i = 0; i < index; i++)
            {
                videoList.MoveNext();
            }

            while (videoList.MoveNext())
            {
                Console.WriteLine("! - ! update index : " + i);
                StoryBoardElement elt = videoList.Current;
                if (negativ == true)
                    videoList.Current.editTimes(elt.startTime - updateTime, elt.endTime - updateTime, elt.startTimeInSource, elt.endTimeInSource);
                else if (negativ == false)
                    videoList.Current.editTimes(elt.startTime + updateTime, elt.endTime + updateTime, elt.startTimeInSource, elt.endTimeInSource);
                i++;
            }

            Console.WriteLine("---------------");
            printAllElements();
            Console.WriteLine("---------------");

        }

        /// <summary>
        /// update the startTime and endTime of every storyboard elements between two given elements
        /// </summary>
        /// <param name="indexStart">index of the first element to update</param>
        /// <param name="indexStop">index of the last element to update</param>
        /// <param name="updateTime">time which will be remove of the startTime and endTime</param>
        /// /// <param name="negativ">true if the time is to remove, false if the time is to add</param>
        public void updateTimesBetween(int indexStart, int indexStop, TimeSpan updateTime, Boolean negativ)
        {
            IEnumerator<StoryBoardElement> videoList = fileList.GetEnumerator();
            int i;
            for (i = 0; i < indexStart; i++)
            {
                videoList.MoveNext();
            }

            while (videoList.MoveNext())
            {

                Console.WriteLine("! - ! update index : " + i);
                StoryBoardElement elt = videoList.Current;
                if (negativ == true)
                    videoList.Current.editTimes(elt.startTime - updateTime, elt.endTime - updateTime, elt.startTimeInSource, elt.endTimeInSource);
                else if (negativ == false)
                    videoList.Current.editTimes(elt.startTime + updateTime, elt.endTime + updateTime, elt.startTimeInSource, elt.endTimeInSource);
                if (i == indexStop)
                {
                    break;
                }
                i++;
            }

            Console.WriteLine("---------------");
            printAllElements();
            Console.WriteLine("---------------");

        }

        /// <summary>
        /// Add a subtitle to the corresponding storyBoard elements
        /// </summary>
        /// <param name="start">start time of the subtitle in the storyBoard</param>
        /// <param name="end">end time of the subtitle in the storyBoard</param>
        /// <param name="content">text of the subtitle</param>
        public void addSubtitleToStory(TimeSpan start, TimeSpan end, String content)
        {
            IEnumerator<StoryBoardElement> videoList = fileList.GetEnumerator();
            StoryBoardElement elt;
            TimeSpan startElt;
            TimeSpan endElt;

            this.subtitles.AddSubtitle(start, end, content);

            while (videoList.MoveNext())
            {
                elt = videoList.Current;

                /* subtitle middle of elt */
                if (elt.startTime <= start && elt.endTime >= end)
                {
                    startElt = TimeSpan.FromMilliseconds(start.TotalMilliseconds - elt.startTime.TotalMilliseconds);
                    endElt = TimeSpan.FromMilliseconds(end.TotalMilliseconds - elt.startTime.TotalMilliseconds);
                    elt.subtitles.AddSubtitle(startElt, endElt, content);
                }
                /* subtitle in entire elt */
                else if (elt.startTime > start && elt.endTime < end)
                {
                    startElt = elt.startTimeInSource;
                    endElt = elt.endTimeInSource;
                    elt.subtitles.AddSubtitle(startElt, endElt, content);
                }
                /* subtitle in end elt */
                else if ((elt.startTime < start && elt.endTime > start) && (elt.endTime < end))
                {
                    startElt = elt.startTimeInSource;
                    endElt = TimeSpan.FromMilliseconds(end.TotalMilliseconds - elt.startTime.TotalMilliseconds);
                    elt.subtitles.AddSubtitle(startElt, endElt, content);
                }
                /* subtitle in begining elt */
                else if ((elt.startTime > start) && (elt.startTime > end && elt.endTime < end))
                {
                    startElt = TimeSpan.FromMilliseconds(start.TotalMilliseconds - elt.startTime.TotalMilliseconds);
                    endElt = elt.endTimeInSource;
                    elt.subtitles.AddSubtitle(startElt, endElt, content);
                }
            }
        }

        /// <summary>
        /// calculates the end time in the source
        /// </summary>
        /// <returns></returns>
        public TimeSpan calcEndInSource(TimeSpan startTime, TimeSpan endTime, TimeSpan startTimeInSource)
        {
            double seconds;
            seconds = endTime.TotalSeconds - startTime.TotalSeconds;
            TimeSpan endSource = TimeSpan.FromSeconds(startTimeInSource.TotalSeconds + seconds);
            return endSource;
        }

        /// <summary>
        /// Show infos about time of all elements in console
        /// </summary>
        public void printAllElements()
        {
            IEnumerator<StoryBoardElement> videoList = fileList.GetEnumerator();
            while (videoList.MoveNext())
            {
                StoryBoardElement elt = videoList.Current;
                Console.WriteLine(elt.fileName + " | story : " + elt.startTime + " / " + elt.endTime + " | source : " + elt.startTimeInSource + " / " + elt.endTimeInSource);
            }
        }

        /// <summary>
        /// Get the index of a given storyboard element
        /// </summary>
        /// <param name="eltToFind">the storyboard element</param>
        /// <returns></returns>
        private int getIndex(StoryBoardElement eltToFind)
        {
            IEnumerator<StoryBoardElement> videoList = fileList.GetEnumerator();
            StoryBoardElement elt;
            int index = 0;
            while (videoList.MoveNext())
            {
                elt = videoList.Current;
                if (elt == eltToFind)
                {
                    return index;
                }
                index++;
            }
            return -1;

        }

        /// <summary>
        /// /!\ Clean the storyboard /!\
        /// </summary>
        public void purge()
        {
            _fileList.Clear();
        }

        /// <summary>
        /// Sort the file list
        /// </summary>
        public void sortFileList()
        {
            ObservableCollection<StoryBoardElement> oldFileList = fileList;
            fileList = new ObservableCollection<StoryBoardElement>(oldFileList.OrderBy(x => x.startTime));
            printAllElements();
        }
    }
}


using System;
using System.Collections;
using USC.GISResearchLab.Common.Threading.ProgressStates.Interfaces;

namespace USC.GISResearchLab.Common.Threading.ProgressStates
{
    public class PercentCompletableProgressState : ExceptionableProgressState, IPercentCompletableProgressState
    {
        #region Properties

        public string CurrentFile { get; set; }

        public bool StartTimeSet { get; set; }
        public DateTime StartTime { get; set; }
        public ArrayList RecordsPerSecondList { get; set; }
        public bool MustRecalculateRecordsPerSecond { get; set; }
        public double LastRecordsPerSecond { get; set; }

        public double RecordsPerSecond
        {
            get
            {
                double ret = 0;

                if (Completed > 0)
                {
                    if (ElapsedTime.TotalSeconds > 0)
                    {
                        ret = (double)Completed / (double)ElapsedTime.TotalSeconds;
                    }
                }
                return ret;
            }
        }

        public string RecordsPerSecondString
        {
            get
            {
                return Convert.ToInt32(RecordsPerSecond) + "/s";
            }
        }

        public ArrayList AverageSecondsPerRecordList { get; set; }
        public bool MustRecalculateAverageSecondsPerRecord { get; set; }
        public double LastAverageSecondsPerRecord { get; set; }

        public double AverageSecondsPerRecord
        {
            get
            {
                if (MustRecalculateAverageSecondsPerRecord)
                {
                    if (AverageSecondsPerRecordList != null)
                    {
                        if (AverageSecondsPerRecordList.Count > 0)
                        {
                            double total = 0;
                            for (int i = 0; i < AverageSecondsPerRecordList.Count; i++)
                            {
                                total += (double)AverageSecondsPerRecordList[i];
                            }
                            LastAverageSecondsPerRecord = total / (double)AverageSecondsPerRecordList.Count;
                        }
                    }

                    MustRecalculateAverageSecondsPerRecord = false;
                }

                return LastAverageSecondsPerRecord;
            }
        }


        public TimeSpan ElapsedTime
        {
            get { return DateTime.Now - StartTime; }
        }

        public TimeSpan RemainingTimeSeconds
        {
            get
            {
                TimeSpan ret = new TimeSpan(0);

                if (Completed < Total)
                {
                    double secondsRemaining = 0.0;

                    if (!StartTimeSet)
                    {
                        StartTime = DateTime.Now;
                        StartTimeSet = true;
                    }
                    else
                    {
                        if (RecordsPerSecond > 0 && !Double.IsInfinity(RecordsPerSecond) && !Double.IsNaN(RecordsPerSecond))
                        {
                            secondsRemaining = ((double)Total - (double)Completed) / RecordsPerSecond;
                        }
                    }

                    if (secondsRemaining > 0 && !Double.IsInfinity(secondsRemaining) && !Double.IsNaN(secondsRemaining))
                    {
                        ret = new TimeSpan(0, 0, Convert.ToInt32(secondsRemaining));
                    }
                }
                return ret;
            }
        }

        public string RemainingTimeWithSpeedString
        {
            get
            {
                string ret = RemainingTimeString;
                ret += " @" + Convert.ToInt32(RecordsPerSecond) + "/s";
                return ret;
            }
        }

        public string RemainingTimeString
        {
            get
            {
                string ret = "";

                object lockable = RemainingTimeSeconds.Ticks;
                lock (lockable)
                {
                    TimeSpan timeSpan = new TimeSpan((long)lockable);



                    if (timeSpan.TotalSeconds > 0)
                    {
                        if (timeSpan.TotalDays >= 1)
                        {
                            if (timeSpan.TotalDays == 1)
                            {
                                ret = Math.Round(timeSpan.TotalDays, 1) + " day";
                            }
                            else if (timeSpan.TotalDays < 5)
                            {
                                ret = Math.Round(timeSpan.TotalDays, 1) + " days";
                            }
                            else
                            {
                                ret = Math.Round(timeSpan.TotalDays, 1) + " days";
                            }
                        }
                        else if (timeSpan.TotalHours >= 1)
                        {
                            if (timeSpan.TotalHours == 1)
                            {
                                ret = Math.Round(timeSpan.TotalHours, 1) + " hr";
                            }
                            else
                            {
                                ret = Math.Round(timeSpan.TotalHours, 1) + " hrs";
                            }
                        }
                        else if (timeSpan.TotalMinutes >= 1)
                        {
                            if (timeSpan.TotalMinutes == 1)
                            {
                                ret = Math.Round(timeSpan.TotalMinutes) + " min";
                            }
                            else
                            {
                                ret = Math.Round(timeSpan.TotalMinutes) + " mins";
                            }
                        }
                        else
                        {
                            if (timeSpan.TotalSeconds == 1)
                            {
                                ret = Math.Round(timeSpan.TotalSeconds) + " sec";
                            }
                            else
                            {
                                ret = Math.Round(timeSpan.TotalSeconds) + " secs";
                            }
                        }
                    }
                }

                return ret;
            }
        }

        public int Completed { get; set; }
        public int Total { get; set; }

        public double PercentCompleted
        {
            get
            {
                double ret = 0;
                if (Completed >= 0 && Total > 0)
                {
                    ret = (Convert.ToDouble(Completed) / Convert.ToDouble(Total)) * 100;
                }
                return ret;
            }
        }

        public string PercentCompletedString
        {
            get
            {
                string ret = "0";
                if (PercentCompleted > 0)
                {
                    ret = "" + Math.Round(PercentCompleted, 2);
                }
                return ret;
            }
        }



        #endregion

        public PercentCompletableProgressState()
        {
            Total = 0;
            Completed = 0;
        }

        public override string ToString()
        {
            string ret = Message;
            if (Total > 0)
            {
                ret += " - " + Completed + "/" + Total + ": " + PercentCompleted + "%";
            }
            return ret;
        }

        public void ResetCounts()
        {
            Total = 0;
            Completed = 0;
        }
    }
}

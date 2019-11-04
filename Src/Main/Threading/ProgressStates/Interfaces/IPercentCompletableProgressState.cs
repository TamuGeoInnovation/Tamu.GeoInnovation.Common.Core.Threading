using System;
using System.Collections;

namespace USC.GISResearchLab.Common.Threading.ProgressStates.Interfaces
{
    public interface IPercentCompletableProgressState : IExceptionableProgressState
    {
        #region Properties

        bool StartTimeSet { get; set; }
        DateTime StartTime { get; set; }
        ArrayList RecordsPerSecondList { get; set; }
        bool MustRecalculateRecordsPerSecond { get; set; }
        double LastRecordsPerSecond { get; set; }

        double RecordsPerSecond { get; }

        string RecordsPerSecondString { get; }

        ArrayList AverageSecondsPerRecordList { get; set; }
        bool MustRecalculateAverageSecondsPerRecord { get; set; }
        double LastAverageSecondsPerRecord { get; set; }

        double AverageSecondsPerRecord { get; }


        TimeSpan ElapsedTime { get; }

        TimeSpan RemainingTimeSeconds { get; }

        string RemainingTimeWithSpeedString { get; }

        string RemainingTimeString { get; }

        int Completed { get; set; }
        int Total { get; set; }

        double PercentCompleted { get; }

        string PercentCompletedString { get; }



        #endregion


    }
}

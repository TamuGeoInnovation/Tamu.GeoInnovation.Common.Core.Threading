using System;

namespace USC.GISResearchLab.Common.Threading.ProgressStates.Interfaces
{
    
    public interface IExceptionableProgressState : IMessageableProgressState
    {
        #region Properties

        Exception Exception { get; set; }
        bool Error { get; set; }
        int NumberOfErrors { get; set; }
        int NumberOfRepeatedErrors { get; set; }
        ExceptionFatalityType ExceptionFatalityType { get; set; }
        ExceptionOccuranceType ExceptionOccuranceType { get; set; }

        #endregion

       
    }
}

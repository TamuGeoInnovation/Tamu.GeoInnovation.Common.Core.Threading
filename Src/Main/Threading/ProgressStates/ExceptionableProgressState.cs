using System;
using System.Text;
using USC.GISResearchLab.Common.Threading.ProgressStates.Interfaces;

namespace USC.GISResearchLab.Common.Threading.ProgressStates
{
    public enum ExceptionFatalityType { Unknown, NonFatal, Fatal };
    public enum ExceptionOccuranceType { Unknown, Unique, Repeated };

    public class ExceptionableProgressState : MessageableProgressState, IExceptionableProgressState
    {
        #region Properties

        public Exception Exception { get; set; }
        public bool Error { get; set; }
        public int NumberOfErrors { get; set; }
        public int NumberOfRepeatedErrors { get; set; }
        public ExceptionFatalityType ExceptionFatalityType { get; set; }
        public ExceptionOccuranceType ExceptionOccuranceType { get; set; }

        #endregion

        public ExceptionableProgressState()
        {
            Error = false;
        }

        public ExceptionableProgressState(string message, Exception exception)
        {
            Message = message;
            Exception = exception;
            Error = true;
        }

        public ExceptionableProgressState(string exceptionMessage)
        {
            Message = exceptionMessage;
            Exception = new Exception(Message);
            Error = true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Message);
            if (Error)
            {
                sb.AppendLine(Exception.Message);
            }
            return sb.ToString();
        }
    }
}

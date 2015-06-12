using USC.GISResearchLab.Common.Threading.ProgressStates.Interfaces;
namespace USC.GISResearchLab.Common.Threading.ProgressStates
{
    public class MessageableProgressState : IMessageableProgressState
    {
        #region Properties
        public string Message { get; set; }
        public object[] Data { get; set; }
        public string Current { get; set; }
        public string ProcessGuid { get; set; }

        #endregion

        public MessageableProgressState()
        {
        }

        public MessageableProgressState(string message)
        {
            Message = message;
        }

        public override string ToString()
        {
            return Message;
        }
    }
}

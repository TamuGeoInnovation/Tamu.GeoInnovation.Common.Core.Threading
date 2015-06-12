namespace USC.GISResearchLab.Common.Threading.ProgressStates.Interfaces
{
    public interface IMessageableProgressState
    {
        #region Properties
        string Message { get; set; }
        object[] Data { get; set; }
        string Current { get; set; }
        string ProcessGuid { get; set; }

        #endregion

    }
}

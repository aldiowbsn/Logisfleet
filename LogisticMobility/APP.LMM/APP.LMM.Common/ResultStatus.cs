
namespace APP.LMM.Common
{
    public class ResultStatus
    {
        #region Private Properties
        private int _status = -1;
        private string _messageText = string.Empty;
        #endregion

        #region Constructor
        public ResultStatus() { }
        #endregion

        #region Public Method and Properties
        public bool IsSuccess
        {
            get { return _status == 0; }
        }

        public int Status
        {
            get { return _status; }
        }

        public string MessageText
        {
            get { return _messageText; }
            set { _messageText = value; }
        }

        public void SetSuccessStatus()
        {
            _status = 0;
        }

        public void SetSuccessStatus(string message)
        {
            _status = 0;
            _messageText = message;
        }
        #endregion
    }
}

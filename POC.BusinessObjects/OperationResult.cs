using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.BusinessObjects
{

    public class OperationResult<TResult>
    {
        private OperationResult()
        {
        }

        public bool Success { get; private set; }
        public bool ShowMessage { get; private set; }
        public TResult Result { get; private set; }
        public string NonSuccessMessage { get; private set; }
        public Exception Exception { get; private set; }
        public string MessageType { get; private set; }

        public static OperationResult<TResult> CreateSuccessResult(TResult result, string nonSuccessMessage = "", string messageType = "", bool showMessage = false)
        {
            return new OperationResult<TResult> { Success = true, Result = result, ShowMessage = showMessage, NonSuccessMessage = nonSuccessMessage, MessageType = messageType };
        }

        //public static OperationResult<TResult> CreateFailure(string nonSuccessMessage = "", string messageType = "", bool showMessage = false)
        //{
        //    return new OperationResult<TResult> { Success = false, ShowMessage = showMessage, NonSuccessMessage = nonSuccessMessage, MessageType = messageType };
        //}

        public static OperationResult<TResult> CreateFailure(string nonSuccessMessage, bool showMessage = false)
        {
            return new OperationResult<TResult> { Success = false, NonSuccessMessage = nonSuccessMessage, ShowMessage = showMessage };
        }

        public static OperationResult<TResult> CreateFailure(Exception ex, bool showMessage = false)
        {
            return new OperationResult<TResult>
            {
                Success = false,
                NonSuccessMessage = String.Format("{0}{1}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace),
                Exception = ex,
                ShowMessage = showMessage
            };
        }
        public static OperationResult<TResult> CreateFailure(TResult result, string nonSuccessMessage = "", string messageType = "", bool showMessage = false)
        {
            return new OperationResult<TResult> { Result = result, Success = false, ShowMessage = showMessage, NonSuccessMessage = nonSuccessMessage, MessageType = messageType };
        }
    }
}

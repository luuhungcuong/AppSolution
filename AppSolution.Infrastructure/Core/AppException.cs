using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppSolution.Infrastructure.Core
{
    public class AppException : Exception
    {
        public string BzMessageId { get; set; }
        public string BzMessageDetail { get; set; }
        public Exception BaseException { get; set; }
        public ErrorType ErrorType { get; set; }

        public ProcessResult ProcessResult { get; set; }
        public AppException()
        {
            ErrorType = ErrorType.Danger;
            ProcessResult = ProcessResult.Failure;
        }
    }
    public enum ErrorType
    {
        Success = 1,
        Info,
        Warning,
        Danger,
    }
    public enum ProcessResult
    {
        Success = 1,
        Failure = 2
    }
}
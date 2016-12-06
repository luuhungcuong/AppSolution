using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolution.Infrastructure.Core
{

    public interface IMsgModel
    {

    }
    public class InputMsgModel : IMsgModel
    {
        [Required]
        public string Action { get; set; }

        public string EncryptData { get; set; }
    }
    public class OutputMsgModel : IMsgModel
    {
        public OutputMsgModel()
        {
            Data = new System.Dynamic.ExpandoObject();
            this.Result = ProcessResult.Success;
            Message = String.Empty;
        }

        public ProcessResult Result { get; set; }
        public string Message { get; set; }       
        public dynamic Data { get; set; }
    }    
}

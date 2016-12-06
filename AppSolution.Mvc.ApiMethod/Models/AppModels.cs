using AppSolution.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppSolution.Mvc.ApiMethod.Models
{
    public class MessageModel : IMsgModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public String Message { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSN.Web.ViewModels.UserAcc
{
    public class PostCreateInputModel
    {
        [Required]
        public string Content { get; set; }
    }
}

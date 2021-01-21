using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Web.ViewModels.Comments
{
    public class CreateCommentInputModel
    {
        public int PostId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }
    }
}

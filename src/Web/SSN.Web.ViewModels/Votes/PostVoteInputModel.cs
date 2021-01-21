using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Web.ViewModels.Votes
{
    public class PostVoteInputModel
    {
        public int PostId { get; set; }

        public bool IsUpVote { get; set; }
    }
}

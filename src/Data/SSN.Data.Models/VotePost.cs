using SSN.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Data.Models
{
    public class VotePost : BaseModel<int>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        public VoteType Type { get; set; }
    }
}

using SSN.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSN.Data.Models
{
    public class VoteUser : BaseModel<int>
    {
        public int UserAccId { get; set; }

        public virtual UserAcc UserAcc { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public VoteType Type { get; set; }
    }
}

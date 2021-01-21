using SSN.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSN.Data.Models
{
    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<VoteUser>();

        }

        public string Title { get; set; }

        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int UserAccId { get; set; }

        public virtual UserAcc UserAcc { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<VoteUser> Votes { get; set; }
    }
}

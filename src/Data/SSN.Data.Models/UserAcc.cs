namespace SSN.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SSN.Data.Common.Models;

    public class UserAcc : BaseDeletableModel<int>
    {
        public UserAcc()
        {
            this.Posts = new HashSet<Post>();
            this.Votes = new HashSet<VoteUser>();
        }

        public string Email { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Specialty { get; set; }

        public string SecondName { get; set; }

        public int Id { get; set; }

        public bool Gender { get; set; }

        public string FilePath { get; set; }

        public long UIN { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<VoteUser> Votes { get; set; }
    }
}

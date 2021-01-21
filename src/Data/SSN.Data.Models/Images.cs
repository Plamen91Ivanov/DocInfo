using SSN.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSN.Data.Models
{
    public class Images : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}

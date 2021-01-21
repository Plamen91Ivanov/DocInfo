using SSN.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Data.Models
{
    public class User : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

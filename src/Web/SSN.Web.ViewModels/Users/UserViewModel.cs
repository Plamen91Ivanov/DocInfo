using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using SSN.Data.Models;

namespace SSN.Web.ViewModels.Users
{
   public class UserViewModel : IMapFrom<User>
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Specialty { get; set; }

        public string Url => $"f/{this.Name}";

    }
}

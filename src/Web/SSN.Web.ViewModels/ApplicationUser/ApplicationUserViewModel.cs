using Microsoft.AspNetCore.Http;
using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Web.ViewModels.ApplicationUser
{
     public class ApplicationUserViewModel : IMapFrom<SSN.Data.Models.ApplicationUser>
    {
        public string UserName { get; set; }

        public string Id { get; set; }

        public IFormFile Photo { get; set; }

        public IEnumerable<UserImages> Images { get; set; }
    }
}

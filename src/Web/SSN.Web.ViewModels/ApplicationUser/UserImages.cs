using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Web.ViewModels.ApplicationUser
{
    public class UserImages : IMapFrom<Images>
    {
        public string ImagePath { get; set; }

        public string FilePath { get; set; }
    }
}

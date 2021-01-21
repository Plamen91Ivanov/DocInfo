using AutoMapper;
using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Web.ViewModels.Users
{
   public class NameViewModel : IMapFrom<User>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url => $"f/{this.Name}";
    }
}

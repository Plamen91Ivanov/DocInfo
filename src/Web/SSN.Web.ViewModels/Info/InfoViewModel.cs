using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Web.ViewModels.Info
{
    public class InfoViewModel : IMapFrom<Information>
    {

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string ThirdName { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string Status { get; set; }

        public string Sity { get; set; }

        public int Vipusk { get; set; }

        public string Online { get; set; }

        public long Neg { get; set; }

        public string Description { get; set; }
    }
}

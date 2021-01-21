using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Web.ViewModels.UserInfo
{
   public class UserinformationViewModel : IMapFrom<Information>
    {
        public string NickName { get; set; }
    }
}

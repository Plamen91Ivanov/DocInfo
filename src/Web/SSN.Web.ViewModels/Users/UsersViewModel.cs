using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Web.ViewModels.Users
{
    public class UsersViewModel : IMapFrom<User>
    {
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}

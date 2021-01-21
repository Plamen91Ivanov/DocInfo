using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Web.ViewModels.UserAcc
{
    public class UsersAccViewModel : IMapFrom<SSN.Data.Models.UserAcc>
    {
        public IEnumerable<UserAccViewModel> Users { get; set; }

        public IEnumerable<UserAccViewModel> Doc { get; set; }

        public IEnumerable<PostInUserAccViewModel> UserPosts { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}

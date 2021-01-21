using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Web.ViewModels.UserAcc
{
    public class SearchUserViewModel : IMapFrom<SSN.Data.Models.ApplicationUser>
    {
        public string Email { get; set; }

        public string Name { get {
                string nameInput = this.Email;
                string[] words = nameInput.Split('@');
                string nameSplit = words[0];
                return nameSplit;
            } }

        public string Id { get; set; }

        public string Url => $"/Account/{this.Email.Replace(' ', '-')}/{Id}";

        public IEnumerable<PhotoInUserAccViewModel> Photos { get; set; }
    }
}

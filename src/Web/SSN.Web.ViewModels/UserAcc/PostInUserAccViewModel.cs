using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SSN.Web.ViewModels.UserAcc
{
    public class PostInUserAccViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FormatedDate
        {
            get
            {
                var formatedDate = this.CreatedOn.ToString("dd/MM/yyyy HH:mm");
                return formatedDate;
            }
        }

        public string Content { get; set; }

        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Content, @"<[^>]+>", string.Empty));
                return content.Length > 130
                ? content.Substring(0, 130) + "..."
                : content;
            }
        }

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }

        public IEnumerable<UserAccViewModel> Users { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}

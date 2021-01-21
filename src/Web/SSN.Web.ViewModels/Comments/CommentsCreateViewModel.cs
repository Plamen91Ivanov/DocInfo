using SSN.Data.Models;
using SSN.Services.Mapping;
using SSN.Web.ViewModels.Posts;
using SSN.Web.ViewModels.UserAcc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSN.Web.ViewModels.Comments
{
    public class CommentsCreateViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public IEnumerable<PostCommentViewModel> Comments { get; set; }

        public IEnumerable<PostInUserAccViewModel> UserPosts { get; set; }



    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SSN.Data.Models;
using SSN.Services.Data;
using SSN.Web.ViewModels.Comments;
using SSN.Web.ViewModels.Posts;
using SSN.Web.ViewModels.UserAcc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSN.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            ICommentsService commentsService,
            IPostsService postsService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.postsService = postsService;
            this.userManager = userManager;
        }

        public IActionResult CommentCreate(int postId,string name, int id)
        {
            var viewModel = new PostViewModel();
            viewModel = this.postsService.GetPostById<PostViewModel>(postId);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentInputModel input)
            {
            var parentId = input.ParentId == 0 ? (int?)null : input.ParentId;

            if (parentId.HasValue)
            {
                if (!this.commentsService.IsInSamePost(parentId.Value, input.PostId))
                {
                    return this.BadRequest();
                }
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.commentsService.Create(input.PostId, userId, input.Content, parentId);
            return this.RedirectToAction("ById", "Posts", new { id = input.PostId });
            //action-byid
            //controller-posts
        }
    }
}

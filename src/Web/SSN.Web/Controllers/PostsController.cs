using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SSN.Data.Models;
using SSN.Services.Data;
using SSN.Web.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSN.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(
            IPostsService postsService,
            UserManager<ApplicationUser> userManager)
        {
            this.postsService = postsService;
            this.userManager = userManager;
        }

        public IActionResult ById(int id)
        {
            var postViewModel = this.postsService.GetPostById<PostViewModel>(id);
            return this.View(postViewModel);
        }

        public IActionResult PostById(int id)
        {
            var postViewModel = this.postsService.GetPostById<PostViewModel>(id);
            return this.View(postViewModel);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SSN.Data.Models;
using SSN.Services.Data;
using SSN.Web.ViewModels.UserAcc;
using SSN.Web.ViewModels.Votes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SSN.Web.Controllers
{
    public class UserAccController : Controller
    {
        private const int ItemPerPage = 5;

        private readonly IUserAccService addService;
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISearchService searchService;
        private readonly IHostingEnvironment hostingEnviroment;
        private readonly IAppUserService appUserService;
        private readonly IWebScraper webScraper;

        public UserAccController(
                                 IUserAccService addService,
                                 IPostsService postsService,
                                 UserManager<ApplicationUser> userManager,
                                 ISearchService searchService,
                                 IHostingEnvironment hostingEnviroment,
                                 IAppUserService appUserService,
                                 IWebScraper webScraper)
        {
            this.addService = addService;
            this.postsService = postsService;
            this.userManager = userManager;
            this.searchService = searchService;
            this.hostingEnviroment = hostingEnviroment;
            this.appUserService = appUserService;
            this.webScraper = webScraper;
        }

        public IActionResult AddUser()
        {
            var addViewModel = new UserAccViewModel(); 

            return this.View(addViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserAccViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string uniqueFileName = null;
            if (input.Photo != null)
            {
               string uploadsFolder = Path.Combine(this.hostingEnviroment.WebRootPath, "images");
               uniqueFileName = Guid.NewGuid().ToString() + "_" + input.Photo.FileName;
               var filePath = Path.Combine(uploadsFolder, uniqueFileName);
               input.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

               var addUser = await this.addService.AddAsync(
               input.Email,
               input.Name,
               input.City,
               input.Specialty,
               uniqueFileName
               );
            }
            else
            {
                string filePath = null;
                var addUser = await this.addService.AddAsync(
                input.Email,
                input.Name,
                input.City,
                input.Specialty,
                uniqueFileName
                );
            }
            return this.RedirectToAction("AddUser");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComent(PostCreateInputModel input, int id, string name)
        {
            var userAccName = name;
            var userAccId = id;
            var user = await this.userManager.GetUserAsync(this.User);
            var postId = await this.postsService.CreateAsync(input.Content, userAccId, user.Id);

            return this.RedirectToAction("GetByName", new { name = name });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int postId, string name)
        {

            var deletePost = await this.postsService.DeleteAsync(postId);

            return this.RedirectToAction("GetByName", new { name = name });
        }

        public IActionResult GetByName(string name, int page = 1)
        {
            var viewModel =
                this.addService.GetById<UserAccViewModel>(name);
            viewModel.UserPosts = this.postsService.GetByCategoryId<PostInUserAccViewModel>(viewModel.Id, ItemPerPage, (page - 1) * ItemPerPage);

            var count = this.postsService.GetCountByCategoryId(viewModel.Id);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemPerPage);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public IActionResult GetByUserName(string name, string Id)
        {
            var viewModel =
                this.addService.GetByUserName<SearchUserViewModel>(name);
            viewModel.Photos = this.appUserService.GetAllImages<PhotoInUserAccViewModel>(Id);
            return this.View(viewModel);
        }


        public IActionResult Register()
        {
            return this.View();
        }

        public IActionResult Create(int id, string name)
        {
            ViewBag.Test = id;
            ViewBag.Name = name;

            return this.View();
        }

        public IActionResult UploadPhoto()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadPhoto(PhotoInUserAccViewModel input)
        {

            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string uniqueFileName = null;
            string uploadsFolder = Path.Combine(this.hostingEnviroment.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + input.Photo.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            input.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

            var addPhoto = await this.addService.AddPhotoAsync(
            uniqueFileName,
            user.Id
            );

            return this.RedirectToAction("Profile", "ApplicationUser");
        }

        public IActionResult SearchUser(string email)
        {
            if (email == null)
            {
                var foundUserEmail = new SearchUsersViewModel
                {
                    Users = this.searchService.GetAllUsers<SearchUserViewModel>(),
                };
                return this.View(foundUserEmail);

            }else
            {
                var foundUserEmail = new SearchUsersViewModel
                {
                    Users = this.searchService.SearchUserEmail<SearchUserViewModel>(email),
                };
                return this.View(foundUserEmail);
            }
        }
    }
}

namespace SSN.Web.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SSN.Data.Models;
    using SSN.Services.Data;
    using SSN.Web.ViewModels.ApplicationUser;

    public class ApplicationUserController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAppUserService appUserService;
        private readonly IHostingEnvironment hostingEnvironment;
        private object hostingEnviroment;

        public ApplicationUserController(
            UserManager<ApplicationUser> userManager,
            IAppUserService appUserService,
            IHostingEnvironment hostingEnvironment
            )
        {
            this.userManager = userManager;
            this.appUserService = appUserService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var addViewModel = new ApplicationUserViewModel
            {
                Images = this.appUserService.GetAllImages<UserImages>(user.Id),
            };
            return this.View(addViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadPhoto(ApplicationUserViewModel input)
        {

            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string uniqueFileName = null;
            if (input.Photo != null)
            {
                string uploadsFolder = Path.Combine(this.hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + input.Photo.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                input.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

                var addPhoto = await this.appUserService.AddPhotoAsync(
                uniqueFileName,
                user.Id
                );
            }
            return this.RedirectToAction("Profile", "ApplicationUser");
        }

    }
}

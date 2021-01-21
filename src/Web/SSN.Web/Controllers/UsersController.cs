using Microsoft.AspNetCore.Mvc;
using SSN.Data.Models;
using SSN.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSN.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly INameService userService;

        public UsersController(INameService userService)
        {
            this.userService = userService;
        }

        public IActionResult UserPage()
        {
            return this.View();
        }

        public IActionResult ById(string city)
        {
            var viewModel =
                this.userService.GetById<User>(city);

            return this.View(viewModel);
        }
    }
}

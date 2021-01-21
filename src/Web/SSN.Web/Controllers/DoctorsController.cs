using Microsoft.AspNetCore.Mvc;
using SSN.Services.Data;
using SSN.Web.ViewModels.UserAcc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSN.Web.Controllers
{
    public class DoctorsController : BaseController
    {
        private const int ItemPerPage = 5;

        private readonly IUserAccService userAccService;
        private readonly IUserInfo userInfo;

        public DoctorsController(IUserAccService userAccService,
                                 IUserInfo userInfo)
        {
            this.userAccService = userAccService;
            this.userInfo = userInfo;
        }

        public IActionResult doctorResult(string search, string region, string specialty, string name, string searchId, int page = 1)
        {
            if (search != null && region != null && specialty != "Specialty")
            {

            }
            else if (search != null && region != null)
            {

            }
            else if (search != null && specialty != "Specialty")
            {

            }
            else if (region != null && specialty != "Specialty")
            {

            }
            else if (name != null)
            {
                var inputParts = name.Split(' ');

                if (inputParts.Length == 3)
                {
                    var keyNumber = int.Parse(inputParts[0]);
                    var firstPart = inputParts[1];
                    var secPart = inputParts[2];
                    if (keyNumber == 1)
                    {
                        search = firstPart;
                        region = secPart;
                    }
                    else if (keyNumber == 2)
                    {
                        search = firstPart;
                        specialty = secPart;
                    }
                    else
                    {
                        region = firstPart;
                        specialty = secPart;
                    }
                }
                else if (inputParts.Length == 4)
                {
                    var firstPart = inputParts[0];
                    var secPart = inputParts[1];
                    var thyrdPart = inputParts[2];

                    search = firstPart;
                    region = secPart;
                    specialty = thyrdPart;
                }
            }

            if (search != null)
            {
                name = null;
            }
            else if(searchId == "search")
            {
               search = name;
            }

            if (region != null)
            {
                name = null;
            }
            else if (searchId == "region")
            {
                region = name;
            }

            if (specialty != "Specialty" && specialty != null)
            {
                name = null;
            }
            else if (searchId == "specialty")
            {
                specialty = name;
            }

            this.ViewBag.Search = search;
            this.ViewBag.Name = name;
            this.ViewBag.Region = region;
            this.ViewBag.Specialty = specialty;

            if (search != null && region != null && specialty != "Specialty" && specialty != null)
            {
                var foundUserAndRegion = new UsersAccViewModel
                {
                    Users = this.userAccService.SearchUserAndRegionAndSpecialty<UserAccViewModel>(search, region, specialty, ItemPerPage, (page - 1) * ItemPerPage),
                };

                var doctorsCount = this.userAccService.SearchCount(search, region, specialty);
                foundUserAndRegion.PagesCount = (int)Math.Ceiling((double)doctorsCount / ItemPerPage);
                if (foundUserAndRegion.PagesCount == 0)
                {
                    foundUserAndRegion.PagesCount = 1;
                }

                foundUserAndRegion.CurrentPage = page;

                return this.View(foundUserAndRegion);

            }
            else if (search != null && region != null)
            {
                var foundUserAndRegion = new UsersAccViewModel
                {
                    Users = this.userAccService.SearchUserAndRegion<UserAccViewModel>(search, region, ItemPerPage, (page - 1) * ItemPerPage),
                };

                var doctorsCount = this.userAccService.SearchCount(search, region, specialty);
                foundUserAndRegion.PagesCount = (int)Math.Ceiling((double)doctorsCount / ItemPerPage);
                if (foundUserAndRegion.PagesCount == 0)
                {
                    foundUserAndRegion.PagesCount = 1;
                }

                foundUserAndRegion.CurrentPage = page;

                return this.View(foundUserAndRegion);
            }
            else if (search != null && specialty != "Specialty" && specialty != null)
            {
                var foundUserAndRegion = new UsersAccViewModel
                {
                    Users = this.userAccService.SearchUserAndSpecialty<UserAccViewModel>(search, specialty, ItemPerPage, (page - 1) * ItemPerPage),
                };

                var doctorsCount = this.userAccService.SearchCount(search, region, specialty);
                foundUserAndRegion.PagesCount = (int)Math.Ceiling((double)doctorsCount / ItemPerPage);
                if (foundUserAndRegion.PagesCount == 0)
                {
                    foundUserAndRegion.PagesCount = 1;
                }

                foundUserAndRegion.CurrentPage = page;

                return this.View(foundUserAndRegion);
            }
            else if (specialty != "Specialty" && specialty != null && region != null)
            {
                var foundUserAndRegion = new UsersAccViewModel
                {
                    Users = this.userAccService.SearchSpecialtyAndRegion<UserAccViewModel>(specialty, region, ItemPerPage, (page - 1) * ItemPerPage),
                };

                var doctorsCount = this.userAccService.SearchCount(search, region, specialty);
                foundUserAndRegion.PagesCount = (int)Math.Ceiling((double)doctorsCount / ItemPerPage);
                if (foundUserAndRegion.PagesCount == 0)
                {
                    foundUserAndRegion.PagesCount = 1;
                }

                foundUserAndRegion.CurrentPage = page;

                return this.View(foundUserAndRegion);
            }
            else if (!string.IsNullOrEmpty(search))
            {
                var foundUser = new UsersAccViewModel
                {
                    Users = this.userAccService.Search<UserAccViewModel>(search, ItemPerPage, (page - 1) * ItemPerPage),
                };

                var doctorsCount = this.userAccService.SearchCount(search, region, specialty);
                foundUser.PagesCount = (int)Math.Ceiling((double)doctorsCount / ItemPerPage);
                if (foundUser.PagesCount == 0)
                {
                    foundUser.PagesCount = 1;
                }

                foundUser.CurrentPage = page;
                return this.View(foundUser);
            }
            else if (!string.IsNullOrEmpty(region))
            {
                var foundRegion = new UsersAccViewModel
                {
                    Users = this.userAccService.SearchRegion<UserAccViewModel>(region, ItemPerPage, (page - 1) * ItemPerPage),
                };

                var doctorsCount = this.userAccService.SearchCount(search, region, specialty);
                foundRegion.PagesCount = (int)Math.Ceiling((double)doctorsCount / ItemPerPage);
                if (foundRegion.PagesCount == 0)
                {
                    foundRegion.PagesCount = 1;
                }

                foundRegion.CurrentPage = page;

                return this.View(foundRegion);
            }
            else if (specialty != "Specialty" && specialty != null)
            {
                var foundRegion = new UsersAccViewModel
                {
                    Users = this.userAccService.SearchSpecialty<UserAccViewModel>(specialty, ItemPerPage, (page - 1) * ItemPerPage),
                };

                var doctorsCount = this.userAccService.SearchCount(search, region, specialty);
                foundRegion.PagesCount = (int)Math.Ceiling((double)doctorsCount / ItemPerPage);
                if (foundRegion.PagesCount == 0)
                {
                    foundRegion.PagesCount = 1;
                } 
                foundRegion.CurrentPage = page;

                return this.View(foundRegion);
            }

            // var viewModel = new UsersAccViewModel(); 
            // viewModel.Users = this.userAccService.GetAll<UserAccViewModel>();
            var instanceOfoBject = new UsersAccViewModel();
            instanceOfoBject.PagesCount = 0;

            return this.View(instanceOfoBject);
        }
    }
}

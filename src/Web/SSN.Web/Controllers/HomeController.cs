namespace SSN.Web.Controllers
{
    using System.Diagnostics;

    using SSN.Web.ViewModels.Users;

    using Microsoft.AspNetCore.Mvc;
    using SSN.Services.Data;
    using SSN.Web.ViewModels;
    using SSN.Web.ViewModels.UserAcc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using SSN.Web.ViewModels.UserInfo;
    using System;

    public class HomeController : BaseController
    {
        private const int ItemPerPage = 10;

        private readonly IUserAccService userAccService;
        private readonly IUserInfo userInfoService;
        private readonly IVotesService votesService;

        public HomeController(IUserAccService userAccService,
                              IUserInfo userInfoService,
                              IVotesService votesService)
        {
            this.userAccService = userAccService;
            this.userInfoService = userInfoService;
            this.votesService = votesService;
        }

        public IActionResult Index()
        {

            //    //var getDoctors = this.userAccService.AddAsyncDoctor().GetAwaiter().GetResult();
            //    //bestdoctors.bg get 750 doctors info
            //    // var getDoctor = this.userAccService.AddDoctor(41, 50).GetAwaiter().GetResult();


            //    // idc some test get email m8 be
            //    // var tesst = new UserinformationViewModel();

            //    // for (int i = 1; i < 3; i++)
            //    // {
            //    //     var getnick = this.userInfoService.GetByUserId<UserinformationViewModel>(i);
            //    //     var nick = getnick.NickName.Replace("(", String.Empty);
            //    //     var nickN = nick.Replace(")", String.Empty);
            //    //     this.userInfoService.Email(nickN);
            //    // }

            //    // get users from pmgl rdy 
            //   var from = 6611;
            //   var to = 6640;
            //
            //   for (int i = 0; i < 3; i++)
            //   {
            //       var information = this.userInfoService.AddAsync(from, to);
            //       from += 30;
            //       to += 30;
            //   }

            //    if (name != null)
            //    {
            //        search = name;
            //    }
            //    this.ViewBag.Search = search;
            //    this.ViewBag.Name = name;
            //    this.ViewBag.Region = region;
            //    this.ViewBag.Specialty = specialty;

            //    if (search != null && region != null && specialty != specialty)
            //    {
            //        var foundUserAndRegion = new UsersAccViewModel
            //        {
            //            Users = this.userAccService.SearchUserAndRegionAndSpecialty<UserAccViewModel>(search, region, specialty),
            //        };

            //        return this.View(foundUserAndRegion);

            //    }else if (search != null && region != null)
            //    {
            //        var foundUserAndRegion = new UsersAccViewModel
            //        {
            //            Users = this.userAccService.SearchUserAndRegion<UserAccViewModel>(search, region),
            //        };

            //        return this.View(foundUserAndRegion);
            //    }else if (search != null && specialty != specialty)
            //    {
            //        var foundUserAndRegion = new UsersAccViewModel
            //        {
            //            Users = this.userAccService.SearchUserAndSpecialty<UserAccViewModel>(search, specialty),
            //        };

            //        return this.View(foundUserAndRegion);
            //    }
            //    else if (specialty != specialty && region != null)
            //    {
            //        var foundUserAndRegion = new UsersAccViewModel
            //        {
            //            Users = this.userAccService.SearchSpecialtyAndRegion<UserAccViewModel>(specialty, region),
            //        };

            //        return this.View(foundUserAndRegion);
            //    }
            //    else if (!string.IsNullOrEmpty(search))
            //    {
            //        var foundUser = new UsersAccViewModel
            //        {
            //            Users = this.userAccService.Search<UserAccViewModel>(search, ItemPerPage, (page - 1) * ItemPerPage),
            //        };

            //        var doctorsCount = this.userAccService.SearchCount(search);
            //        foundUser.PagesCount = (int)Math.Ceiling((double)doctorsCount / ItemPerPage);
            //        foundUser.CurrentPage = page;
            //        return this.View(foundUser);
            //    }
            //    else if (!string.IsNullOrEmpty(region))
            //    {
            //        var foundRegion = new UsersAccViewModel
            //        {
            //            Users = this.userAccService.SearchRegion<UserAccViewModel>(region),
            //        };

            //        return this.View(foundRegion);
            //    }
            //    else if (!string.IsNullOrEmpty(specialty))
            //    {
            //        var foundRegion = new UsersAccViewModel
            //        {
            //            Users = this.userAccService.SearchSpecialty<UserAccViewModel>(specialty),
            //        };
            //        return this.View(foundRegion);
            //    }

            //    // var viewModel = new UsersAccViewModel();

            //    // viewModel.Users = this.userAccService.GetAll<UserAccViewModel>();
            //    var instanceOfoBject = new UsersAccViewModel();
            //    instanceOfoBject.PagesCount = 0;

            var mostVotedDocId = this.userAccService.MostVotedDoc();
            var mostCommentDocId = this.userAccService.MostCommentDoc();

            var mostVotedDoctors = new UsersAccViewModel
            {
                Users = this.userAccService.GetMostVoted<UserAccViewModel>(mostVotedDocId),
                Doc = this.userAccService.GetMostVoted<UserAccViewModel>(mostCommentDocId),
            };

            return this.View(mostVotedDoctors);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

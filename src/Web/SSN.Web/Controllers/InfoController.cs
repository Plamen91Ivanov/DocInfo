using Microsoft.AspNetCore.Mvc;
using SSN.Services.Data;
using SSN.Web.ViewModels.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSN.Web.Controllers
{
    public class InfoController : BaseController
    {
        private readonly IInfoServce infoServce;

        public InfoController(IInfoServce infoServce)
        {
            this.infoServce = infoServce;
        }

        public IActionResult showInfo()
        {
            //var findInfo = new InfosViewModel
            //{
            //    Infos = this.infoServce.GetInfo<InfoViewModel>(),
            //};
            var findInfo = this.infoServce.GetAll<InfoViewModel>();

            return this.View(findInfo);
             
        }
    }
}

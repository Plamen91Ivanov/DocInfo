using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSN.Data.Models;
using SSN.Services.Mapping;
using SSN.Web.ViewModels.UserAcc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSN.Web.Areas.Identity.Pages.Account.Manage
{
    public class UploadImage : PageModel, IMapFrom<Images>
    {
        public IFormFile Photo { get; set; }
        
        public IEnumerable<PhotosInUserAccViewModel> Images { get; set; }
    }
}

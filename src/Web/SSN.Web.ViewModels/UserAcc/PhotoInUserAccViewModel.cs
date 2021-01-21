using Microsoft.AspNetCore.Http;
using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Web.ViewModels.UserAcc
{
    public class PhotoInUserAccViewModel : IMapFrom<Images>
    {
        public int Id { get; set; }

        public IFormFile Photo { get; set; }

        public string ImagePath { get; set; }

        public IEnumerable<PhotosInUserAccViewModel> Photos { get; set; }
    }
}

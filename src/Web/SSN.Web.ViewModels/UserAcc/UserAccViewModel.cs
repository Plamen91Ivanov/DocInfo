using AutoMapper;
using Microsoft.AspNetCore.Http;
using SSN.Data.Models;
using SSN.Services.Mapping;
using SSN.Web.ViewModels.Votes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSN.Web.ViewModels.UserAcc
{
    public class UserAccViewModel : VoteResponseModel, IMapFrom<SSN.Data.Models.UserAcc>, IHaveCustomMappings
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }

        public bool Gender { get; set; }

        public string City { get; set; }

        public string Specialty { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int PostsCount { get; set; }

        public int VotesCount1 { get; set; }

        public int UpVotesCount1 { get; set; }

        public int DownVotesCount1 { get; set; }

        public string Url => $"f/{this.Name}";

        public IFormFile Photo { get; set; }

        public string FilePath { get; set; }

        public IEnumerable<PostInUserAccViewModel> UserPosts { get; set; }

        public IEnumerable<PhotoInUserAccViewModel> UserPhotos { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SSN.Data.Models.UserAcc, UserAccViewModel>()
           .ForMember(x => x.UpVotesCount, options =>
           {
               options.MapFrom(p => p.Votes
               .Sum(v => (int)v.Type));
           });
        }
 
    }
}

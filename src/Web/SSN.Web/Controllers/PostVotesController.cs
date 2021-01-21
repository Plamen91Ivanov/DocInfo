using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SSN.Data.Models;
using SSN.Services.Data;
using SSN.Web.ViewModels.Votes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSN.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostVotesController : ControllerBase
    {
        private readonly IVotePostsService postVotesService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostVotesController(IVotePostsService postVotesService,
            UserManager<ApplicationUser> userManager)
        {
            this.postVotesService = postVotesService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PostVoteResponseModel>> Post(PostVoteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.postVotesService.VoteAsync(input.PostId, userId, input.IsUpVote);
            var upVotes = this.postVotesService.GetVote(input.PostId);
            var downVotes = this.postVotesService.GetDownVote(input.PostId);

            return new PostVoteResponseModel { DownVotesCount = downVotes, UpVotesCount = upVotes };

        }
    }
}

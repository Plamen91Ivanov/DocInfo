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
    public class VotessController : ControllerBase
    {
        private readonly IVoteUserService votesService;
        private readonly IVotePostsService postVotesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotessController(
            IVoteUserService votesService,
            IVotePostsService postVotesService,
            UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.postVotesService = postVotesService;
            this.userManager = userManager;
        }

        //Post /api/votes
        // Request body: {"postId":1,"isUpVote":true}
        // Response body: {"votesCount":16}
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

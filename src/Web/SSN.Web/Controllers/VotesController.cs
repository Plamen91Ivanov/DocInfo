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
    public class VotesController : ControllerBase
    {
        private readonly IVoteUserService votesService;
        private readonly UserManager<ApplicationUser> userManager;

        public VotesController(
            IVoteUserService votesService,
            UserManager<ApplicationUser> userManager)
        {
            this.votesService = votesService;
            this.userManager = userManager;
        }

        //Post /api/votes
        // Request body: {"postId":1,"isUpVote":true}
        // Response body: {"votesCount":16}
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VoteResponseModel>> Post(VoteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.votesService.VoteAsync(input.UserAccId, userId, input.IsUpVote);
            var votes = this.votesService.GetVote(input.UserAccId);
            var downVotes = this.votesService.GetDownVote(input.UserAccId);

            return new VoteResponseModel { UpVotesCount = votes, VotesDownCount = downVotes };
        }
    }
}

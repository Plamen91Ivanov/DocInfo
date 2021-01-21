using SSN.Data.Common.Repositories;
using SSN.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSN.Services.Data
{
    public class VoteUserService : IVoteUserService
    {
        private readonly IRepository<VoteUser> votesRepository;

        public VoteUserService(IRepository<VoteUser> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public int GetVote(int userAccId)
        {
            var upVotes = this.votesRepository.All()
                .Where(x => x.UserAccId == userAccId)
                .Where(x => x.Type > 0)
                .Sum(x => (int)x.Type);
            return upVotes;
        }

        public int GetDownVote(int userAccId)
        {
            var downVotes = this.votesRepository.All()
                .Where(x => x.UserAccId == userAccId)
                .Where(x => x.Type < 0)
                .Sum(x => (int)x.Type);
            return downVotes;
        }

        public async Task VoteAsync(int userAccId, string userId, bool isUpVote)
        {
            var vote = this.votesRepository.All()
               .FirstOrDefault(x => x.UserAccId == userAccId && x.UserId == userId);
            if (vote != null)
            {
                vote.Type = isUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                vote = new VoteUser
                {
                    UserAccId = userAccId,
                    UserId = userId,
                    Type = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }

    }
}

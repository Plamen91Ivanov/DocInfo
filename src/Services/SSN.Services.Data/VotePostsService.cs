using SSN.Data.Common.Models;
using SSN.Data.Common.Repositories;
using SSN.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSN.Services.Data
{
    public class VotePostsService : IVotePostsService
    {
        private readonly IRepository<VotePost> postRepository;

        public VotePostsService(IRepository<VotePost> postRepository)
        {
            this.postRepository = postRepository;
        }

        public int GetDownVote(int postId)
        {
            var downVotes = this.postRepository.All()
                 .Where(x => x.PostId == postId)
                 .Where(x => x.Type < 0)
                 .Sum(x => (int)x.Type);
            return downVotes;
        }

        public int GetVote(int postId)
        {
            var upVotes = this.postRepository.All()
               .Where(x => x.PostId == postId)
               .Where(x => x.Type > 0)
               .Sum(x => (int)x.Type);
            return upVotes;
        }

        public async Task VoteAsync(int postId, string userId, bool isUpVote)
        {
            var postVote = this.postRepository.All()
                .FirstOrDefault(x => x.PostId == postId && x.UserId == userId);
            if (postVote != null)
            {
                postVote.Type = isUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                postVote = new VotePost
                {
                    PostId = postId,
                    UserId = userId,
                    Type = isUpVote ? VoteType.UpVote : VoteType.DownVote
                };

                await this.postRepository.AddAsync(postVote);
            }
            await this.postRepository.SaveChangesAsync();

        }
    }
}

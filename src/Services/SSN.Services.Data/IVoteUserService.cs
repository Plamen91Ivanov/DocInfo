using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSN.Services.Data
{
   public interface IVoteUserService
    {
        Task VoteAsync(int postId, string userId, bool isUpVote);

        int GetVote(int postId);

        int GetDownVote(int postId);
    }
}

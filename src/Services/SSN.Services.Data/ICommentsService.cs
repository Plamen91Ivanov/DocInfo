using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSN.Services.Data
{
    public interface ICommentsService
    {
        Task Create(int postId, string userId, string content, int? parentId = null);

        bool IsInSamePost(int commentId, int postId);
    }
}

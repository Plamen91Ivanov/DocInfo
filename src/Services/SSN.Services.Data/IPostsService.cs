using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSN.Services.Data
{
    public interface IPostsService
    {
        Task<int> CreateAsync(string content, int userAccId, string userId);

        Task<int> DeleteAsync(int postId);

        T GetById<T>(int id);

        T GetPostById<T>(int id);

        IEnumerable<T> GetByCategoryId<T>(int userAccId, int? take = null, int skip = 0);

        int GetCountByCategoryId(int userAccId);
    }
}

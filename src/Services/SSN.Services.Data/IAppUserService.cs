using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSN.Services.Data
{
    public interface IAppUserService
    {
        T GetByUserId<T>(string appUserId);

        IEnumerable<T> GetAllImages<T>(string appUserId);

        Task<int> AddPhotoAsync(
             string ImagePath,
             string id
             );
    }
}

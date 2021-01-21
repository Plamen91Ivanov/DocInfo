using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSN.Services.Data
{
    public interface ISearchService
    {
        IEnumerable<T> SearchUserEmail<T>(string email);

        IEnumerable<T> GetAllUsers<T>(int? count = null);

    }
}

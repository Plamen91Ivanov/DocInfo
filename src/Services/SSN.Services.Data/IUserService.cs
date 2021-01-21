using System;
using System.Collections.Generic;
using System.Text;

namespace SSN.Services.Data
{
    public interface IUserService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> Search<T>(string search);

        IEnumerable<T> SearchRegion<T>(string search);

        IEnumerable<T> SearchSpecialty<T>(string search);

        IEnumerable<T> SearchUserAndRegion<T>(string search, string region);

        IEnumerable<T> SearchUserAndSpecialty<T>(string search, string region);

        IEnumerable<T> SearchSpecialtyAndRegion<T>(string search, string region);

        IEnumerable<T> SearchUserAndRegionAndSpecialty<T>(string search, string region, string specialty);
    }
}

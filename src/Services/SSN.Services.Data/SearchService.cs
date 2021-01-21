using SSN.Data.Common.Repositories;
using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSN.Services.Data
{
    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> applicationUser;

        public SearchService(IDeletableEntityRepository<ApplicationUser> applicationUser)
        {
            this.applicationUser = applicationUser;
        }

        public IEnumerable<T> SearchUserEmail<T>(string email)
        {
            IQueryable<ApplicationUser> query = this.applicationUser.All().Where(x => x.Email.Contains(email));

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllUsers<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query = this.applicationUser.All().OrderBy(x => x.Email);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }
    }
}

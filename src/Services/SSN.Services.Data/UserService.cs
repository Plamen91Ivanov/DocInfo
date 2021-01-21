using SSN.Data.Common.Repositories;
using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSN.Services.Data
{
    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UserService (IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query = this.usersRepository.All().OrderBy(x => x.UserName);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> Search<T>(string search)
        {
            IQueryable<ApplicationUser> searchUser = this.usersRepository.All().Where(x => x.UserName.Contains(search));

            return searchUser.To<T>().ToList();
        }

        public IEnumerable<T> SearchRegion<T>(string region)
        {
            IQueryable<ApplicationUser> searchRegion = this.usersRepository.All().Where(x => x.City.Contains(region));

            return searchRegion.To<T>().ToList();
        }

        public IEnumerable<T> SearchSpecialty<T>(string specialty)
        {
            IQueryable<ApplicationUser> searchSpecialty = this.usersRepository.All().Where(x => x.Specialty.Contains(specialty));

            return searchSpecialty.To<T>().ToList();
        }

        public IEnumerable<T> SearchUserAndRegion<T>(string search, string region)
        {
            IQueryable<ApplicationUser> searchUserAndRegion = this.usersRepository.All().Where(x => x.City.Contains(region) && x.UserName.Contains(search));

            return searchUserAndRegion.To<T>().ToList();
        }

        public IEnumerable<T> SearchUserAndSpecialty<T>(string search, string specialty)
        {
            IQueryable<ApplicationUser> searchUserAndRegion = this.usersRepository.All().Where(x => x.Specialty.Contains(specialty) && x.UserName.Contains(search));

            return searchUserAndRegion.To<T>().ToList();
        }

        public IEnumerable<T> SearchSpecialtyAndRegion<T>(string speacialty, string region)
        {
            IQueryable<ApplicationUser> searchUserAndRegion = this.usersRepository.All().Where(x => x.City.Contains(region) && x.Specialty.Contains(speacialty));

            return searchUserAndRegion.To<T>().ToList();
        }

        public IEnumerable<T> SearchUserAndRegionAndSpecialty<T>(string search, string region, string specialty)
        {
            IQueryable<ApplicationUser> searchUserAndRegionAndSpecialty = this.usersRepository.All().Where(x => x.City.Contains(region) && x.UserName.Contains(search) && x.Specialty.Contains(specialty));

            return searchUserAndRegionAndSpecialty.To<T>().ToList();
        }

    }
}

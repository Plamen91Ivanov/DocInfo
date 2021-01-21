using SSN.Data.Common.Repositories;
using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSN.Services.Data
{
    public class NameService : INameService
    {
        private readonly IDeletableEntityRepository<User> usersRepository;

        public NameService(IDeletableEntityRepository<User> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public T GetById<T>(string city)
        {
            var category = this.usersRepository.All()
               .Where(x => x.Name == city)
               .To<T>().FirstOrDefault();

            return category;
        }
    }
}

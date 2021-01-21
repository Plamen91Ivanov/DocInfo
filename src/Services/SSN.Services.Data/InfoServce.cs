using SSN.Data.Common.Repositories;
using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SSN.Services.Data
{
    public class InfoServce : IInfoServce
    {
        private readonly IDeletableEntityRepository<Information> deletableEntityRepository;

        public InfoServce(IDeletableEntityRepository<Information> deletableEntityRepository)
        {
            this.deletableEntityRepository = deletableEntityRepository;
        }

        public IEnumerable<T> GetInfo<T>()
        {
                 var query = this.deletableEntityRepository.All()
                .GroupBy(x => x.Neg)
                .Where(x => x.Count() > 1)
                .Select(v => v.Key);

                 return query.To<T>().ToList();

        }

        public T GetAll<T>()
        {
            var add = this.deletableEntityRepository.All()
               .To<T>().FirstOrDefault();
            return add;
        }
    }
}

using SSN.Data.Common.Repositories;
using SSN.Data.Models;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSN.Services.Data
{
    public class AppUserService : IAppUserService
    {
        private readonly IDeletableEntityRepository<Images> imageRepository;

        public AppUserService(
            IDeletableEntityRepository<Images> imageRepository
            )
        {
            this.imageRepository = imageRepository;
        }
 
        public async Task<int> AddPhotoAsync(string imagePath, string userId)
        {
            var addPhoto = new Images
            {
                ImagePath = imagePath,
                UserId = userId,
            };

            await this.imageRepository.AddAsync(addPhoto);
            await this.imageRepository.SaveChangesAsync();
            return addPhoto.Id;
        }

        public IEnumerable<T> GetAllImages<T>(string appUserId)
        {
            IQueryable<Images> query = this.imageRepository.All().Where(x => x.UserId == appUserId);

            return query.To<T>().ToList();

        }

        public T GetByUserId<T>(string appUserId)
        {
            var add = this.imageRepository.All().Where(x => x.UserId == appUserId)
               .To<T>().FirstOrDefault();
            return add;
        }
    }
}

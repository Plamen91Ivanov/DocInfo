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
    public class PostsService : IPostsService
    {

        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(
            IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public async Task<int> CreateAsync(string content, int userAccId, string userId)
        {
            var post = new Post
            {
                UserAccId = userAccId,
                Content = content,
                UserId = userId,
            };

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();
            return post.Id;
        }

        public async Task<int> DeleteAsync(int postId)
        {
            var post = this.postsRepository.All()
                .FirstOrDefault(x => x.Id == postId);
            post.IsDeleted = true;

            await this.postsRepository.SaveChangesAsync();

            return 1;
        }

        public T GetById<T>(int id)
        {
            var post = this.postsRepository.All().Where(x => x.UserAccId == id)
               .To<T>().FirstOrDefault();
            return post;
        }

        public T GetPostById<T>(int id)
        {
            var post = this.postsRepository.All().Where(x => x.Id == id)
               .To<T>().FirstOrDefault();
            return post;
        }

        public IEnumerable<T> GetByCategoryId<T>(int userAccId, int? take = null, int skip = 0)
        {
            var query = this.postsRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Where(x => x.UserAccId == userAccId).Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        //what happend here 
        public int GetCountByCategoryId(int userAccId)
        {
            return this.postsRepository.All().Count(x => x.UserAccId == userAccId);
        }
    }
}

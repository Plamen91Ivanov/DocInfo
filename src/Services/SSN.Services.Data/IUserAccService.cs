using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SSN.Services.Data
{
    public interface IUserAccService
    {
        List<int> MostVotedDoc();

        List<int> MostCommentDoc();

        IEnumerable<T> GetAll<T>(int? count = null);

        IEnumerable<T> GetUser<T>(string name);

        IEnumerable<T> Search<T>(string search, int? take = null, int skip = 0);

        int SearchCount(string search, string region, string specialty);

        IEnumerable<T> SearchRegion<T>(string search, int? take = null, int skip = 0);

        IEnumerable<T> SearchSpecialty<T>(string search, int? take = null, int skip = 0);

        IEnumerable<T> SearchUserAndRegion<T>(string search, string region, int? take = null, int skip = 0);

        IEnumerable<T> SearchUserAndSpecialty<T>(string search, string region, int? take = null, int skip = 0);

        IEnumerable<T> SearchSpecialtyAndRegion<T>(string search, string region, int? take = null, int skip = 0);

        IEnumerable<T> SearchUserAndRegionAndSpecialty<T>(string search, string region, string specialty, int? take = null, int skip = 0);

        T GetById<T>(string name);

        IEnumerable<T> GetMostVoted<T>(List<int> mostVotedDocsId);

        IEnumerable<T> GetMostComment<T>(List<int> mostCommentDocsId);

        T GetByUserName<T>(string name);

        T GetByUserId<T>(int id);

        Task<int> AddAsync(
               string email,
               string name,
               string city,
               string specialty,
               string uniqueFileName
               );

        Task<int> AddPhotoAsync(
               string ImagePath,
               string id
               );

        Task<int> AddDoctor(int fromId, int toId);

        Task<int> AddAsyncDoctor();

    }
}

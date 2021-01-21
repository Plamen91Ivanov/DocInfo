using SSN.Data.Models;
using SSN.Data.Common.Repositories;
using SSN.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AngleSharp.Html.Parser;
using System.Net;
using AngleSharp.Dom;
using System.Net.Http;

namespace SSN.Services.Data
{
    public class UserAccService : IUserAccService
    {
        private readonly IDeletableEntityRepository<UserAcc> addRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> addUserRepository;
        private readonly IDeletableEntityRepository<Images> addImageRepository;
        private readonly IRepository<VoteUser> repository;
        private readonly IRepository<VoteUser> voteRepository;
        private readonly IRepository<Post> postRepository;

        public UserAccService(
            IDeletableEntityRepository<UserAcc> addRepository,
            IDeletableEntityRepository<ApplicationUser> addUserRepository,
            IDeletableEntityRepository<Images> addImageRepository,
            IRepository<VoteUser> repository,
            IRepository<VoteUser> voteRepository,
            IRepository<Post> postRepository)
        {
            this.addRepository = addRepository;
            this.addUserRepository = addUserRepository;
            this.addImageRepository = addImageRepository;
            this.repository = repository;
            this.voteRepository = voteRepository;
            this.postRepository = postRepository;
        }

        public List<int> MostVotedDoc()
        {
            var mostVotedDoc = this.voteRepository.All()
                .GroupBy(x => x.UserAccId)
                .Select(x => new
                {
                    id = x.Key,
                    count = x.Count()
                })
                .OrderByDescending(x => x.count)
                .Take(3)
                .ToList();

            var docId = new List<int>();

            foreach (var item in mostVotedDoc)
            {
                docId.Add(item.id);
            }

            return docId;
        }

        public List<int> MostCommentDoc()
        {
            var mostCommendDoc = this.postRepository.All()
                .GroupBy(x => x.UserAccId)
                .Select(x => new
                {
                    id = x.Key,
                    count = x.Count()
                })
                .OrderByDescending(x => x.count)
                .Take(3)
                .ToList();

            var commendDocId = new List<int>();

            foreach (var item in mostCommendDoc)
            {
                commendDocId.Add(item.id);
            }

            return commendDocId;
        }

        public IEnumerable<T> GetMostVoted<T>(List<int> mostVotedDocsId)
        {
            var mostVoted = this.addRepository.All().Where(x => x.Id == mostVotedDocsId[0] || x.Id == mostVotedDocsId[1] || x.Id == mostVotedDocsId[2])
            .To<T>().ToList();

            return mostVoted;
        }

        public IEnumerable<T> GetMostComment<T>(List<int> mostCommentDoc)
        {
            var mostComment = this.addRepository.All().Where(x => x.Id == mostCommentDoc[0] || x.Id == mostCommentDoc[1] || x.Id == mostCommentDoc[2])
                .To<T>().ToList();

            return mostComment;
        }

        public T GetById<T>(string name)
        {
            var add = this.addRepository.All().Where(x => x.Name == name)
               .To<T>().FirstOrDefault();

            return add;
        }

        public T GetByUserId<T>(int id)
        {
            var add = this.addRepository.All().Where(x => x.Id == id)
               .To<T>().FirstOrDefault();
            return add;
        }

        public IEnumerable<T> GetUser<T>(string name)
        {
            IQueryable<UserAcc> query = this.addRepository.All().Where(x => x.Name == name);

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<UserAcc> query = this.addRepository.All().OrderBy(x => x.Name);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> Search<T>(string search, int? take = null, int skip = 0)
        {
            IQueryable<UserAcc> searchUser = this.addRepository.All().Where(x => x.Name.Contains(search)).Skip(skip);
            if (take.HasValue)
            {
                searchUser = searchUser.Take(take.Value);
            }

            return searchUser.To<T>().ToList();
        }

        public int SearchCount(string search, string region, string specialty)
        {
            if (search != null && region != null && specialty != "Specialty" && specialty != null)
            {
                var count = this.addRepository.All().Where(x => x.City.Contains(region) && x.Name.Contains(search) && x.Specialty.Contains(specialty)).Count();
                return count;
            }
            else if (search != null && region != null)
            {
                var count = this.addRepository.All().Where(x => x.City.Contains(region) && x.Name.Contains(search)).Count();
                return count;
            }
            else if (search != null && specialty != "Specialty" && specialty != null)
            {
                var count = this.addRepository.All().Where(x => x.Name.Contains(search) && x.Specialty.Contains(specialty)).Count();
                return count;
            }
            else if (region != null && specialty != "Specialty" && specialty != null)
            {
                var count = this.addRepository.All().Where(x => x.City.Contains(region) && x.Specialty.Contains(specialty)).Count();
                return count;
            }
            else if (specialty != "Specialty" && specialty != null)
            {
                var specialtyCount = this.addRepository.All().Where(x => x.Specialty.Contains(specialty)).Count();
                return specialtyCount;
            }
            else if (region != null)
            {
                var regionCount = this.addRepository.All().Where(x => x.City.Contains(region)).Count();
                return regionCount;
            }
            else if (search != null)
            {

                var searchCount = this.addRepository.All().Where(x => x.Name.Contains(search)).Count();
                return searchCount;
            }
            else
            {
                return 0;
            }
        }

        public IEnumerable<T> SearchRegion<T>(string region, int? take = null, int skip = 0)
        {
            IQueryable<UserAcc> searchRegion = this.addRepository.All().Where(x => x.City.Contains(region)).Skip(skip);
            if (take.HasValue)
            {
                searchRegion = searchRegion.Take(take.Value);
            }

            return searchRegion.To<T>().ToList();
        }

        public IEnumerable<T> SearchSpecialty<T>(string specialty, int? take = null, int skip = 0)
        {
            IQueryable<UserAcc> searchSpecialty = this.addRepository.All().Where(x => x.Specialty.Contains(specialty)).Skip(skip);
            if (take.HasValue)
            {
                searchSpecialty = searchSpecialty.Take(take.Value);
            }

            return searchSpecialty.To<T>().ToList();
        }

        public IEnumerable<T> SearchUserAndRegion<T>(string search, string region, int? take = null, int skip = 0)
        {
            IQueryable<UserAcc> searchUserAndRegion = this.addRepository.All().Where(x => x.City.Contains(region) && x.Name.Contains(search)).Skip(skip);
            if (take.HasValue)
            {
                searchUserAndRegion = searchUserAndRegion.Take(take.Value);
            }

            return searchUserAndRegion.To<T>().ToList();
        }

        public IEnumerable<T> SearchUserAndSpecialty<T>(string search, string specialty, int? take = null, int skip = 0)
        {
            IQueryable<UserAcc> searchUserAndRegion = this.addRepository.All().Where(x => x.Specialty.Contains(specialty) && x.Name.Contains(search)).Skip(skip);
            if (take.HasValue)
            {
                searchUserAndRegion = searchUserAndRegion.Take(take.Value);
            }

            return searchUserAndRegion.To<T>().ToList();
        }

        public IEnumerable<T> SearchSpecialtyAndRegion<T>(string speacialty, string region, int? take = null, int skip = 0)
        {
            IQueryable<UserAcc> searchUserAndRegion = this.addRepository.All().Where(x => x.City.Contains(region) && x.Specialty.Contains(speacialty)).Skip(skip);
            if (take.HasValue)
            {
                searchUserAndRegion = searchUserAndRegion.Take(take.Value);
            }

            return searchUserAndRegion.To<T>().ToList();
        }

        public IEnumerable<T> SearchUserAndRegionAndSpecialty<T>(string search, string region, string specialty, int? take = null, int skip = 0)
        {
            IQueryable<UserAcc> searchUserAndRegionAndSpecialty = this.addRepository.All().Where(x => x.City.Contains(region) && x.Name.Contains(search) && x.Specialty.Contains(specialty)).Skip(skip);
            if (take.HasValue)
            {
                searchUserAndRegionAndSpecialty = searchUserAndRegionAndSpecialty.Take(take.Value);
            }

            return searchUserAndRegionAndSpecialty.To<T>().ToList();
        }

        public async Task<int> AddAsync(string email, string name, string city, string specialty, string uniqueFileName)
        {
            var addDoc = new UserAcc
            {
                Email = email,
                Name = name,
                City = city,
                Specialty = specialty,
                FilePath = uniqueFileName,
            };

            await this.addRepository.AddAsync(addDoc);
            await this.addRepository.SaveChangesAsync();
            return addDoc.Id;
        }

        public T GetByUserName<T>(string email)
        {
            var add = this.addUserRepository.All().Where(x => x.UserName == email)
               .To<T>().FirstOrDefault();
            return add;
        }

        public async Task<int> AddPhotoAsync(string imagePath, string userId)
        {
            var addPhoto = new Images
            {
                ImagePath = imagePath,
                UserId = userId,
            };

            await this.addImageRepository.AddAsync(addPhoto);
            await this.addImageRepository.SaveChangesAsync();
            return addPhoto.Id;
        }

        public async Task<int> AddDoctor(int fromId, int toId)
        {
            var parser = new AngleSharp.Html.Parser.HtmlParser();
            var client = new HttpClient();


            for (var page = fromId; page <= toId; page++)
            {
                Console.Write('^');
                var url = $"https://bestdoctors.bg/doctors/p/{page}";
                string html = null;
                for (var i = 0; i < 10; i++)
                {
                    try
                    {
                        var response = await client.GetAsync(url);
                        html = await response.Content.ReadAsStringAsync();
                        break;
                    }
                    catch
                    {
                        Console.Write('!');
                        Thread.Sleep(500);
                    }
                }

                if (string.IsNullOrWhiteSpace(html))
                {
                    continue;
                }

                var document = parser.ParseDocument(html);
                var docBox = document.GetElementsByClassName("docbox");
                foreach (var item in docBox)
                {
                    var docNameCollection = item.GetElementsByTagName("h4");
                    var docName = docNameCollection[0].TextContent.Replace("\n", string.Empty).Trim();

                    var cityCollection = item.GetElementsByTagName("p");
                    var cityReplace = cityCollection[1].TextContent.Replace(",", string.Empty);
                    var city = cityReplace.Replace("\n", string.Empty).Trim();

                    var specialtyCollection = item.GetElementsByTagName("span");
                    var specialty = specialtyCollection[0].TextContent.Replace("\n", string.Empty).Trim();

                    var imageCollection = item.GetElementsByTagName("img");
                    var imageAttributes = imageCollection[0].Attributes;
                    var imageSrc = imageAttributes[0].Value;

                    var hospitalCollection = item.GetElementsByTagName("a");
                    if (hospitalCollection.Length > 1)
                    {
                        var hospital = hospitalCollection[1].TextContent.Replace("\n", string.Empty).Trim();

                        var addDoctor = new UserAcc()
                        {
                            Name = docName,
                            City = city,
                            SecondName = hospital,
                            Specialty = specialty,
                            FilePath = imageSrc,
                        };

                        await this.addRepository.AddAsync(addDoctor);
                    }
                    else
                    {
                        var addDoctor = new UserAcc()
                        {
                            Name = docName,
                            City = city,
                            Specialty = specialty,
                            FilePath = imageSrc,
                        };

                        await this.addRepository.AddAsync(addDoctor);
                    }
                }
            }

            await this.addRepository.SaveChangesAsync();

            return 1;
        }

        public async Task<int> AddAsyncDoctor()
        {
            var parser = new AngleSharp.Html.Parser.HtmlParser();
            var client = new HttpClient();


            for (int cityNumber = 1; cityNumber < 28; cityNumber++)
            {
                var city = "";
                if (cityNumber != 1 && cityNumber != 2 && cityNumber != 23)
                {
                    switch (cityNumber)
                    {
                        case 1:
                            city = "готово";
                            break;
                        case 2:
                            city = "готово";
                            break;
                        case 3:
                            city = "Бургас";
                            break;
                        case 4:
                            city = "Варна";
                            break;
                        case 5:
                            city = "Велико Търново";
                            break;
                        case 6:
                            city = "Видин";
                            break;
                        case 7:
                            city = "Враца";
                            break;
                        case 8:
                            city = "Габрово";
                            break;
                        case 9:
                            city = "Добрич";
                            break;
                        case 10:
                            city = "Кърджали";
                            break;
                        case 11:
                            city = "Кюстендил";
                            break;
                        case 12:
                            city = "Ловеч";
                            break;
                        case 13:
                            city = "Монтана";
                            break;
                        case 14:
                            city = "Пазарджик";
                            break;
                        case 15:
                            city = "Перник";
                            break;
                        case 16:
                            city = "Плевен";
                            break;
                        case 17:
                            city = "Пловдив";
                            break;
                        case 18:
                            city = "Разград";
                            break;
                        case 19:
                            city = "Русе";
                            break;
                        case 20:
                            city = "Силистра";
                            break;
                        case 21:
                            city = "Сливен";
                            break;
                        case 22:
                            city = "Смолян";
                            break;
                        case 23:
                            city = "София";
                            break;
                        case 24:
                            city = "София-област";
                            break;
                        case 25:
                            city = "Стара Загора";
                            break;
                        case 26:
                            city = "Търговище";
                            break;
                        case 27:
                            city = "Хасково";
                            break;
                        case 28:
                            city = "Шумен";
                            break;
                        case 29:
                            city = "Ямбол";
                            break;
                    }
                }
                else
                {
                    continue;
                }
                var numberOfDoctors = 1.0;
                for (var page = 1; page <= numberOfDoctors; page++)
                {
                    var url = $"https://blsbg.eu/bg/medics/unionlist/{cityNumber}?UIN_page={page}&ajax=yw0";
                    string html = null;
                    for (var i = 0; i < 5; i++)
                    {
                        try
                        {
                            var response = await client.GetAsync(url);
                            html = await response.Content.ReadAsStringAsync();
                            break;
                        }
                        catch
                        {
                            Console.Write('!');
                            Thread.Sleep(500);
                        }
                    }

                    if (string.IsNullOrWhiteSpace(html))
                    {
                        continue;
                    }

                    var document = parser.ParseDocument(html);
                    var numberOfDoctorsDiv = document.GetElementsByClassName("summary");
                    var numberOfDoctorsArr = numberOfDoctorsDiv[0].TextContent.Split(' ');
                    numberOfDoctors = Math.Ceiling(double.Parse(numberOfDoctorsArr[numberOfDoctorsArr.Count() - 1]) / 25);

                    var doctorsTable = document.GetElementsByClassName("items");
                    var test = document.GetElementsByTagName("tr");



                    var count = 1;
                    foreach (var item in test)
                    {
                        var tdElement = item.GetElementsByTagName("td");

                        if (count == 1)
                        {
                            count++;
                            continue;
                        }

                        var addDoctor = new UserAcc()
                        {
                            UIN = long.Parse(tdElement[0].TextContent),
                            Name = tdElement[2].TextContent,
                            City = city,
                            Specialty = tdElement[3].TextContent,
                        };

                        await this.addRepository.AddAsync(addDoctor);

                    }

                }
            }
            await this.addRepository.SaveChangesAsync();

            return 1;
        }

    }
}
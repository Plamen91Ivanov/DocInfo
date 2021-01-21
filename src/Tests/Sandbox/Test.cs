using AngleSharp.Html.Parser;
using SSN.Data.Common.Repositories;
using SSN.Data.Models;
using SSN.Services.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox
{
    public class Test
    {
        private readonly SSN.Data.Common.Repositories.IDeletableEntityRepository<UserAcc> repository;

        public Test(IDeletableEntityRepository<UserAcc> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<int>> GatherData(int fromId, int toId)
        {
            var jokes = new List<JokeModel>();

            var users = new List<Users>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var parser = new HtmlParser();
            var webClient = new WebClient { Encoding = Encoding.GetEncoding("windows-1251") };

            for (var i = fromId; i <= toId; i++)
            {
                var url = $"https://users.pomagalo.com/204,1,0,0,0,0,0,0,0,0,?page=3";
                string html = null;
                for (var j = 0; j < 10; j++)
                {
                    try
                    {
                        html = webClient.DownloadString(url);
                        break;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(1000);
                    }
                }

                if (string.IsNullOrWhiteSpace(html))
                {
                    continue;
                }

                var document = parser.ParseDocument(html);
                var jokeContent = document.QuerySelector(".my_buddies_list");
                var getElements = jokeContent.QuerySelectorAll("tr");
                foreach (var element in getElements)
                {
                    var contentElement = element.GetElementsByClassName("text");

                    foreach (var item in contentElement)
                    {
                        if (string.IsNullOrWhiteSpace(item.QuerySelector("span").InnerHtml))
                        {
                            break;
                        }
                        var test = item.QuerySelector("span").InnerHtml;

                        var userName = new Users { User = test };
                        users.Add(userName);

                        var userAcc = new UserAcc
                        {
                            Name = test,
                        };

                        await this.repository.AddAsync(userAcc);
                        await this.repository.SaveChangesAsync();

                        Console.WriteLine($"{test}");
                    }
                    //  var userName= new Users { User = contentElement };
                    //  users.Add(userName);

                }

                var categoryName = jokeContent.QuerySelector("tr")?.TextContent?.Trim();

                if (!string.IsNullOrWhiteSpace(jokeContent.TextContent) &&
                    !string.IsNullOrWhiteSpace(categoryName))
                {
                    var jokeModel = new JokeModel { Category = categoryName, Content = jokeContent.TextContent };
                    jokes.Add(jokeModel);
                }

            }

            return null;
        }

        public class JokeModel
        {
            public string Category { get; set; }

            public string Content { get; set; }
        }

        public class Users
        {
            public string User { get; set; }
        }

    }
}

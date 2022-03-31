using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

using Take.Api.TestApi.Facades.Interfaces;
using Take.Api.TestApi.Services;

namespace Take.Api.TestApi.Facades
{
    public class HomeFacade : IHomeFacade
    {
        private readonly IHomeService _homeService;

        public HomeFacade(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<object> GetTakeBlipRepositoriesAsync()
        {
            var response = await _homeService.GetTakeBlipRepositoriesAsync();
            var responseToJson = JsonConvert.DeserializeObject<List<GithubResponse>>(response);
    
            var cRespositories = responseToJson.Where(repo => repo.Language == "C#");
            var orderReposByDate = cRespositories.OrderBy(x => x.RepositoreCreatedAt).ToList();
            var firstFiveRepositories = orderReposByDate.GetRange(0, 5);

            return firstFiveRepositories;
        }
    }
}

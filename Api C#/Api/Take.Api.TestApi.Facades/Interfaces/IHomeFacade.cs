using System.Threading.Tasks;
using System.Collections.Generic;

namespace Take.Api.TestApi.Facades.Interfaces
{
    public interface IHomeFacade
    {
        /// <summary>
        /// Gets a list of Take Blip repositories
        /// </summary>
        /// <returns>A list of objects </returns>
        Task<object> GetTakeBlipRepositoriesAsync();
    }
}

using MonkeyHubApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonkeyHubApp.Services
{
    public interface IMonkeyHubApiService
    {
        Task<List<Tag>> GetTagsAsync();

        Task<List<Content>> GetContentsByTagIdAsync(string TagId);

        Task<List<Content>> GetContentsByFilterAsync(string filter);
    }
}

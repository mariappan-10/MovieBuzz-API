using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoriesContracts
{
    public interface IWatchlistRepository
    {
        Task AddAsync(WatchListData item);
        Task RemoveAsync(Guid userId, string imdbId);

        Task<bool> ExistsAsync(Guid userId, string imdbId);

        Task<List<string>> GetByUserIdAsync(Guid userId);

        Task<List<WatchListData>> GetAllAsync();

    }
}

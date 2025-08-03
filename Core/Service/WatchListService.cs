using Core.Entities;
using Core.RepositoriesContracts;
using Core.ServiceContracts;

namespace Core.Service
{
    public class WatchListService : IWatchListService
    {

        private readonly IWatchlistRepository _watchlistRepository;

        public WatchListService(IWatchlistRepository watchlistRepository)
        {
            _watchlistRepository = watchlistRepository;
        }


        public async Task<(bool Success, string Message)> AddMovieToWatchList(Guid userId, string imdbId)
        {
            if (await _watchlistRepository.ExistsAsync(userId, imdbId))
                return (false, "Movie is already in the watchlist.");

            await _watchlistRepository.AddAsync(new WatchListData
            {
                ApplicationUserId = userId,
                imdbId = imdbId
            });

            return (true, "Movie added to watchlist.");
        }

        public Task<List<string>> GetWatchListByUserId(Guid userId)
        {
            return _watchlistRepository.GetByUserIdAsync(userId);
        }

        public async Task<(bool Success, string Message)> RemoveMovieFromWatchList(Guid userId, string imdbId)
        {
            if (!await _watchlistRepository.ExistsAsync(userId, imdbId))
                return (false, "Movie is not in the watchlist.");
            await _watchlistRepository.RemoveAsync(userId, imdbId);
            return (true, "Movie removed from watchlist.");
        }

        public async Task<List<WatchListData>> GetAllWatchLists()
        {
            return await _watchlistRepository.GetAllAsync();
        }
    }
}

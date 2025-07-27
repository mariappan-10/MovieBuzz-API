using Core.Entities;

namespace Core.ServiceContracts
{
    public interface IWatchListService
    {
        Task<(bool Success, string Message)> AddMovieToWatchList(Guid userId, string imdbId);
        Task<(bool Success, string Message)> RemoveMovieFromWatchList(Guid userId, string imdbId);
        Task<List<string>> GetWatchListByUserId(Guid userId);
    }
}

using Core.DTO;
using Core.Entities;

namespace Core.ServiceContracts
{
    public interface IMovieService
    {
        public Task<MovieData> GetMovieDataByTitle(string title);
        public Task<MovieData> GetMovieDataByImdbId(string imdbId);

        public Task<SearchPreviewResponse> GetSearchPreview(string title);
        public Task<List<WatchListPreviewDTO>> GetWatchListPreview(List<string> imdbIds);

    }
      
}

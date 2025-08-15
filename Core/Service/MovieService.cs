using Core.DTO;
using Core.Entities;
using Core.ServiceContracts;
using System.Text.Json;

namespace Core.Service
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "76ff5f42"; // Replace with your actual OMDB API key
        private readonly string _baseUrl = "https://www.omdbapi.com/";

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MovieData> GetMovieDataByTitle(string title)
        {
            var url = $"{_baseUrl}?apikey={_apiKey}&t={Uri.EscapeDataString(title)}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"HTTP error: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            var movie = JsonSerializer.Deserialize<MovieData>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (movie.Response == "False")
                throw new Exception($"API error: {movie.Error}");

        
            return movie;
        }

        public async Task<MovieData> GetMovieDataByImdbId(string imdbId)
        {
            var url = $"{_baseUrl}?apikey={_apiKey}&i={Uri.EscapeDataString(imdbId)}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"HTTP error: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            var movie = JsonSerializer.Deserialize<MovieData>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (movie.Response == "False")
                throw new Exception($"API error: {movie.Error}");


            return movie;
        }

        public async Task<SearchPreviewResponse> GetSearchPreview(string title, int? year = null, int? page = null)
        {
            var url = $"{_baseUrl}?apikey={_apiKey}&s={Uri.EscapeDataString(title)}&type=movie";
            if (year.HasValue)
            {
                url += $"&y={year.Value}";
            }
            if (page.HasValue)
            {
                url += $"&page={page.Value}";
            }
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"HTTP error: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"OMDb API response: {content}"); // For debugging purposes
            var searchResponse = JsonSerializer.Deserialize<SearchPreviewResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (!searchResponse.IsSuccess)
                throw new Exception($"OMDb API error");

            return searchResponse;
        }

        public async Task<List<WatchListPreviewDTO>> GetWatchListPreview(List<string> imdbIds)
        {
            var result = new List<WatchListPreviewDTO>();

            foreach (var id in imdbIds)
            {
                var url = $"{_baseUrl}?apikey={_apiKey}&i={id}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    continue;

                var content = await response.Content.ReadAsStringAsync();
                var movie = JsonSerializer.Deserialize<MovieData>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (movie != null && movie.Response == "True")
                {
                    result.Add(new WatchListPreviewDTO
                    {
                        Title = movie.Title,
                        Poster = movie.Poster
                    });
                }
            }

            return result;
        }
    }

}


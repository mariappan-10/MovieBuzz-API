using Core.ServiceContracts;
using Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IWatchListService _watchListService;

        public MoviesController(IMovieService IMovieService, IWatchListService watchListService, ApplicationDbContext applicationDbContext)
        {
            _movieService = IMovieService;
            _watchListService = watchListService;
        }

        private string? GetUserId()
        {
            return User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet]
        public IActionResult Testfunc()
        {
            return Ok("Hello");
        }

        //[HttpGet("{title}")]
        //public async Task<IActionResult> GetMovieByTitle(string title)
        //{
        //    try
        //    {
        //        var movie = await _movieService.GetMovieDataByTitle(title);
        //        return Ok(movie);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [AllowAnonymous]
        [HttpGet("{imdbId}")]
        public async Task<IActionResult> GetMovieByImdbId(string imdbId)
        {
            try
            {
                var movie = await _movieService.GetMovieDataByImdbId(imdbId);
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpGet("search/{title}")]
        public async Task<IActionResult> GetSearchPreview(string title)
        {
            try
            {
                var searchResponse = await _movieService.GetSearchPreview(title);
                return Ok(searchResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("add-to-watchlist")]
        public async Task<IActionResult> AddMovieToWatchList([FromQuery] string imdbId)
        {
            if (string.IsNullOrWhiteSpace(imdbId))
                return BadRequest("IMDb ID is required.");

            var userIdString = GetUserId();
            if (!Guid.TryParse(userIdString, out var userId))
                return Unauthorized("Invalid user ID from token.");

            try
            {
                var (success, message) = await _watchListService.AddMovieToWatchList(userId, imdbId);

                if (!success)
                    return Conflict(message); // 409 Conflict

                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpDelete("remove-from-watchlist")]

        public async Task<IActionResult> RemoveMovieFromWatchList([FromQuery] string imdbId)
        {
            if (string.IsNullOrWhiteSpace(imdbId))
                return BadRequest("IMDb ID is required.");
            var userIdString = GetUserId();
            if (!Guid.TryParse(userIdString, out var userId))
                return Unauthorized("Invalid user ID from token.");
            try
            {
                var (success, message) = await _watchListService.RemoveMovieFromWatchList(userId, imdbId);
                if (!success)
                    return NotFound(message); // 404 Not Found
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("display-watchlist")]
        public async Task<IActionResult> GetWatchList()
        {
            var userIdString = GetUserId();
            if (!Guid.TryParse(userIdString, out var userId))
                return Unauthorized("Invalid user ID from token.");
            try
            {
                var watchListIds = await _watchListService.GetWatchListByUserId(userId);
                //var watchListPreview = await _movieService.GetWatchListPreview(watchListIds);
                return Ok(watchListIds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
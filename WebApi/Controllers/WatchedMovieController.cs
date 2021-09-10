using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchedMovieController : ControllerBase
    {
        private IWatchedMovieService _watchedMovieService;
        private readonly ILogger<WatchedMovieController> _logger;

        public WatchedMovieController(IWatchedMovieService watchedMovieService, ILogger<WatchedMovieController> logger)
        {
            _watchedMovieService = watchedMovieService;
            _logger = logger;
        }

        [HttpPost("add")]
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> Add([FromQuery] int filmId)
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _watchedMovieService.AddMovieToWatchedList(filmId, token);
            if (result.Success)
            {
                _logger.LogInformation($"The movie with {filmId} has added to watched list.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("list")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> List()
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _watchedMovieService.ListWatchedMovieDto(token);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> Delete(WatchedMovieDto watchedMovieDto)
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _watchedMovieService.DeleteMovieFromWatchedList(watchedMovieDto.WatchedMovieId, token, watchedMovieDto.Movie.Id);
            if (result.Success)
            {
                _logger.LogInformation($"The movie with {watchedMovieDto.Movie.Id} has deleted from watched list.");

                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}

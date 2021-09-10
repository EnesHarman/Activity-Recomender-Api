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
    public class MovieWishListController : ControllerBase
    {
        private IMovieWishListService _movieWishListService;
        private ILogger<MovieWishListController> _logger;

        public MovieWishListController(IMovieWishListService movieWishListService, ILogger<MovieWishListController> logger)
        {
            _movieWishListService = movieWishListService;
            _logger = logger;
        }

        [HttpPost("add")]
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> Add([FromQuery] int filmId)
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _movieWishListService.AddMovieToWishList(filmId, token);
            if (result.Success)
            {
                _logger.LogInformation($"The movie with {filmId} id has added to wish list.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("list")]
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> List()
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _movieWishListService.ListWishedMovie(token);
            if (result.Success)
            {

                return Ok(result.Data);
            }
            return BadRequest(result.Message);

        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Delete([FromBody] MovieWishListDto movieWishListDto)
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _movieWishListService.DeleteMovieFromWishList(movieWishListDto.MoviesWishId, token, movieWishListDto.Movie.Id);
            if (result.Success)
            {
                _logger.LogInformation($"The movie with {movieWishListDto.Movie.Id} id has deleted from wish list.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}

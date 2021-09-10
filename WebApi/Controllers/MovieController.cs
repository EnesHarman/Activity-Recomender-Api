using Business.Abstract;
using Core.Utilities.Pagtination;
using Entities.Concrete;
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
    public class MovieController : ControllerBase
    {
        private IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;
        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] PaginationFilter paginationFilter)
        {
            var result = await _movieService.getMovieList(paginationFilter);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleById(int id)
        {
            var result = await _movieService.getMovieById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete]
        [Authorize(Roles ="Manager")]
        public async Task<IActionResult> Delete(Movie movie)
        {
            var result = await _movieService.delete(movie);
            if (result.Success)
            {
                _logger.LogInformation($"The movie ({movie.Series_Title}) has deleted from service.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        [Authorize(Roles="Manager")]
        public async Task<IActionResult> Add([FromBody] Movie movie)
        {
            var result = await _movieService.add(movie);
            if (result.Success)
            {
                _logger.LogInformation($"The movie ({movie.Series_Title}) has added to service.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message); 
        }

        [HttpGet("list/director")]
        public async Task<IActionResult> GetListByDirector([FromQuery] string director)
        {
            var result = await _movieService.getMovieListByDirector(director);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("list/star")]
        public async Task<IActionResult> GetListByStar([FromQuery] string star)
        {
            var result = await _movieService.getMovieListByArtist(star);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var result = await _movieService.getMovieByName(name);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("random")]
        [Authorize (Roles="Customer")]
        public async Task<IActionResult> GetRandomMovie()
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _movieService.getRandomMovie(token);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}

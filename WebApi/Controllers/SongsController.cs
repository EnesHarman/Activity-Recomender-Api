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
    public class SongsController : ControllerBase
    {
        private readonly ILogger<SongsController> _logger;
        private ISongService _songService;
        public SongsController(ISongService songService, ILogger<SongsController> logger)
        {
            _songService = songService;
            _logger = logger;
        }

        [HttpPost("")]
        [Authorize(Roles="Manager")]
        public async Task<IActionResult> addSong([FromBody] Song song)
        {
            var result = await _songService.addSong(song);
            if (result.Success)
            {
                _logger.LogInformation($"The song ({song.song_name}) has been added to Database.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetAll([FromQuery]PaginationFilter paginationFilter)
        {
            var result = await _songService.listSongs(paginationFilter);
            
            if (result.Success)
            {
                _logger.LogInformation("Songs has been listed.");
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("list/artist")]
        public async Task<IActionResult> getListByArtist([FromQuery]string artist)
        {
            var result = await _songService.listSongByArtist(artist);
            if (result.Success)
            {
                _logger.LogInformation($"Songs of {artist} has been listed.");
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getSongById(int id)
        {
            var result = await _songService.getSongById(id);
            if (result.Success)
            {
                _logger.LogInformation($"The song ({result.Data.song_name}) has been listed.");
                return Ok(result.Data);

            }
            return BadRequest(result.Message);

        }

        [HttpDelete]
        [Authorize(Roles = "Manager")]

        public async Task<IActionResult> deleteSong([FromBody] Song song)
        {
            var result = await _songService.deleteSong(song);
            if (result.Success)
            {
                _logger.LogInformation($"The song ({song.song_name}) has been deleted.");
                return Ok(result.Message);

            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        [Authorize(Roles = "Manager")]

        public async Task<IActionResult> updateSong([FromBody] Song song)
        {
            var result = await _songService.updateSong(song);
            if(result.Success){
                _logger.LogInformation($"The song ({song.song_name}) has been updated.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("random")]
        public async Task<IActionResult> getRandomSong()
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _songService.getRandomSong(token);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);

        }

    }
}

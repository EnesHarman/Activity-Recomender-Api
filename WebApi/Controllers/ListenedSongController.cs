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
    public class ListenedSongController : ControllerBase
    {
        private IListenedSongService _listenedSongService;
        private readonly ILogger<ListenedSongController> _logger;

        public ListenedSongController(ILogger<ListenedSongController> logger, IListenedSongService listenedSongService)
        {
            _logger = logger;
            _listenedSongService = listenedSongService;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Add([FromQuery] int songId)
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _listenedSongService.AddSongToListenedList(songId, token);
            if (result.Success)
            {
                _logger.LogInformation($"The song with {songId} has added to listened list.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("list")]
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> List()
        {

            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _listenedSongService.ListListenedSongs(token);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Delete([FromBody]ListenedSongDto listenedSongDto)
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _listenedSongService.DeleteSongFromListenedList(listenedSongDto.ListenedSongId, token, listenedSongDto.Song.Id);
            if (result.Success)
            {
                _logger.LogInformation($"The song with {listenedSongDto.Song.Id} has deleted from listened list.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}

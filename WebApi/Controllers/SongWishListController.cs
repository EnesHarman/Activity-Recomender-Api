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
    public class SongWishListController : ControllerBase
    {
        private ISongWishListService _songWishListService;
        private readonly ILogger<SongWishListController> _logger;

        public SongWishListController(ISongWishListService songWishListService, ILogger<SongWishListController> logger)
        {
            _songWishListService = songWishListService;
            _logger = logger;
        }

        [HttpGet("list")]
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> List()
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _songWishListService.ListWishedSongs(token);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> Add([FromQuery]int songId)
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _songWishListService.AddSongToWishList(songId,token);
            if (result.Success)
            {
                _logger.LogInformation($"The song with {songId} has added to wish list.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> Delete([FromBody] SongWishListDto songWishListDto)
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _songWishListService.DeleteSongFromWishList(songWishListDto.SongWishListId,token, songWishListDto.Song.Id);
            if (result.Success)
            {
                _logger.LogInformation($"The song with {songWishListDto.Song.Id} has deleted from wish list.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

    }

    
}

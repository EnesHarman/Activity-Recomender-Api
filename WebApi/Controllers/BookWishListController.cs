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
    public class BookWishListController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private IBookWishListService _bookWishListService;

        public BookWishListController(ILogger<BookController> logger, IBookWishListService bookWishListService)
        {
            _logger = logger;
            _bookWishListService = bookWishListService;
        }

        [HttpGet("list")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> List()
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _bookWishListService.ListWishBookList(token);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Add([FromQuery]int bookId)
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _bookWishListService.AddBookToWishList(bookId, token);
            if (result.Success)
            {
                _logger.LogInformation($"The book with {bookId} id has added to wish list.");

                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> Delete([FromBody]BookWishListDto bookWishListDto)
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _bookWishListService.DeleteBookFromWishList(bookWishListDto.WishBookId, token, bookWishListDto.Book.Id);
            if (result.Success)
            {
                _logger.LogInformation($"The book with {bookWishListDto.Book.Id} id has deleted from wish list.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}

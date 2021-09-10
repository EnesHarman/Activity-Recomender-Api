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
    public class ReadBookListController : ControllerBase
    {
        private IReadBookService _readBookService;
        private readonly ILogger<BookController> _logger;

        public ReadBookListController(IReadBookService readBookService, ILogger<BookController> logger)
        {
            _readBookService = readBookService;
            _logger = logger;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddReadList([FromQuery] int bookId)
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _readBookService.AddBookToReadList(bookId, token);
            if (result.Success)
            {
                _logger.LogInformation($"The book with {bookId} id has added to read list.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> DeleteReadList([FromBody] ReadBookDto readBookDto)
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _readBookService.DeleteBookToReadList(readBookDto.ReadBookId, token, readBookDto.Book.Id);
            if (result.Success)
            {
                _logger.LogInformation($"The book with {readBookDto.Book.Id} id has deleted from read list.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("list")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> ListReadBooks()
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _readBookService.ListReadBookList(token);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}

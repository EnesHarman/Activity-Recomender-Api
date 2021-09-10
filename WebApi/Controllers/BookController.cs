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
    public class BookController : ControllerBase
    {
        private IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] PaginationFilter paginationFilter)
        {
            var result = await _bookService.GetBookList(paginationFilter);
            if (result.Success)
            {
                _logger.LogInformation($"Books has been listed.");
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("")]
        [Authorize (Roles ="Manager")]
        public async Task<IActionResult> Add([FromBody] Book book)
        {
            var result = await _bookService.AddBook(book);
            if (result.Success)
            {
                _logger.LogInformation($"The book ({book.BookTitle}) has been added.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Update([FromBody] Book book)
        {
            var result = await _bookService.UpdateBook(book);
            if (result.Success)
            {
                _logger.LogInformation($"The book ({book.BookTitle}) has been updated.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);

        } 

        [HttpDelete]
        [Authorize(Roles = "Manager")]

        public async Task<IActionResult> Delete([FromBody] Book book)
        {
            var result = await _bookService.DeleteBook(book);
            if (result.Success)
            {
                _logger.LogInformation($"The book ({book.BookTitle}) has been deleted.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _bookService.GetBookById(id);
            if (result.Success)
            {
                _logger.LogInformation($"The book ({result.Data.BookTitle}) has been listed.");
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("author")]
        public async Task<IActionResult> GetByAuthor([FromQuery]string author)
        {
            var result = await _bookService.GetBookListByAuthor(author);
            if (result.Success)
            {
                _logger.LogInformation($"The author ({author}) of books  has been listed.");
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("random")]
        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> GetRandomBook()
        {
            var token = this.Request.Headers.GetCommaSeparatedValues("Authorization").First().Split(" ")[1];
            var result = await _bookService.GetRandomBook(token);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}

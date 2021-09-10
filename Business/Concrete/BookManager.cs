using Business.Abstract;
using Business.Constants;
using Business.Utilities.Abstract;
using Business.Validation;
using Core.Middlewares.ErrorHandling;
using Core.Utilities.Pagtination;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BookManager : IBookService
    {
        private IEntityValidator _validator;
        private IReadBookDal _readBookDal;
        private IBookDal _bookDal;
        private IParser _jwtParser;
        public BookManager(IBookDal bookDal, IEntityValidator validator, IParser jwtParser, IReadBookDal readBookDal)
        {
            _bookDal = bookDal;
            _validator = validator;
            _jwtParser = jwtParser;
            _readBookDal = readBookDal;
        }
        public async Task<IResult> AddBook(Book book)
        {
            _validator.Validate(book);
            await _bookDal.add(book);
            return new SuccessResult(Messages.bookAdded);
        }

        public async Task<IResult> DeleteBook(Book book)
        {
            _validator.Validate(book);
            await _bookDal.delete(book);
            return new SuccessResult(Messages.bookDeleted);
        }

        public async Task<IDataResult<Book>> GetBookById(int id)
        {
            Book book = await _bookDal.Get(book => book.Id == id);
            return new SuccessDataResult<Book>(book);
        }

        public async Task<IDataResult<List<Book>>> GetBookList(PaginationFilter paginationFilter)
        {
            List<Book> books =await _bookDal.GetBooksWithPagination(paginationFilter);
            return new SuccessDataResult<List<Book>>(books);
        }

        public async Task<IDataResult<List<Book>>> GetBookListByAuthor(string author)
        {
            List<Book> books = await _bookDal.Getlist(book => book.BookAuthor == author);
            return new SuccessDataResult<List<Book>>(books);
        }

        public async Task<IDataResult<Book>> GetRandomBook(string token)
        {
            Book book = new Book();
            User user = await _jwtParser.ParseJwtToUser(token);
            List<ReadBook> userReadBooks = await _readBookDal.Getlist(b => b.UserId == user.Id);
            do
            {
                book = await _bookDal.GetRandomBook();
                if(book == null)
                {
                    throw new BusinessException("Internal server error.");
                }

            } while (!this.CheckBookInUsersReadList(book.Id, userReadBooks));
            
            return new SuccessDataResult<Book>(book);
        }

        public async Task<IResult> UpdateBook(Book book)
        {
            _validator.Validate(book);
            await _bookDal.update(book);
            return new SuccessResult(Messages.bookUpdated);
        }

        private bool CheckBookInUsersReadList(int BookId, List<ReadBook> readBooks)
        {
            foreach (var item in readBooks)
            {
                if(item.BookId == BookId)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

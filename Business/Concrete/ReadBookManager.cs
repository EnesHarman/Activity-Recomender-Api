using Business.Abstract;
using Business.Constants;
using Business.Utilities.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ReadBookManager:IReadBookService
    {
        private IReadBookDal _readBookDal;
        private IParser _jwtParser;
        private IBookDal _bookDal;

        public ReadBookManager(IReadBookDal readBookDal, IParser jwtParser,  IBookDal bookDal)
        {
            _readBookDal = readBookDal;
            _jwtParser = jwtParser;
            _bookDal = bookDal;
        }

        public async Task<IResult> AddBookToReadList(int bookId, string token)
        {
            User user = await _jwtParser.ParseJwtToUser(token);

            ReadBook readBook = new ReadBook { BookId = bookId, UserId = user.Id };

            List<ReadBook> readBooks = await _readBookDal.Getlist(b => b.UserId == user.Id);

            foreach(var item in readBooks)
            {
                if(item.BookId == readBook.BookId)
                {

                return new ErrorResult(Messages.readBookAlreadyAdded);
                }
            }

            await _readBookDal.add(readBook);
            return new SuccessResult(Messages.readBookAdded);
        }

        public async Task<IResult> DeleteBookToReadList(int readBookId, string token, int bookId)
        {
            User user = await _jwtParser.ParseJwtToUser(token);
            ReadBook readBook = new ReadBook { Id = readBookId, BookId = bookId, UserId = user.Id };
            await _readBookDal.delete(readBook);
            return new SuccessResult(Messages.readBookDeleted);
        }

        public async Task<IDataResult<List<ReadBookDto>>> ListReadBookList(string token)
        {
            List<ReadBookDto> books = new List<ReadBookDto>();
            User user = await _jwtParser.ParseJwtToUser(token);
            List<ReadBook> readBooks =await _readBookDal.Getlist(b => b.UserId == user.Id);
            
            if(readBooks.Count == 0)
            {
                return new ErrorDataResult<List<ReadBookDto>>(Messages.readBookListEmpty);
            }
            foreach (var item in readBooks)
            {
                books.Add(new ReadBookDto {ReadBookId = item.Id, Book = await _bookDal.Get(b => b.Id == item.BookId)});
            }
            return new SuccessDataResult<List<ReadBookDto>>(books);
        }
    }
}

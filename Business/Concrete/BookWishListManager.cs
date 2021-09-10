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
    public class BookWishListManager : IBookWishListService
    {
        private IBookWishListDal _BookWishListDal;
        private IParser _parser;
        private IBookDal _bookDal;

        public BookWishListManager(IBookWishListDal bookWishListDal, IParser parser, IBookDal bookDal)
        {
            _BookWishListDal = bookWishListDal;
            _parser = parser;
            _bookDal = bookDal;
        }

        public async Task<IResult> AddBookToWishList(int bookId, string token)
        {
            List<BooksWishList> booksWishLists = new List<BooksWishList>();
            User user = await _parser.ParseJwtToUser(token);
            BooksWishList booksWishListItem = new BooksWishList { BookId = bookId, UserId = user.Id };

            booksWishLists = await _BookWishListDal.Getlist(b => b.UserId == user.Id);

            foreach (var item in booksWishLists)
            {
                if(item.BookId == booksWishListItem.BookId)
                {
                    return new ErrorResult(Messages.bookWishAlreadyAdded);
                }
            }
            await _BookWishListDal.add(booksWishListItem);
            return new SuccessResult(Messages.bookWishAdded);
        }

        public async Task<IResult> DeleteBookFromWishList(int wishBookId, string token, int bookId)
        {
            User user = await _parser.ParseJwtToUser(token);
            BooksWishList booksWishListItem = new BooksWishList {Id = wishBookId, BookId = bookId, UserId = user.Id };
            await _BookWishListDal.delete(booksWishListItem);
            return new SuccessResult(Messages.bookWishDeleted);
        }

        public async Task<IDataResult<List<BookWishListDto>>> ListWishBookList(string token)
        {
            User user = await _parser.ParseJwtToUser(token);
            List<BooksWishList> booksWishLists = await _BookWishListDal.Getlist(b => b.UserId == user.Id);
            List<BookWishListDto> books = new List<BookWishListDto>();

            if (booksWishLists.Count == 0)
            {
                return new ErrorDataResult<List<BookWishListDto>>(Messages.bookWishListEmpty);
            }

            foreach (var item in booksWishLists)
            {
                books.Add(new BookWishListDto { WishBookId = item.Id, Book = await _bookDal.Get(b => b.Id == item.BookId)});
            }
            return new SuccessDataResult<List<BookWishListDto>>(books);
        }
    }
}

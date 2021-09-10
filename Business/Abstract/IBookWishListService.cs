using Core.Utilities.Result.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBookWishListService
    {
        Task<IResult> AddBookToWishList(int bookId, string token);
        Task<IResult> DeleteBookFromWishList(int wishBookId, string token, int bookId);
        Task<IDataResult<List<BookWishListDto>>> ListWishBookList(string token);
    }
}

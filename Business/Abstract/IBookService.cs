using Core.Utilities.Pagtination;
using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBookService
    {
        Task<IResult> AddBook(Book book);
        Task<IResult> DeleteBook(Book book);
        Task<IResult> UpdateBook(Book book);
        Task<IDataResult<Book>> GetBookById(int id);
        Task<IDataResult<List<Book>>> GetBookList(PaginationFilter paginationFilter);
        Task<IDataResult<List<Book>>> GetBookListByAuthor(string author);
        Task<IDataResult<Book>> GetRandomBook(string token);

    }
}

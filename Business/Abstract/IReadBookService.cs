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
    public interface IReadBookService
    {
        Task<IResult> AddBookToReadList(int bookId, string token);
        Task<IResult> DeleteBookToReadList(int readBookId, string token, int bookId);
        Task<IDataResult<List<ReadBookDto>>> ListReadBookList(string token);
    }
}

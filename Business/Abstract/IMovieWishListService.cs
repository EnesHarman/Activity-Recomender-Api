using Core.Utilities.Result.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMovieWishListService
    {
        Task<IResult> AddMovieToWishList(int filmId, string token);
        Task<IResult> DeleteMovieFromWishList(int wishedMovieId, string token, int movieId);
        Task<IDataResult<List<MovieWishListDto>>> ListWishedMovie(string token);
    }
}

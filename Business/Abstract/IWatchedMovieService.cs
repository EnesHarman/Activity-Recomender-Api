using Core.Utilities.Result.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IWatchedMovieService
    {
        Task<IResult> AddMovieToWatchedList(int filmId, string token);
        Task<IResult> DeleteMovieFromWatchedList(int watchedMovieId, string token, int movieId);
        Task<IDataResult<List<WatchedMovieDto>>> ListWatchedMovieDto(string token);
    }
}

using Business.Abstract;
using Business.Constants;
using Business.Utilities.Abstract;
using Core.Middlewares.ErrorHandling;
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
    public class WatchedMovieManager : IWatchedMovieService
    {
        private IWatchedMoviesDal _watchedMoviesDal;
        private IParser parser;
        private IMovieDal _movieDal;

        public WatchedMovieManager(IWatchedMoviesDal watchedMoviesDal, IParser parser, IMovieDal movieDal)
        {
            _watchedMoviesDal = watchedMoviesDal;
            this.parser = parser;
            _movieDal = movieDal;
        }

        public async Task<IResult> AddMovieToWatchedList(int filmId, string token)
        {
            User user = await parser.ParseJwtToUser(token);
            List<WatchedMovie> watchedMovies = await _watchedMoviesDal.Getlist(m => m.UserId == user.Id);

            foreach (var item in watchedMovies)
            {
                if(item.MovieId == filmId)
                {
                    return new ErrorResult(Messages.watchedMovieAlreadyAdded);
                }
            }
            WatchedMovie watchedMovie = new WatchedMovie { MovieId = filmId, UserId = user.Id };
            await _watchedMoviesDal.add(watchedMovie);
            return new SuccessResult(Messages.watchedMovieAdded);
            
        }

        public async Task<IResult> DeleteMovieFromWatchedList(int watchedMovieId, string token, int movieId)
        {
            User user = await parser.ParseJwtToUser(token);
            WatchedMovie watchedMovie = new WatchedMovie { Id = watchedMovieId, MovieId = movieId, UserId = user.Id };
            await _watchedMoviesDal.delete(watchedMovie);
            return new SuccessResult(Messages.watchedMovieDeleted);
        }

        public async Task<IDataResult<List<WatchedMovieDto>>> ListWatchedMovieDto(string token)
        {
            User user = await parser.ParseJwtToUser(token);
            List<WatchedMovieDto> movieDtos = new List<WatchedMovieDto>();
            List<WatchedMovie> watchedMovies = await _watchedMoviesDal.Getlist(m => m.UserId == user.Id);

            if(watchedMovies.Count == 0)
            {
                return new ErrorDataResult<List<WatchedMovieDto>>(Messages.watchedMovieListEmpty);
            }

            foreach (var item in watchedMovies)
            {
                movieDtos.Add(new WatchedMovieDto { Movie = await _movieDal.Get(m => m.Id == item.MovieId) , WatchedMovieId = item.Id });
            }

            return new SuccessDataResult<List<WatchedMovieDto>>(movieDtos);
        }
    }
}

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
    public class MovieWishListManager : IMovieWishListService
    {
        private IMoviesWishListDal _moviesWishListDal;
        private IParser parser;
        private IMovieDal _movieDal;

        public MovieWishListManager(IParser parser, IMovieDal movieDal, IMoviesWishListDal moviesWishListDal)
        {
            this.parser = parser;
            _movieDal = movieDal;
            _moviesWishListDal = moviesWishListDal;
        }

        public async Task<IResult> AddMovieToWishList(int filmId, string token)
        {
            User user =await parser.ParseJwtToUser(token);
            List<MoviesWishList> moviesWishLists = await _moviesWishListDal.Getlist(m => m.UserId == user.Id);

            foreach (var item in moviesWishLists)
            {
                if(item.MovieId == filmId)
                {
                    return new ErrorResult(Messages.watchedMovieAlreadyAdded);
                }
            }

            MoviesWishList moviesWishList = new MoviesWishList { MovieId = filmId, UserId = user.Id };
            await _moviesWishListDal.add(moviesWishList);
            return new SuccessResult(Messages.watchedMovieAdded);
        }

        public async Task<IResult> DeleteMovieFromWishList(int wishedMovieId, string token, int movieId)
        {
            User user = await parser.ParseJwtToUser(token);
            MoviesWishList moviesWishList = new MoviesWishList { Id = wishedMovieId, MovieId = movieId, UserId = user.Id };
            await _moviesWishListDal.delete(moviesWishList);
            return new SuccessResult(Messages.watchedMovieDeleted);
        }

        public async Task<IDataResult<List<MovieWishListDto>>> ListWishedMovie(string token)
        {
            User user = await parser.ParseJwtToUser(token);
            List<MovieWishListDto> movieDtos = new List<MovieWishListDto>();
            List<MoviesWishList> wishedMovie = await _moviesWishListDal.Getlist(m => m.UserId == user.Id);

            if (wishedMovie.Count == 0)
            {
                return new ErrorDataResult<List<MovieWishListDto>>(Messages.watchedMovieListEmpty);
            }

            foreach (var item in wishedMovie)
            {
                movieDtos.Add(new MovieWishListDto { Movie = await _movieDal.Get(m => m.Id == item.MovieId), MoviesWishId = item.Id });
            }

            return new SuccessDataResult<List<MovieWishListDto>>(movieDtos);
        }
    }
}

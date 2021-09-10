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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MovieManager : IMovieService
    {
        private readonly IMovieDal _movieDal;
        private IEntityValidator _validator;
        private IWatchedMoviesDal _watchedMoviesDal;
        private IParser _jwtParser;

        public MovieManager(IMovieDal movieDal, IEntityValidator validator, IParser jwtParser, IWatchedMoviesDal watchedMoviesDal)
        {
            _movieDal = movieDal;
            _validator = validator;
            _jwtParser = jwtParser;
            _watchedMoviesDal = watchedMoviesDal;
        }

        public async Task<IResult> add(Movie movie)
        {
            _validator.Validate(movie);
            await _movieDal.add(movie);
            return new SuccessResult(Messages.movieAdded);
        }

        public async Task<IResult> delete(Movie movie)
        {
            _validator.Validate(movie);
            await _movieDal.delete(movie);
            return new SuccessResult(Messages.movieDeleted);
        }

        public async Task<IDataResult<Movie>> getMovieById(int id)
        {
            Movie movie;
            movie = await _movieDal.Get(mov => mov.Id == id);
            return new SuccessDataResult<Movie>(movie);
        }

        public async Task<IDataResult<List<Movie>>> getMovieByName(string name)
        {
            List<Movie> movies = new List<Movie>();
            movies = await _movieDal.Getlist(mov => mov.Series_Title == name);
            return new SuccessDataResult<List<Movie>>(movies);
        }

        public async Task<IDataResult<List<Movie>>> getMovieList(PaginationFilter paginationFilter)
        {
            List<Movie> movies = new List<Movie>();
             movies = await _movieDal.getMoviesWithPagination(paginationFilter);
            //movies = await _movieDal.Getlist();
            return new SuccessDataResult<List<Movie>>(movies);
        }

        public async Task<IDataResult<List<Movie>>> getMovieListByArtist(string artist)
        {
            List<Movie> movies = new List<Movie>();
            movies = await _movieDal.Getlist(mov => mov.Star1 == artist || mov.Star2 == artist || mov.Star3 == artist || mov.Star4 == artist);
            return new SuccessDataResult<List<Movie>>(movies);
        }

        public async Task<IDataResult<List<Movie>>> getMovieListByDirector(string director)
        {
            List<Movie> movies = new List<Movie>();
            movies = await _movieDal.Getlist(mov => mov.Director == director);
            return new SuccessDataResult<List<Movie>>(movies);
        }

        public async Task<IDataResult<Movie>> getRandomMovie(string token)
        {
            Movie movie = new Movie();
            User user = await _jwtParser.ParseJwtToUser(token);
            List<WatchedMovie> watchedMovies = await _watchedMoviesDal.Getlist(m => m.UserId == user.Id);
            do
            {
                movie = await _movieDal.GetRandomMovie();
                if(movie == null)
                {
                    throw new BusinessException("Internal server error.");
                }

            } while (!CheckMovieUserWatchedList(movie.Id,watchedMovies));
            return new SuccessDataResult<Movie>(movie);
        }

        public async Task<IResult> update(Movie movie)
        {
            _validator.Validate(movie);
            await _movieDal.update(movie);
            return new SuccessResult(Messages.movieUpdated);
        }

        private bool CheckMovieUserWatchedList(int filmId, List<WatchedMovie> watchedMovies)
        {
            foreach (var item in watchedMovies)
            {
                if(filmId == item.MovieId)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

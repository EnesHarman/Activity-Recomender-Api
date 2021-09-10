using Core.Utilities.Pagtination;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMovieService
    {
        Task< IResult> add(Movie movie);
        Task<IResult> update(Movie movie);
        Task<IResult> delete(Movie movie);
        Task<IDataResult<Movie>> getMovieById(int id);
        Task<IDataResult<List<Movie>>> getMovieList(PaginationFilter paginationFilter);
        Task<IDataResult<List<Movie>>> getMovieListByArtist(string artist);
        Task<IDataResult<List<Movie>>> getMovieListByDirector(string director);
        Task<IDataResult<List<Movie>>> getMovieByName(string name);
        Task<IDataResult<Movie>> getRandomMovie(string token);
    }
}

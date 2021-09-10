using Core.Utilities.Pagtination;
using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISongService
    {
        Task<IDataResult<Song>> getSongById(int id);
        Task<IResult> addSong(Song song);
        Task<IResult> updateSong(Song song);
        Task<IResult> deleteSong(Song song);
        Task<IDataResult<List<Song>>> listSongByArtist(string artist);
        Task<IDataResult<List<Song>>> listSongs(PaginationFilter paginationFilter);
        Task<IDataResult<Song>> getRandomSong(string token);

    }
}

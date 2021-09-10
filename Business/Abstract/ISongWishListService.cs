using Core.Utilities.Result.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISongWishListService
    {
        Task<IResult> AddSongToWishList(int songId, string token);
        Task<IResult> DeleteSongFromWishList(int wishedSongId, string token, int songId);
        Task<IDataResult<List<SongWishListDto>>> ListWishedSongs(string token);
    }
}

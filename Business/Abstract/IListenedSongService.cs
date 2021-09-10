using Core.Utilities.Result.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IListenedSongService
    {
        Task<IResult> AddSongToListenedList(int songId, string token);
        Task<IResult> DeleteSongFromListenedList(int listenedSongId, string token, int songId);
        Task<IDataResult<List<ListenedSongDto>>> ListListenedSongs(string token);
    }
}

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
    public class ListenedSongManager : IListenedSongService
    {
        private ISongDal _songDal;
        private IParser _parser;
        private IListenedSongDal _listenedSongDal;
        
        public ListenedSongManager(ISongDal songDal, IParser parser, IListenedSongDal listenedSongDal)
        {
            _songDal = songDal;
            _parser = parser;
            _listenedSongDal = listenedSongDal;
        }

       
        public async Task<IResult> AddSongToListenedList(int songId, string token)
        {
            User user = await _parser.ParseJwtToUser(token);
            List<ListenedSong> listenedSongs = await _listenedSongDal.Getlist(s => s.UserId == user.Id);
            foreach (var item in listenedSongs)
            {
                if(item.SongId == songId)
                {
                    return new ErrorResult(Messages.listenedSongAlreadyExist);
                }
            }
            ListenedSong listenedSong = new ListenedSong { SongId = songId, UserId = user.Id };
            await _listenedSongDal.add(listenedSong);
            return new SuccessResult(Messages.listenedSongAdded);
        }

        public async Task<IResult> DeleteSongFromListenedList(int listenedSongId, string token, int songId)
        {
            User user = await _parser.ParseJwtToUser(token);
            ListenedSong listenedSong = new ListenedSong { Id = listenedSongId, SongId = songId, UserId = user.Id };
            await _listenedSongDal.delete(listenedSong);
            return new SuccessResult(Messages.listenedSongDeleted);
        }

        public async Task<IDataResult<List<ListenedSongDto>>> ListListenedSongs(string token)
        {
            User user = await _parser.ParseJwtToUser(token);
            List<ListenedSongDto> listenedSongs = new List<ListenedSongDto>();
            List<ListenedSong> songs = await _listenedSongDal.Getlist(s => s.UserId == user.Id);
            foreach (var item in songs)
            {
                listenedSongs.Add(new ListenedSongDto { ListenedSongId = item.Id, Song = await _songDal.Get(s => s.Id == item.SongId)});
            }
            return new SuccessDataResult<List<ListenedSongDto>>(listenedSongs); 
        }
    }
}

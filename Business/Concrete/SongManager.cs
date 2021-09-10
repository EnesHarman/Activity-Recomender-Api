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
    public class SongManager : ISongService

    {

        private ISongDal _songDal;
        private readonly IEntityValidator _validator;
        private IParser _jwtParser;
        private IListenedSongDal _listenedSongDal;

        public SongManager(ISongDal songDal, IEntityValidator validator, IParser jwtParser, IListenedSongDal listenedSongDal)
        {
            _songDal = songDal;
            _validator = validator;
            _jwtParser = jwtParser;
            _listenedSongDal = listenedSongDal;
        }

        public async Task<IResult> addSong(Song song)
        {
            _validator.Validate(song);
            int id = await _songDal.add(song);
            return new SuccessResult(Messages.songAdded);
        }

        public async Task<IResult> deleteSong(Song song)
        {
            _validator.Validate(song);
            await _songDal.delete(song);
            return new SuccessResult(Messages.songDeleted);

        }

        public async Task<IDataResult<Song>> getSongById(int id)
        {
            Song song =await _songDal.Get(song => song.Id == id);
            return new SuccessDataResult<Song>(song);
        }

        public async Task<IDataResult<List<Song>>> listSongByArtist(string artist)
        {
            List<Song> songs = await _songDal.Getlist(song => song.artist_name == artist);
            return new SuccessDataResult<List<Song>>(songs);
        }

        public async Task<IDataResult<List<Song>>> listSongs(PaginationFilter paginationFilter)
        {
            List<Song> songs = await _songDal.GetListWithPagination(paginationFilter);
            return new SuccessDataResult<List<Song>>(songs);

        }

        public async Task<IResult> updateSong(Song song)
        {
            _validator.Validate(song);
            await _songDal.update(song);
            return new SuccessResult(Messages.songUpdated);
        }

        public async Task<IDataResult<Song>> getRandomSong(string token)
        {
            Song song = new Song();
            User user = await _jwtParser.ParseJwtToUser(token);
            List<ListenedSong> listenedSongs = await _listenedSongDal.Getlist(s => s.UserId == user.Id);
            do
            {
                song = await _songDal.GetRandomSong();
                if(song == null)
                {
                    throw new BusinessException("Internal server error.");
                }
            } while (!CheckSongInListenedList(song.Id,listenedSongs));
            return new SuccessDataResult<Song>(song);

        }
        private bool CheckSongInListenedList(int songId, List<ListenedSong> listenedSong)
        {
            foreach (var item in listenedSong)
            {
                if(item.SongId == songId)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

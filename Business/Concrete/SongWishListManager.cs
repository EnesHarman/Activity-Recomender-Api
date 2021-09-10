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
    public class SongWishListManager : ISongWishListService
    {
        private ISongDal _songDal;
        private IParser _parser;
        private ISongWishListDal _songWishListDal;
        public SongWishListManager(ISongDal songDal, IParser parser, ISongWishListDal songWishListDal)
        {
            _songDal = songDal;
            _parser = parser;
            _songWishListDal = songWishListDal;
        }

        public async Task<IResult> AddSongToWishList(int songId, string token)
        {
            User user = await _parser.ParseJwtToUser(token);
            List<SongWishList> wishedSong = await _songWishListDal.Getlist(s => s.UserId == user.Id);
            foreach (var item in wishedSong)
            {
                if (item.SongId == songId)
                {
                    return new ErrorResult(Messages.listenedSongAlreadyExist);
                }
            }
            SongWishList songWishList = new SongWishList { SongId = songId, UserId = user.Id };
            await _songWishListDal.add(songWishList);
            return new SuccessResult(Messages.listenedSongAdded);
        }

        public async Task<IResult> DeleteSongFromWishList(int wishedSongId, string token, int songId)
        {
            User user = await _parser.ParseJwtToUser(token);
            SongWishList wishedSong = new SongWishList { Id = wishedSongId, SongId = songId, UserId = user.Id };
            await _songWishListDal.delete(wishedSong);
            return new SuccessResult(Messages.listenedSongDeleted);
        }

        public async Task<IDataResult<List<SongWishListDto>>> ListWishedSongs(string token)
        {
            User user = await _parser.ParseJwtToUser(token);
            List<SongWishListDto> wishedSongs = new List<SongWishListDto>();
            List<SongWishList> songs = await _songWishListDal.Getlist(s => s.UserId == user.Id);
            foreach (var item in songs)
            {
                wishedSongs.Add(new SongWishListDto { SongWishListId = item.Id, Song = await _songDal.Get(s => s.Id == item.SongId) });
            }
            return new SuccessDataResult<List<SongWishListDto>>(wishedSongs);
        }
    }
}

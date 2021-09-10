using Autofac;
using Business.Abstract;
using Business.Adapters;
using Business.Concrete;
using Business.Utilities.Abstract;
using Business.Utilities.Jwt;
using Business.Validation;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DepedencyResolver.Autofac
{
    public class AutofacBusinessModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfSongDal>().As<ISongDal>();
            builder.RegisterType<SongManager>().As<ISongService>();

            builder.RegisterType<EfListenedSongDal>().As<IListenedSongDal>();
            builder.RegisterType<ListenedSongManager>().As<IListenedSongService>();

            builder.RegisterType<EfSongWishListDal>().As<ISongWishListDal>();
            builder.RegisterType<SongWishListManager>().As<ISongWishListService>();

            builder.RegisterType<BookManager>().As<IBookService>();
            builder.RegisterType<EfBookDal>().As<IBookDal>();

            builder.RegisterType<EfMovieDal>().As<IMovieDal>();
            builder.RegisterType<MovieManager>().As<IMovieService>();

            builder.RegisterType<EfWatchedMovieDal>().As<IWatchedMoviesDal>();
            builder.RegisterType<WatchedMovieManager>().As<IWatchedMovieService>();

            builder.RegisterType<EfMovieWishListDal>().As<IMoviesWishListDal>();
            builder.RegisterType<MovieWishListManager>().As<IMovieWishListService>();

            builder.RegisterType<MernisPersonCheckAdapter>().As<IPersonCheckSystem>();

            builder.RegisterType<AuthManager>().As<IAuthService>();

            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<UserManager>().As<IUserService>();

            builder.RegisterType<EfUserRoleDal>().As<IUserRoleDal>();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>();

            builder.RegisterType<EntityValidator>().As<IEntityValidator>();

            builder.RegisterType<EfManagerDal>().As<IManagerDal>();

            builder.RegisterType<EfReadBookDal>().As<IReadBookDal>();
            builder.RegisterType<ReadBookManager>().As<IReadBookService>();

            builder.RegisterType<EfBookWishListDal>().As<IBookWishListDal>();
            builder.RegisterType<BookWishListManager>().As<IBookWishListService>();

            builder.RegisterType<JwtParser>().As<IParser>();
        }
    }
}

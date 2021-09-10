using Core.Utilities.Configuration;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Context
{
    public class ActivityRcmDbContext : DbContext
    {

        protected IConfiguration _configuration;

        public ActivityRcmDbContext()
        {
            _configuration = ConfigurationResolver.GetConfiguration();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BooksWishList> BooksWishLists { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ListenedSong> ListenedSongs { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MoviesWishList> MoviesWishLists { get; set; }
        public DbSet<ReadBook> ReadBooks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongWishList> SongWishLists { get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<WatchedMovie> WatchedMovies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ActivityRecomenderDB"));
        }

    }
}

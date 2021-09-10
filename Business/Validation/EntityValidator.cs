using Business.Adapters;
using Core.Middlewares.ErrorHandling;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Validation
{
    public class EntityValidator : IEntityValidator
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        IPersonCheckSystem _personCheckSystem;

        public EntityValidator(IPersonCheckSystem personCheckSystem)
        {
            _personCheckSystem = personCheckSystem;
        }

        public void Validate(Book book)
        {
            if(book.ISBN == null)
            {
                throw new BusinessException("The ISBN value can not be empty.");
            }
            else if(book.BookTitle == null)
            {
                throw new BusinessException("The Title value can not be empty.");
            }
            else if (book.BookAuthor==null)
            {
                throw new BusinessException("The Author value can not be empty.");
            }
            else if(book.YearOfPublication == null)
            {
                throw new BusinessException("The Year value can not be empty.");
            }

            else
            {
                return;
            }
        }

        public void Validate(Song song)
        {
            if(song.song_name == null)
            {
                throw new BusinessException("The Name value can not be empty.");
            }
            else if(song.artist_name == null)
            {
                throw new BusinessException("The Artist value can not be empty.");
            }
            else if(song.album_names == null)
            {
                throw new BusinessException("The Album value can not be empty.");
            }
            else
            {
                return;
            }
        }

        public void Validate(Movie movie)
        {
            if(movie.Poster_Link == null)
            {
                throw new BusinessException("The Poster Link can not be empty.");
            }
            else if (movie.Series_Title == null)
            {
                throw new BusinessException("The Series Title can not be empty.");
            }
            else if (movie.Released_Year == null)
            {
                throw new BusinessException("The Relased Year can not be empty.");
            }
            else if (movie.Runtime == null)
            {
                throw new BusinessException("The Runtime can not be empty.");
            }
            else if (movie.Genre == null)
            {
                throw new BusinessException("The Genre can not be empty.");
            }
            else if (movie.IMDB_Rating == 0.0M)
            {
                throw new BusinessException("The IMDB Rating can not be empty.");
            }
            else if (movie.Overview == null)
            {
                throw new BusinessException("The Overview can not be empty.");
            }
            else if (movie.Director == null)
            {
                throw new BusinessException("The Director can not be empty.");
            }
            else if (movie.Star1 == null)
            {
                throw new BusinessException("The Star 1 can not be empty.");
            }
            else if (movie.Star2 == null)
            {
                throw new BusinessException("The Star 2 can not be empty.");
            }
            else if (movie.Star3 == null)
            {
                throw new BusinessException("The Star 3 can not be empty.");
            }
            else if (movie.Star4 == null)
            {
                throw new BusinessException("The Star 4 can not be empty.");
            }
            else 
            {
                return;

            }

        }
        public void Validate(CustomerRegisterDto customerRegisterDto) 
        {
            if (!regex.IsMatch(customerRegisterDto.Email.Trim()))
            {
                throw new Exception("Please use a valid Gmail to register.");
            }
            else if (customerRegisterDto.Password.Length  <5) 
            {
                throw new Exception("Please use a password with min-length 6.");
            }
            else if (customerRegisterDto.FirstName.Length < 2)
            {
                throw new Exception("Please use a name with min-length 3");
            }
            else if (customerRegisterDto.FirstName.Any(c => char.IsDigit(c)))
            {
                throw new Exception("First name can not contain number type value.");
            }
            else if (customerRegisterDto.LastName.Length < 2)
            {
                throw new Exception("Please use a last name with min-length 3");
            }
            else if (customerRegisterDto.LastName.Any(c => char.IsDigit(c)))
            {
                throw new Exception("Last name can not contain number type value.");
            }
            else
            {
                return;
            }

        }

        public async  Task Validate(ManagerRegisterDto managerRegisterDto)
        {

            if (!regex.IsMatch(managerRegisterDto.Email.Trim()))
            {
                throw new Exception("Please use a valid Gmail to register.");
            }
            else if (managerRegisterDto.Password.Length < 5)
            {
                throw new Exception("Please use a password with min-length 6.");
            }
            else if (managerRegisterDto.FirstName.Length < 2)
            {
                throw new Exception("Please use a name with min-length 3");
            }
            else if (managerRegisterDto.FirstName.Any(c => char.IsDigit(c)))
            {
                throw new Exception("First name can not contain number type value.");
            }
            else if (managerRegisterDto.LastName.Length < 2)
            {
                throw new Exception("Please use a last name with min-length 3");
            }
            else if (managerRegisterDto.LastName.Any(c => char.IsDigit(c)))
            {
                throw new Exception("Last name can not contain number type value.");
            }
            else if(managerRegisterDto.NationalIdentity.Length != 11)
            {
                throw new Exception("Identity number must be 11 digit.");
            }
            else if(!await _personCheckSystem.checkPersonReal(managerRegisterDto.NationalIdentity, managerRegisterDto.FirstName,
                managerRegisterDto.LastName, managerRegisterDto.BirhDate))
            {
                throw new Exception("Managers must register with their real info.");
            }
            else
            {
                return ;
            }

        }

    }
}

using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public interface IEntityValidator
    { 
         void Validate(Book book);
         void Validate(Song book);
         void Validate(Movie movie);
        void Validate(CustomerRegisterDto customerRegisterDto);
        Task Validate(ManagerRegisterDto managerRegisterDto);

    }
}

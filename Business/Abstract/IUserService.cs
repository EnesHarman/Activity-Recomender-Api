using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<User>> GetByMail(string mail);
        IDataResult<Role> GetRole(User user);
        Task<IResult> Add(CustomerRegisterDto customerRegisterDto);
        Task<IResult> Add(ManagerRegisterDto managerRegisterDto);
    }
}

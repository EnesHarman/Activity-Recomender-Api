using Core.Utilities.Result.Abstract;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<User>> Login(UserLoginDto userLoginDto);

        Task<IResult> Register(CustomerRegisterDto customerRegisterDto);
        Task<IResult> Register(ManagerRegisterDto managerRegisterDto);
        Task<IDataResult<AccessToken>> CreateAccessToken(User user);
    }
}

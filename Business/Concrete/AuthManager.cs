using Business.Abstract;
using Business.Constants;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private  IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(ITokenHelper tokenHelper, IUserService userService)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
        }


        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var result = _userService.GetRole(user);
            var token = _tokenHelper.CreateToken(user, result.Data);
            return new SuccessDataResult<AccessToken>(token);
        }

        public async Task<IDataResult<User>> Login(UserLoginDto userLoginDto)
        {
            var result = await _userService.GetByMail(userLoginDto.Email);
            if (!result.Success)
            {
                return new ErrorDataResult<User>(Messages.userNotExist);
            }
            bool passwordCheck = HashingHelper.VerifyPasswordHass(userLoginDto.Password, result.Data.PasswordHash, result.Data.PasswordSalt);
            if (!passwordCheck)
            {
                return new ErrorDataResult<User>(Messages.userPasswordError);
            }

            return new SuccessDataResult<User>(result.Data);
        }

        public async Task<IResult> Register(CustomerRegisterDto customerRegisterDto)
        {
            if(customerRegisterDto.Password != customerRegisterDto.PasswordForCheck)
            {
                return new ErrorResult(Messages.userValidatePassword);
            }

            var result = await _userService.Add(customerRegisterDto);
            return result;
        }

        public async Task<IResult> Register(ManagerRegisterDto managerRegisterDto)
        {
            if (managerRegisterDto.Password != managerRegisterDto.PasswordForCheck)
            {
                return new ErrorResult(Messages.userValidatePassword);
            }

            var result = await _userService.Add(managerRegisterDto);
            return result;
        }
    }
}

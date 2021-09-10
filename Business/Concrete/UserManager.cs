using Business.Abstract;
using Business.Constants;
using Core.Utilities.Hashing;
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
    public class UserManager : IUserService
    {
        private  IUserDal _userDal;
        private  ICustomerDal _customerDal;
        private  IUserRoleDal _userRoleDal;
        private IManagerDal _managerDal;

        public UserManager(IUserDal userDal, ICustomerDal customerDal, IUserRoleDal userRoleDal, IManagerDal managerDal)
        {
            _userDal = userDal;
            _customerDal = customerDal;
            _userRoleDal = userRoleDal;
            _managerDal = managerDal;
        }

        public async Task<IResult> Add(CustomerRegisterDto customerRegisterDto)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            HashingHelper.CreatePasswordHash(customerRegisterDto.Password, out passwordHash, out passwordSalt);

            User user = new User { 
                FirstName = customerRegisterDto.FirstName,
                LastName = customerRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = customerRegisterDto.Email
            };

            int userId = await _userDal.add(user);
            await _customerDal.add(new Customer { Status = true, UserId = userId });
            await _userRoleDal.add(new UserRole { RoleId = 2, UserId = userId });

            return new SuccessResult(Messages.userAdded);
        }

        public async Task<IResult> Add(ManagerRegisterDto managerRegisterDto)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            HashingHelper.CreatePasswordHash(managerRegisterDto.Password, out passwordHash, out passwordSalt);

            User user = new User
            {
                FirstName = managerRegisterDto.FirstName,
                LastName = managerRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = managerRegisterDto.Email,
                BirthDate = managerRegisterDto.BirhDate
            };

            int userId = await _userDal.add(user);
            await _managerDal.add(new Manager { UserId = userId ,NationalIdentity = managerRegisterDto.NationalIdentity});
            await _userRoleDal.add(new UserRole { RoleId = 1, UserId = userId });

            return new SuccessResult(Messages.userAdded);
        }

        public async Task<IDataResult<User>> GetByMail(string mail)
        {
            User user= await _userDal.Get(u => u.Email == mail);
            if(user!= null)
            {
                return new SuccessDataResult<User>(user);
            }
            return new ErrorDataResult<User>();
        }

        public IDataResult<Role> GetRole(User user)
        {
            var role = _userDal.GetUserRoles(user);
            return new SuccessDataResult<Role>(role);
        }
    }
}

using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, ActivityRcmDbContext>, IUserDal
    {
        public Role GetUserRoles(User user)
        {
            using(var context = new ActivityRcmDbContext())
            {
                var result = from role in context.Roles
                             join userRole in context.UserRoles
                             on role.Id equals userRole.RoleId
                             where userRole.UserId == user.Id
                             select new Role { Id = role.Id, RoleName = role.RoleName };
                Console.WriteLine(result.FirstOrDefault());
                return result.FirstOrDefault();
            }
        }
    }
}

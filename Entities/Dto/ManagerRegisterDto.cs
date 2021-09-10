using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class ManagerRegisterDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordForCheck { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalIdentity { get; set; }
        public int BirhDate { get; set; }
    }
}

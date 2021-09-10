using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Adapters
{
    public interface IPersonCheckSystem
    {
        Task<bool> checkPersonReal(string IDNum, string name, string lastname, int birthDate);
    }
}

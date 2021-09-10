
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Utilities.Abstract
{
    public interface IParser
    {
        Task<User> ParseJwtToUser(string token);
    }
}

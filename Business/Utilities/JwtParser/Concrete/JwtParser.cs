using Business.Abstract;
using Business.Utilities.Abstract;
using Entities.Concrete;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Utilities.Jwt
{
    public class JwtParser : IParser
    {
        JwtSecurityTokenHandler _handler;
        IUserService _userService;
        public JwtParser(IUserService userService)
        {
            _handler = new JwtSecurityTokenHandler();
            _userService = userService;
        }
        public async  Task<User> ParseJwtToUser(string token)
        {
            
            var data = _handler.ReadJwtToken(token);
            string email = data.Payload.ElementAt(1).Value.ToString();
            
            var result = await _userService.GetByMail(email);
            
            return result.Data;
        }
    }
}

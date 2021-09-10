using Business.Abstract;
using Business.Validation;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IEntityValidator _validator;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, IEntityValidator validator, ILogger<AuthController>logger)
        {
            _authService = authService;
            _validator = validator;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CustomerRegisterDto customerRegisterDto)
        {
            _validator.Validate(customerRegisterDto);
            var result = await _authService.Register(customerRegisterDto);
            if (result.Success)
            {
                _logger.LogInformation($"{customerRegisterDto.FirstName} has registered to service as a customer with that email: {customerRegisterDto.Email}.");
                return Ok( result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("manager/register")]
        public async Task<IActionResult> ManagerRegister([FromBody] ManagerRegisterDto managerRegisterDto)
        {
            await _validator.Validate(managerRegisterDto);
            var result = await _authService.Register(managerRegisterDto);
            if (result.Success)
            {
                _logger.LogInformation($"{managerRegisterDto.FirstName} has registered to service as a manager with that email: {managerRegisterDto.Email}.");
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var result = await _authService.Login(userLoginDto);
            var jwt = await _authService.CreateAccessToken(result.Data);
            if (jwt.Success)
            {
                _logger.LogInformation($"{userLoginDto.Email} has logged to service.");
                return Ok(jwt.Data);
            }
            return BadRequest(jwt.Message);
            
        }
    }
}

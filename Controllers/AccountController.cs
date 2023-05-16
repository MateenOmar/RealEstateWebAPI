using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Dtos;
using WebAPI.Errors;
using WebAPI.Extensions;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration configuration;

        public AccountController(IUnitOfWork uow, IConfiguration configuration)
        {
            this.uow = uow;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq){
            var user = await uow.UserRepository.Authenticate(loginReq.username, loginReq.password);

            ApiError apiError = new ApiError();

            if(user == null) {
                apiError.ErrorCode = Unauthorized().StatusCode;
                apiError.ErrorMessage = "Invalid User ID or Password";
                apiError.ErrorDetails = "This error appears when provided user id or password does not exits";
                return Unauthorized(apiError);
            }

            var loginRes = new LoginResDto();
            loginRes.username = user.username;
            loginRes.Token = CreateJWT(user);

            return Ok(loginRes);
        }

        private string CreateJWT(User user) {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name,user.username),
                new Claim(ClaimTypes.NameIdentifier,user.ID.ToString())
            };

            var signingCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq){
            ApiError apiError = new ApiError();

            if(loginReq.username.IsEmpty() || loginReq.password.IsEmpty()) {
                    apiError.ErrorCode=BadRequest().StatusCode;
                    apiError.ErrorMessage="User name or password can not be blank";                    
                    return BadRequest(apiError);
            }  

            if (await uow.UserRepository.UserAlreadyExists(loginReq.username)) {
                apiError.ErrorCode = BadRequest().StatusCode;
                apiError.ErrorMessage = "User already exists.";
                return BadRequest(apiError);
            }
            
            uow.UserRepository.Register(loginReq.username, loginReq.password);
            await uow.SaveAsync();

            return StatusCode(201);
        }
    }
}
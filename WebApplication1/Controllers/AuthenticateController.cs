using Application.Common;
using Domain.ViewModel.User;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUserService _userService;
        private EncryptionUtility _encryptionUtility;
        public AuthenticateController(IConfiguration configuration,
        IUserService userService,
        EncryptionUtility encryptionUtility)
        {
            _configuration = configuration;
            _userService = userService;
            _encryptionUtility = encryptionUtility;
        }
        [HttpPost]
        public async Task<IActionResult> Post(LoginViewModel model)
        {
            //1-check username & password
            var user = await _userService.GetAsync(model.UserName);
            if (user == null) return BadRequest("invalid userName");

            var hashPassword = _encryptionUtility.HashWithSalt(model.Password, Guid.Parse(user.PasswordSalt));
            if (user.Password != hashPassword) return BadRequest("invalid password");

            var token = GenerateNewToken(user.Id);
            var refreshToken = Guid.NewGuid();

            var info = new AuthenticateViewModel
            {
                FullName = $"{user.FirstName} {user.LastName}",
                UserId = user.Id,
                Token = token,
                RefreshToken = refreshToken.ToString()
            };

            var userRefreshToken = await _userService.GetRefreshTokenAsync(user.Id);
            var userToken = new UserRefreshTokenViewModel
            {
                UserId = user.Id,
                RefreshToken = refreshToken.ToString(),
                GenerateDate = DateTime.Now,
                IsValid = true
            };

            if (userRefreshToken == null)
            {
                //insert new refresh token in table
                await _userService.InsertRefreshTokenAsync(userToken);
            }
            else
            {
                //update record in db
                await _userService.UpdateRefreshTokenAsync(userToken);
            }

            return Ok(info);
        }

        [HttpPost("newtoken")]
        public async Task<IActionResult> PostNewToken(string refreshToken)
        {
            var refreshTokenTimeOut = _configuration.GetValue<int>("RefreshTokenTimeOut");

            Guid userId = Guid.Parse(User.Claims.SingleOrDefault(q => q.Type == "userGuid").Value);
            var userRefreshToken = await _userService.GetRefreshTokenAsync(userId);
            if(userRefreshToken == null) return BadRequest("invalid request");
            if(userRefreshToken.RefreshToken != refreshToken) return BadRequest("invalid refresh token");
            if(!userRefreshToken.IsValid) return BadRequest("is invalid refresh token");
            if(userRefreshToken.GenerateDate.AddMinutes(refreshTokenTimeOut) < DateTime.Now) return BadRequest("expire refresh token");
            
            var newToken = GenerateNewToken(userId);
            var newRefreshToken = Guid.NewGuid();

            var userToken = new UserRefreshTokenViewModel
            {
                UserId = userId,
                RefreshToken = refreshToken.ToString(),
                GenerateDate = DateTime.Now,
                IsValid = true
            };

            await _userService.UpdateRefreshTokenAsync(userToken);

            return Ok(new {Token = newToken, RefreshToken = newRefreshToken});

        }

        private string GenerateNewToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenKey"));
            var tokenTimeOut = _configuration.GetValue<int>("TokenTimeOut");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userGuid", userId.ToString()),
                }),

                Expires = DateTime.UtcNow.AddMinutes(tokenTimeOut),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
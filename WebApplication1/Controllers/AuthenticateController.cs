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
        public async Task<IActionResult> Post(UserLoginViewModel model)
        {
            //1-check username & password
            var user = await _userService.GetAsync(model.UserName);
            if (user == null) return BadRequest("Invalid UserName Or Password ");

            var hashPassword = _encryptionUtility.HashSHA256(model.Password);
            if (user.Password != hashPassword) return BadRequest("Invalid UserName Or Password ");

            var token = GenerateNewToken(user.Id);
            var refreshToken = Guid.NewGuid();

            var info = new AuthenticateViewModel
            {
                FullName = $"{user.UserName}",
                UserId = user.Id,
                Token = token,
                RefreshToken = refreshToken.ToString()
            };

            var userRefreshToken = await _userService.GetRefreshTokenAsync(user.Id);
            var userToken = new UserRefreshTokenViewModel
            {
                UserId = user.Id,
                RefreshToken = Guid.Parse(refreshToken.ToString()),
                CreateDate = DateTime.Now,
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
        {/*
            if(refreshToken.Length!=36 || refreshToken == null) return BadRequest("invalid refresh token format");

            var refreshTokenTimeOut = _configuration.GetValue<int>("RefreshTokenTimeOut");
            //todo
            int userId = int.Parse(User.Claims.Where(c => c.Type == "userid").SingleOrDefault().Value);

            var userRefreshToken = await _userService.GetRefreshTokenAsync(refreshToken);
            if (userRefreshToken== null || userRefreshToken.RefreshToken==null) return BadRequest("invalid refresh token");
            if (userRefreshToken.RefreshToken != Guid.Parse(refreshToken.ToString())) return BadRequest("invalid refresh token");
            if (!userRefreshToken.IsValid) return BadRequest("is invalid refresh token");
            if (userRefreshToken.CreateDate.AddMinutes(refreshTokenTimeOut) < DateTime.Now) return BadRequest("expire refresh token");
            */

            if (refreshToken.Length != 36 || refreshToken == null) return BadRequest("invalid refresh token format");

            var refreshTokenTimeOut = _configuration.GetValue<int>("RefreshTokenTimeOut");

            int userId = int.Parse(User.Claims.SingleOrDefault(q => q.Type == "userid").Value);
            var userRefreshToken = await _userService.GetRefreshTokenAsync(userId);
            if (userRefreshToken == null) return BadRequest("invalid request");
            if (userRefreshToken.RefreshToken != Guid.Parse(refreshToken)) return BadRequest("invalid refresh token");
            if (!userRefreshToken.IsValid) return BadRequest("is invalid refresh token");
            if (userRefreshToken.CreateDate.AddMinutes(refreshTokenTimeOut) < DateTime.Now) return BadRequest("expire refresh token");

            var newToken = GenerateNewToken(userRefreshToken.UserId);
            var newRefreshToken = Guid.NewGuid();

            var userToken = new UserRefreshTokenViewModel
            {
                UserId = userRefreshToken.UserId,
                RefreshToken = newRefreshToken,
                CreateDate = DateTime.Now,
                IsValid = true
            };

            await _userService.UpdateRefreshTokenAsync(userToken);

            return Ok(new { Token = newToken, RefreshToken = newRefreshToken });

        }

        private string GenerateNewToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenKey"));
            var tokenTimeOut = _configuration.GetValue<int>("TokenTimeOut");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userid", userId.ToString()),
                }),

                Expires = DateTime.UtcNow.AddMinutes(tokenTimeOut),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
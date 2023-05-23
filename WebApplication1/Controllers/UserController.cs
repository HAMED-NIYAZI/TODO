using Application.Common;
using Application.Services.Interfaces;
using Domain.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace Api.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _userService;
        private EncryptionUtility _encryptionUtility;
        public UserController(IUserService userService, EncryptionUtility encryptionUtility)
        {
            _userService = userService;
            _encryptionUtility = encryptionUtility;
        }

        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Write your summary here")]
        public async Task<IActionResult> Add(CreateUserViewModel model)
        {
            //check validation
             var hashPassword = _encryptionUtility.HashSHA256(model.Password);

            var user = new CreateUserViewModel
            {
                UserName = model.UserName,
                Password = hashPassword,
            };

            await _userService.InsertAsync(user);
            return Ok();
        }
    }
}
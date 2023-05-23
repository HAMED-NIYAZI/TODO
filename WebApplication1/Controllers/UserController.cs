using Application.Common;
using Application.Services.Interfaces;
using Domain.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Post(UserViewModel model)
        {
            //check validation
            var saltPassword = Guid.NewGuid();
            var hashPassword = _encryptionUtility.HashWithSalt(model.Password, saltPassword);

            var user = new UserViewModel
            {
                UserName = model.UserName,
                Password = hashPassword,
            };

            await _userService.InsertAsync(user);
            return Ok();
        }
    }
}
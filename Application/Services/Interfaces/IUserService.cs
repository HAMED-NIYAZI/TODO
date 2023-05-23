using Domain.ViewModel.User;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task InsertAsync(CreateUserViewModel model);
        Task<UserInfoViewModel> GetAsync(int userId);
        Task<UserInfoViewModel> GetAsync(string userName);
        Task<UserRefreshTokenViewModel> GetRefreshTokenAsync(int userId);
        Task InsertRefreshTokenAsync(UserRefreshTokenViewModel model);
        Task UpdateRefreshTokenAsync(UserRefreshTokenViewModel model);
    }
}

using Domain.ViewModel.User;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task InsertAsync(UserViewModel model);
        Task<UserViewModel> GetAsync(Guid userId);
        Task<UserViewModel> GetAsync(string userName);
        Task<UserRefreshTokenViewModel> GetRefreshTokenAsync(Guid userId);
        Task InsertRefreshTokenAsync(UserRefreshTokenViewModel model);
        Task UpdateRefreshTokenAsync(UserRefreshTokenViewModel model);
    }
}

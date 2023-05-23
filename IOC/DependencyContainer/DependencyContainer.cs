using Application.Common;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IOC.DependencyContainer
{
    public static class DependencyContainer
    {
        public static void RegisterServices( this IServiceCollection services)
        {
            #region Repositories
            //  services.AddScoped<IRepository, Repository>();
            #endregion

            #region  Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<EncryptionUtility>();
             #endregion
        }
    }
}

using Application.Common;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Domain.Interface;
using Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace IOC.DependencyContainer
{
    public static class DependencyContainer
    {
        public static void RegisterServices( this IServiceCollection services)
        {
            #region Repositories
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region  Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<EncryptionUtility>();
             #endregion
        }
    }
}

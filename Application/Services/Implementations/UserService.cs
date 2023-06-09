﻿using Application.Services.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Domain.Models.User;
using Domain.ViewModel.User;
using Microsoft.Extensions.Configuration;


namespace Application.Services.Implementations
{

    public class UserService : IUserService
    {
        private IConfiguration _configuration;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<UserInfoViewModel> GetAsync(int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TODODBConnection")))
            {
                var sql = "Select * From Users Where Id=@userId";
                var user = await connection.QuerySingleOrDefaultAsync<UserInfoViewModel>(sql, new { userId = userId });
                return user;
            }
        }

        public async Task<UserInfoViewModel> GetAsync(string userName)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TODODBConnection")))
            {
                var sql = "Select * From Users Where userName=@userName";
                var user = await connection.QuerySingleOrDefaultAsync<UserInfoViewModel>(sql, new { userName = userName });
                return user;
            }
        }

        public async Task<UserRefreshTokenViewModel> GetRefreshTokenAsync(int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TODODBConnection")))
            {
                var sql = "Select * From UserRefreshTokens Where userId=@userId";
                var result = await connection.QuerySingleOrDefaultAsync<UserRefreshTokenViewModel>(sql, new { userId = userId });
                return result;
            }
        }

        public async Task<UserRefreshTokenViewModel> GetRefreshTokenAsync(string refreshToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TODODBConnection")))
            {
                var sql = "Select * From UserRefreshTokens Where RefreshToken=@refreshToken";
                var result = await connection.QuerySingleOrDefaultAsync<UserRefreshTokenViewModel>(sql, new { RefreshToken = refreshToken });
                return result;
            }
        }

        public async Task InsertAsync(CreateUserViewModel model)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TODODBConnection")))
            {
                var sql = @"Insert Users(UserName,Password,CreateDate,IsDeleted) 
                            VALUES(@UserName,@Password,getdate(),0)";
                await connection.ExecuteAsync(sql, model);
            }
        }

        public async Task InsertRefreshTokenAsync(UserRefreshTokenViewModel model)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TODODBConnection")))
            {
                var sql = @"Insert UserRefreshTokens(UserId,RefreshToken,CreateDate,IsValid) 
                            VALUES(@UserId,@RefreshToken,@CreateDate,@IsValid)";
                await connection.ExecuteAsync(sql, model);
            }
        }

        public async Task UpdateRefreshTokenAsync(UserRefreshTokenViewModel model)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TODODBConnection")))
            {
                var sql = @"Update UserRefreshTokens set RefreshToken=@RefreshToken,
                            CreateDate=@CreateDate,IsValid = @IsValid
                            WHERE UserId=@UserId";
                await connection.ExecuteAsync(sql, model);
            }
        }
    }
}

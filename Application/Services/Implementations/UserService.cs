using Application.Services.Interfaces;
using Dapper;
using Domain.ViewModel.User;
using Microsoft.Data.SqlClient;
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
        public async Task<UserViewModel> GetAsync(Guid userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TODODBConnection")))
            {
                var sql = "Select * From Users Where Id=@userId";
                var user = await connection.QuerySingleOrDefaultAsync<UserViewModel>(sql, new { userId = userId });
                return user;
            }
        }

        public async Task<UserViewModel> GetAsync(string userName)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TODODBConnection")))
            {
                var sql = "Select * From Users Where userName=@userName";
                var user = await connection.QuerySingleOrDefaultAsync<UserViewModel>(sql, new { userName = userName });
                return user;
            }
        }

        public async Task<UserRefreshTokenViewModel> GetRefreshTokenAsync(Guid userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TODODBConnection")))
            {
                var sql = "Select * From UserTokens Where userId=@userId";
                var result = await connection.QuerySingleOrDefaultAsync<UserRefreshTokenViewModel>(sql, new { userId = userId });
                return result;
            }
        }

        public async Task InsertAsync(UserViewModel model)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TODODBConnection")))
            {
                var sql = @"Insert Users(Id,FirstName,LastName,UserName,Password,PasswordSalt,IsActive) 
                            VALUES(@Id,@FirstName,@LastName,@UserName,@Password,@PasswordSalt,@IsActive)";
                await connection.ExecuteAsync(sql, model);
            }
        }

        public async Task InsertRefreshTokenAsync(UserRefreshTokenViewModel model)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TODODBConnection")))
            {
                var sql = @"Insert UserTokens(UserId,RefreshToken,GenerateDate,IsValid) 
                            VALUES(@UserId,@RefreshToken,@GenerateDate,@IsValid)";
                await connection.ExecuteAsync(sql, model);
            }
        }

        public async Task UpdateRefreshTokenAsync(UserRefreshTokenViewModel model)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("TODODBConnection")))
            {
                var sql = @"Update UserTokens set RefreshToken=@RefreshToken,
                            GenerateDate=@GenerateDate,IsValid = @IsValid
                            WHERE UserId=@UserId";
                await connection.ExecuteAsync(sql, model);
            }
        }
    }
}

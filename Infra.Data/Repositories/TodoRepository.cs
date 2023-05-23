using Domain.Interface;
using Domain.Model.Todo;
using Domain.ViewModel.User;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infra.Data.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        #region ctor
        private readonly IConfiguration _configuration;
        private readonly string GetConnection;

        public TodoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            GetConnection = _configuration.GetConnectionString("TODODBConnection");
        }

        #endregion
        public async Task Add(Todo model)
        {
            using (var connection = new SqlConnection(GetConnection))
            {
                var sql = @"Insert Todos(TaskTitle,TaskDescription,UserId,Priority,CreateDate,IsDeleted) 
                            VALUES(@TaskTitle,@TaskDescription,@UserId,@Priority,getdate(),0)";
                await connection.ExecuteAsync(sql, model);
            }
        }

        public async Task<List<Todo>> GetAll()
        {
            using (var connection = new SqlConnection(GetConnection))
            {
                var sql = "Select * From Todos Where   IsDeleted=0";
                var Todos = await connection.QueryAsync<Todo>(sql);
                return Todos.ToList();
            }
        }

        public async Task<List<Todo>> GetAll(int userId)
        {
            using (var connection = new SqlConnection(GetConnection))
            {
                var sql = "Select * From Todos Where UserId=@userId and IsDeleted=0";
                var Todos = await connection.QueryAsync<Todo>(sql, new { UserId = userId });
                return Todos.ToList();
            }
        }

        public async Task<Todo?> GetById(int id)
        {
            using (var connection = new SqlConnection(GetConnection))
            {
                var sql = "Select * From Todos Where Id=@id";
                var user = await connection.QuerySingleOrDefaultAsync<Todo>(sql, new { Id = id });
                return user;
            }
        }

        public async Task Remove(int id)
        {
            using (var connection = new SqlConnection(GetConnection))
            {
                var sql = "Update Todos Set IsDeleted=1 Where Id=@id";
                await connection.QuerySingleOrDefaultAsync(sql, new { Id = id });
            }
        }

        public async Task Update(Todo model)
        {
            using (var connection = new SqlConnection(GetConnection))
            {
                var sql = "Update Todos Set TaskTitle=@TaskTitle ,TaskDescription=@TaskDescription ,Priority=@Priority Where Id=@Id";
                await connection.QuerySingleOrDefaultAsync(sql, model);
            }
        }
    }
}

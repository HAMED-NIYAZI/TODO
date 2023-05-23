using Domain.Interface;
using Domain.Models.User;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        #region ctor
        public UserRepository(TodoContext context) : base(context)
        {
        }
        #endregion
 
    }
}

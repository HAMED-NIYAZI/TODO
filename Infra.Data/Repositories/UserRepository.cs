using LawyerCoreApp.Domain.Interface;
using LawyerCoreApp.Domain.Models.User;
using LawyerCoreApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LawyerCoreApp.Infra.Data.Repositories
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

using Domain.Interface;
using Domain.Models.User;
using Infra.Data.Context;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Infra.Data.Repositories
{
    public  class GenericRepositoryTest
    {
        private readonly TodoContext _context;
       // private DbSet<T> dbSet;


 

        [Fact]
        public async void Remove_Todo()
        {
            //Arrange
            GenericRepository<User> repo = new GenericRepository<User>(_context);

            //Act
            var res = Task.FromResult(repo.Remove(21));

            //Assert
            Assert.IsType<Task<Task>?>(res);
        }
    }
}

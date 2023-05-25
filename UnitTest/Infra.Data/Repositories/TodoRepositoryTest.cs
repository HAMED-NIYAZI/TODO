using Api.Controllers;
using Application.Common;
using Application.Services.Interfaces;
using Domain.Enum;
using Domain.Model.Todo;
using Infra.Data.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace UnitTest.Infra.Data.Repositories
{
    public class TodoRepositoryTest
    {
        private readonly IConfiguration _configuration;



        [Fact]
        public async void Remove_Todo()
        {
            //Arrange
            TodoRepository repo = new TodoRepository(_configuration);

            //Act
            var res = Task.FromResult(repo.Remove(21));

            //Assert
            Assert.IsType<Task<Task>?>(res);
        }


        [Fact]
        public async void GetById_Todo()
        {
            //Arrange
            TodoRepository repo = new TodoRepository(_configuration);

            //Act
            var res = repo.GetById(21);

            //Assert
            Assert.IsType<Task<Todo?>>(res);
        }
        

        [Fact]
        public async void GetAll_ByUserID_Todo()
        {
            //Arrange
            TodoRepository repo = new TodoRepository(_configuration);

            //Act
            var res = repo.GetAll(21);

            //Assert
            Assert.IsType<Task<List<Todo>>>(res);
        }


        [Fact]
        public async void GetAll_Todo()
        {
            //Arrange
            TodoRepository repo = new TodoRepository(_configuration);

            //Act
            var res = repo.GetAll();

            //Assert
            Assert.IsType<Task<List<Todo>>>(res);
        }



        [Fact]
        public async void Add_Todo()
        {
            //Arrange
            TodoRepository repo = new TodoRepository(_configuration);

            var newTodo = new Todo()
            {
                CreateDate = DateTime.Now,
                IsDeleted = false,
                Priority = TodoPriority.Low,
                TaskDescription = "TaskDescription",
                TaskTitle = "TaskTitle",
                UserId = 1
            };

            //Act
            var res = Task.FromResult(repo.Add(newTodo));

            //Assert
            Assert.IsType<Task<Task>?>(res);

        }


    }
}

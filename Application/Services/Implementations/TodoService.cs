using Application.Services.Interfaces;
using Domain.Interface;
using Domain.Model.Todo;
using Domain.Models.User;
using Domain.ViewModel.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class TodoService : ITodoService
    {
        #region ctor
        private readonly ITodoRepository _todoRepository;
        private readonly IUserRepository _userRepository;


        public TodoService(ITodoRepository repository, IUserRepository userRepository)
        {
            _todoRepository = repository;
            _userRepository = userRepository;
        }
        #endregion

        public async Task Add(CreateTodoDto model)
        {
            var newTodo = new Todo()
            {
                CreateDate = DateTime.Now,
                IsDeleted = false,
                Priority = model.Priority,
                TaskDescription = model.TaskDescription,
                TaskTitle = model.TaskTitle,
                UserId = model.UserId
            };
            await _todoRepository.Add(newTodo);
        }

        public async Task Update(UpdateTodoViewModel model)
        {
            var todo = new Todo()
            {
                Id = model.Id,
                CreateDate = DateTime.Now,
                IsDeleted = model.IsDeleted,
                Priority = model.Priority,
                TaskDescription = model.TaskDescription,
                TaskTitle = model.TaskTitle,
                UserId = model.UserId
            };
            await _todoRepository.Update(todo);
        }

        public async Task<ShowTodoViewModel?> Get(int id)
        {
            var model = await _todoRepository.GetById(id);
            return new ShowTodoViewModel()
            {
                Id = model.Id,
                TaskTitle = model.TaskTitle,
                TaskDescription = model.TaskDescription,
                Priority = model.Priority,
                UserId = model.UserId,
            };
        }

        public async Task<List<ShowTodoViewModel>?> GetAll(int userId)
        {
            var models = await _todoRepository.GetAll(userId);
            if (models == null || models.Count == 0) return null;

            return models.Select(q => new ShowTodoViewModel()
            {
                Id = q.Id,
                TaskTitle = q.TaskTitle,
                TaskDescription = q.TaskDescription,
                Priority = q.Priority,
                UserId = q.UserId,
            }).ToList();

        }

        public async Task<List<ShowTodoViewModel>?> GetAll()
        {
            var models = await _todoRepository.GetAll();
            if (models == null || models.Count == 0) return null; 

            return models.Select(q => new ShowTodoViewModel()
            {
                Id = q.Id,
                TaskTitle = q.TaskTitle,
                TaskDescription = q.TaskDescription,
                Priority = q.Priority,
                UserId = q.UserId,
            }).ToList();
        }

        public async Task Remove(int id)
        {
            await _todoRepository.Remove(id);
        }

        public async Task<UpdateTodoViewModel?> GetForEdit(int id)
        {
            var model = await _todoRepository.GetById(id);
            if (model == null) return null;
            return new UpdateTodoViewModel()
            {
                Id = model.Id,
                TaskTitle = model.TaskTitle,
                TaskDescription = model.TaskDescription,
                Priority = model.Priority,
                UserId = model.UserId,
                IsDeleted=model.IsDeleted,
            };
        }
    }
}

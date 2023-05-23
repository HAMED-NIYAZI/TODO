using Domain.ViewModel.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ITodoService
    {
        Task Add(CreateTodoDto model);
        Task Update(UpdateTodoViewModel model);
        Task Remove(int id);
        Task<ShowTodoViewModel?> Get(int id);
        Task<UpdateTodoViewModel?> GetForEdit(int id);
        Task<List<ShowTodoViewModel>?> GetAll(int userId);
        Task<List<ShowTodoViewModel>?> GetAll();
 
    }
}

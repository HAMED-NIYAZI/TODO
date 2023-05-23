using Domain.Model.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ITodoRepository
    {
        Task Add(Todo model);
        Task Update(Todo model);
        Task<Todo?> GetById(int id);
        Task< List<Todo >> GetAll();
        Task< List<Todo >> GetAll(int userId);
        Task Remove(int id);
    }

 
}

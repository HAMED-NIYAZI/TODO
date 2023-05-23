using Application.Services.Interfaces;
using Domain.ViewModel.Todo;
using Domain.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : BaseController
    {
        #region ctor
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        #endregion


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(CreateTodoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState is not valid");
            }
            //fill user id
            int userId = int.Parse(User.Claims.Where(c => c.Type == "userid").SingleOrDefault().Value);
            var newTodo = new CreateTodoDto() {
                Priority = model.Priority,
                TaskDescription = model.TaskDescription,
                TaskTitle = model.TaskTitle,
                UserId = userId ,
            };
            await _todoService.Add(newTodo);
            return Ok("Todo Added");
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UpdateTodoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState is not valid");
            }
            //fill user id
            int userId = int.Parse(User.Claims.Where(c => c.Type == "userid").SingleOrDefault().Value);
        
            if(userId!=model.UserId) return BadRequest($"Update is not Allowed for this model id=({model.Id}) . You are not the owner");

            await _todoService.Update(model);
            return Ok("Todo Updated");
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Remove(int id)
        {
 
            int userId = int.Parse(User.Claims.Where(c => c.Type == "userid").SingleOrDefault().Value);

            var getTodo =await _todoService.GetForEdit(id);
            if (getTodo == null) return BadRequest($"Todo With Id=({id}) Not Found");
            if (userId != getTodo.UserId) return BadRequest($"Remove is not Allowed for this model id=({id}) . You are not the owner ");
             await _todoService.Remove(id);
            return Ok($"Todo by Id ({id})  Deleted");
        }

        [HttpGet("GetAllUserTodo")]
        [AllowAnonymous]
         public async Task<IActionResult> GetAllUserTodo(int userId)
        {

            var data = await _todoService.GetAll(userId);
            if (data == null || data.Count==0 ) return BadRequest($"There is nothing in database");
             return Ok(data);
        }



        [HttpGet("GetAllTodo")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllTodo()
        {

            var data = await _todoService.GetAll();
            if (data == null || data.Count == 0) return BadRequest($"There is nothing in database");
            return Ok(data);
        }


    }
}


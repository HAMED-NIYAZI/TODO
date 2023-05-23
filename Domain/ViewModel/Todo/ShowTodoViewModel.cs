using Domain.Enum;
using Domain.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel.Todo
{
    public class ShowTodoViewModel
    {
        public int Id { get; set; }
        public string TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        public int UserId { get; set; }
        public UserInfoViewModel User { get; set; }
        public TodoPriority Priority { get; set; }
    }
}

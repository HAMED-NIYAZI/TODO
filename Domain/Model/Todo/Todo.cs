using Domain.Enum;
using Domain.Models.Common;
using Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Todo
{
    public class Todo : BaseEntity<int>
    {
        public string TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public TodoPriority Priority { get; set; }
    }
}

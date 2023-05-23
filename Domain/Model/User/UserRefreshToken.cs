using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Models.User;
     public partial class UserRefreshToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid RefreshToken { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsValid { get; set; }

        public virtual User User { get; set; }
    }
 
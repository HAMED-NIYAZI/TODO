using System;

namespace Domain.ViewModel.User;
 
    public class UserRefreshTokenViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid RefreshToken { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsValid { get; set; }
    }
 
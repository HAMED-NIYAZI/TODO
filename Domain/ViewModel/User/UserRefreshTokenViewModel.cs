using System;

namespace Domain.ViewModel.User;
 
    public class UserRefreshTokenViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime GenerateDate { get; set; }
        public bool IsValid { get; set; }
    }
 
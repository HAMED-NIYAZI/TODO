using System;

namespace Domain.ViewModel.User;
     public class AuthenticateViewModel
    {
        public string FullName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
    
    }
 
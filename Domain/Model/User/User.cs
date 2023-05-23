using Domain.Models.Common;
using Domain.Shared;
using System.ComponentModel.DataAnnotations;


namespace Domain.Models.User;

public class User : BaseEntity<int>
{
    #region Properties

    [Display(Name = "UserName")]
    [MaxLength(32, ErrorMessage = ErrorMessages.MaxLengthError)]
     public string? UserName { get; set; }

    [Display(Name = "Password")]
    [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Password { get; set; }
 
    #endregion

}

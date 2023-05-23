using Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Domain.ViewModel.User;
public class CreateUserViewModel
{
    #region Properties

   // public int Id { get; set; }

    [Display(Name = "UserName")]
    [MaxLength(32, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? UserName { get; set; }

    [Display(Name = "Password")]
    [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Password { get; set; }

    #endregion
}


public class UserLoginViewModel
{
    #region Properties

    // public int Id { get; set; }

    [Display(Name = "UserName")]
    [MaxLength(32, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? UserName { get; set; }

    [Display(Name = "Password")]
    [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Password { get; set; }

    #endregion
}

public class UserInfoViewModel
{
    #region Properties

    public int Id { get; set; }

    [Display(Name = "UserName")]
    [MaxLength(32, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? UserName { get; set; }

    [Display(Name = "Password")]
    [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Password { get; set; }

    #endregion
}

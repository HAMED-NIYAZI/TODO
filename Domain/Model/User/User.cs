using LawyerCoreApp.Domain.Models.Common;
using LawyerCoreApp.Domain.Shared;
using System.ComponentModel.DataAnnotations;


namespace LawyerCoreApp.Domain.Models.User;

public class User : BaseEntity<int>
{
    #region Properties

    [Display(Name = "Email")]
    [EmailAddress(ErrorMessage = ErrorMessages.EmailFormatError)]
    [MaxLength(350, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Email { get; set; }

    [Display(Name = "Mobile")]
    [MaxLength(15, ErrorMessage = ErrorMessages.MaxLengthError)]
    [RegularExpression("^[0-9]*$", ErrorMessage = ErrorMessages.MobileFormatError)]
    public string? Mobile { get; set; }

    [Display(Name = "FirstName")]
    [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? FirstName { get; set; }

    [Display(Name = "LastName")]
    [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? LastName { get; set; }

    [Display(Name = "Password")]
    [MaxLength(300, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Password { get; set; }

    [Display(Name = "EmailActiveCode")]
    [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? EmailActiveCode { get; set; }

    [Display(Name = "MobileActiveCode")]
    //[Required(ErrorMessage = ErrorMessages.RequiredError)]
    [MaxLength(10, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? MobileActiveCode { get; set; }

    [Display(Name = "IsEmailActive")]
    public bool IsEmailActive { get; set; }

    [Display(Name = "IsMobileActive")]
    public bool IsMobileActive { get; set; }

    [Display(Name = "Avatar")]
    [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthError)]
    public string? Avatar { get; set; }

    [Display(Name = "IsActive")]
    public bool IsActive { get; set; }

    [Display(Name = "IsAdmin")]
    public bool IsAdmin { get; set; }

    #endregion


    #region relatiion

   // public ICollection<UserRole>? UserRoles { get; set; }

    #endregion

}

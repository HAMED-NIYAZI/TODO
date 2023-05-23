using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Common;

public class BaseEntity<T>
{
    #region Properties
    
    [Key]
    public T Id { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.Now;    

    public bool IsDeleted { get; set; }
    
    #endregion
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebApplication3.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
 
    [DisplayName("display order")]
    [Range(1,100 , ErrorMessage = "Display order must be between 1 and 100")]
    public int DisplayOrder { get; set; }

    public string CreatedDateTime { get; set; } = Convert.ToString(DateTime.Now, CultureInfo.InvariantCulture);
}



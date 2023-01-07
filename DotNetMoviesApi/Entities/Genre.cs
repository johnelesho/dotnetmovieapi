using System.ComponentModel.DataAnnotations;

namespace DotNetMoviesApi.Entities;


public class Genre
{
    public int Id { get; set; }
    [Required(ErrorMessage = "The field with the name {0} is required")]
    [StringLength(50,ErrorMessage = "{0} must be at least {1} characters")]
    public string Name  { get; set; }
}
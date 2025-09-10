using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookStoreApi.Models;

public class Author
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Author name is required")]
    [StringLength(100, ErrorMessage = "Author name cannot be longer than 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    // Navigation property
    [JsonIgnore]
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookStoreApi.Models;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Book title is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Publication year is required")]
    [Range(1000, 9999, ErrorMessage = "Publication year must be a valid 4-digit year")]
    public int PublicationYear { get; set; }
    
    // Foreign key
    [Required(ErrorMessage = "Author ID is required")]
    [ForeignKey("Author")]
    public int AuthorId { get; set; }
    
    // Navigation property
    [JsonIgnore]
    public Author? Author { get; set; }
}
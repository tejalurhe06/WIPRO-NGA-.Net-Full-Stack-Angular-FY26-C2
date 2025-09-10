using System.ComponentModel.DataAnnotations;

namespace MovieCatalogApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [Range(1888, 2030)]
        public int ReleaseYear { get; set; }
        
        public int DirectorId { get; set; }
        public Director? Director { get; set; }
    }
}
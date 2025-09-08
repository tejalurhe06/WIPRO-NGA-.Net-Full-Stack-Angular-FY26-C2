using System;
using System.ComponentModel.DataAnnotations; 
using LibraryManagementSystem.Core.Models;
namespace LibraryManagementSystem.Core.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }  // Navigation property

        [Required]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }    // Navigation property
    }
}

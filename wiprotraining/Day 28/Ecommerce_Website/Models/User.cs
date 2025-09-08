using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain letters and numbers")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(4, ErrorMessage = "Password must be at least 4 characters")]
    public string Password { get; set; }
}

using System.ComponentModel.DataAnnotations;
namespace be_scrapping;

public class RegistrationRequest{
    [Required]
    public string Email {get; set;} = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;

}

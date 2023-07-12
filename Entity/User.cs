using Microsoft.AspNetCore.Identity;
using web_scraping.Entity;



public class User : IdentityUser
{
    public required string FirsName {get; set;}
    public required string LastName { get; set;}

    public ICollection<History>? Histories { get; set;}

}
    


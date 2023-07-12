using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using web_scraping;
using be_scrapping;

namespace web_scraping.Controllers;
[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    private readonly ScrapingContext _scrapingContext;
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;

    public AuthController(ScrapingContext scrapingContext, UserManager<User> userManager, TokenService tokenService)
    {
        _scrapingContext = scrapingContext;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("register")]

    async public Task<IActionResult> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await _userManager.CreateAsync(new User
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        },request.Password);
        request.Password = "";

        if (user.Succeeded)
            return CreatedAtAction(nameof(Register), new
            {
                email = request.Email
            }, request);

        foreach (var error in user.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return BadRequest(ModelState);
    }

    [HttpPost]
    [Route("login")]
    async public Task <ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user == null)
        {
            return BadRequest("User does not exists");
        }

        bool isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            return BadRequest("Incorrect password");
        }

        var userInDb = _scrapingContext.Users.FirstOrDefault(u => u.Email == request.Email);
        if (userInDb == null)
            return Unauthorized();
        var Token = _tokenService.CreateToken(userInDb);
        await _scrapingContext.SaveChangesAsync();

        ArgumentNullException.ThrowIfNull(user.UserName);
        ArgumentNullException.ThrowIfNull(request.Email);

        return Ok(new AuthResponse
        {
            UserName = user.UserName,
            Email = request.Email,
            Token = Token
        });
    }
}
    



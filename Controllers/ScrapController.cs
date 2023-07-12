using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using web_scraping.Entity;

namespace web_scraping.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScrapController : ControllerBase
    {
        private readonly ScrapingContext _scrapingContext;

        private readonly UserManager<User> _userManager;



        public ScrapController(
            ScrapingContext scrapingContext,
            UserManager<User> userManager
        ) 
            
        {
            _scrapingContext = scrapingContext;
            _userManager = userManager;
 
        }

        [HttpPost,Authorize]
        [Route("scrap")]
        async public Task<IActionResult> Scrap(string companyName)
        {
            ScrappingService scrappingService = new ScrappingService("https://www.occ.com.mx/");

            var history = _scrapingContext.Histories.Add(new History
            {
                CompanyName = companyName,
                JobsCount = await scrappingService.GetResults(companyName),
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            });
            await _scrapingContext.SaveChangesAsync();

            return Ok(history);
        }

    }
}
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

        [HttpPost, Authorize]
        async public Task<IActionResult> Scrap(string companyName)
        {
            ScrappingService scrappingService = new ScrappingService("https://www.occ.com.mx/");
            
            int jobsCount =  await scrappingService.GetResults(companyName);

            _scrapingContext.Histories.Add(new History
            {
                CompanyName = companyName,
                JobsCount = jobsCount,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            });
            await _scrapingContext.SaveChangesAsync();

            return Ok(new{
                JobsCount = jobsCount,
                CompanyName = companyName
            });
        }

        [HttpGet, Authorize]
        public object GetHistory(int page) => new {
            Info = new {
            total = _scrapingContext.Histories
                .Where(history => history.UserId.Equals(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                .Count()
            },
            results = _scrapingContext.Histories
                .OrderByDescending(history => history.UserId)
                .Where(history => history.UserId.Equals(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                .Skip(page * 10)
                .Take(10)
                .ToList()
        };

    }
}

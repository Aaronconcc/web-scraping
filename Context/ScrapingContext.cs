using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using web_scraping.Entity;

public class ScrapingContext : IdentityUserContext<User>
{
    private readonly IConfiguration _config;

    public ScrapingContext(DbContextOptions<ScrapingContext> options, IConfiguration config)
        : base(options)
    {
        _config = config;
    }

    public DbSet<History> Histories {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("Sql"));
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

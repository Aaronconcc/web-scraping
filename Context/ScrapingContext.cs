using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


class ScrapingContext : IdentityUserContext<User>
{
    private readonly IConfiguration _config;
    public ScrapingContext(IConfiguration config)
    {
        _config = config;
    }
    public ScrapingContext(DbContextOptions<ScrapingContext> options, IConfiguration config)
        : base(options)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("Sql"));
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
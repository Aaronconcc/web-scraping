namespace web_scraping.Entity
{
    public class History
    {
        public int Id { get; set; }
        public required string CompanyName { get; set; }
        public int JobsCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public required string UserId {get; set;}
        public virtual User? User { get; set;}   
       
    }
}



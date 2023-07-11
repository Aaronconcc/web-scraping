﻿

namespace web_scraping.Entity
{
    class History
    {
        public required string CompanyName { get; set; }
        public int JobsCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public required string UserId {get; set;}
        public virtual User? User { get; set;}



        
       
    }
}


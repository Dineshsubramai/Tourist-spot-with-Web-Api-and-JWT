using Microsoft.EntityFrameworkCore;
using TouristWebApi.Models;


namespace TouristWebApi.Dbcontext
{
    public class Tourist_DbContext : DbContext
    {
     
        public Tourist_DbContext(DbContextOptions<Tourist_DbContext> options) : base(options)
        {
        }
        public DbSet<place> Places { get; set; }

        public DbSet<User> Users { get; set; }

    }
}

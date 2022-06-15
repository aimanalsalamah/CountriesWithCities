using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
    : base(options)
        {
        }
        public DbSet<BLL.DB.Master.Countries> Countries { get; set; }
        public DbSet<BLL.DB.Master.Cities> Cities { get; set; }
    }
}

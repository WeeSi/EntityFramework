using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PostgreSQL.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<BlogTag> BlogTag { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}
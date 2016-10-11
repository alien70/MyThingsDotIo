using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyThingsDotIo.Models
{
    public class MyThingsDotIoContext : DbContext
    {
        private IConfigurationRoot _config;

        public MyThingsDotIoContext(IConfigurationRoot config, DbContextOptions options) 
            : base(options)
        {
            _config = config;

        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config["ConnectionStrings:MyThingsDotIoConnection"]);
        }
    }
}



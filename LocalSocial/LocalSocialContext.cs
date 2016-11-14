using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using LocalSocial.Models;
using Microsoft.Data.Entity.Infrastructure;

namespace LocalSocial
{
    public class LocalSocialContext : IdentityDbContext<User>
    {
        // Your context has been configured to use a 'LocalSocialContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LocalSocial.LocalSocialContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'LocalSocialContext' 
        // connection string in the application configuration file.


        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=tcp:poznan.database.windows.net,1433;Initial Catalog=LocalSocial;Persist Security Info=False;User ID=poznan;Password=Kaczka1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<Post> Post { get; set; }
    }
}

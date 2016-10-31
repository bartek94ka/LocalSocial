using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LocalSocial.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LocalSocial
{
    public class LocalSocialContext : IdentityDbContext
    {
        // Your context has been configured to use a 'LocalSocialContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LocalSocial.LocalSocialContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'LocalSocialContext' 
        // connection string in the application configuration file.
        public LocalSocialContext()
            : base("Server=tcp:poznan.database.windows.net,1433;Initial Catalog=LocalSocial;Persist Security Info=False;User ID=poznan;Password=Kaczka1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
        {
        }
        public DbSet<Post> Post { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

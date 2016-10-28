using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalSocial.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

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
            : base("name=Model1")
        {
        }
        public static LocalSocialContext Create()
        {
            return new LocalSocialContext();
        }
        public DbSet<Post> Post { get; set; }
        public DbSet<User> User { get; set; }
    }
}

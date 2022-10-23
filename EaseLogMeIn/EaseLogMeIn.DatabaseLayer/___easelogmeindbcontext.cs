using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.DatabaseLayer
{
   public class EaseLogMeInDbContext: DbContext
    {
        public EaseLogMeInDbContext():base("EaseLogMeIn")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Website>().ToTable("Website");
            modelBuilder.Entity<Users>().ToTable("WebUsers");
            modelBuilder.Entity<WebsiteUserMapping>().ToTable("WebsiteUserMapping");
            modelBuilder.Entity<UserActionData>().ToTable("UserActionData");
            modelBuilder.Entity<WebsiteAccessLog>().ToTable("WebsiteAccessLog");
            modelBuilder.Entity<CustomException>().ToTable("CustomException");
            modelBuilder.Entity<DesktopAppLoginHistory>().ToTable("DesktopAppLoginHistory");
            modelBuilder.Entity<RequestLoger>().ToTable("RequestLoger");

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Users> WebUsers { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Website> Website { get; set; }
        public DbSet<WebsiteUserMapping> WebsiteUserMapping { get; set; }
        public DbSet<UserActionData> UserActionData { get; set; }
        public DbSet<WebsiteAccessLog> WebsiteAccessLog { get; set; }
        public DbSet<CustomException> CustomException { get; set; }
        public DbSet<DesktopAppLoginHistory> DesktopAppLoginHistory { get; set; }
        public DbSet<RequestLoger> RequestLoger { get; set; }

    }
}

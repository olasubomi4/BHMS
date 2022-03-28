using BHMS.CORE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHMS.SQL
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<VidUpload> vidupload { get; set; }

        public DbSet<ObjectDetector> objectDetectors { get; set; }

        public DbSet<Hostel> hostels { get; set; }
        public DbSet<HostelRegistration> hostelRegistrations { get; set; }

        public System.Data.Entity.DbSet<BHMS.CORE.Models.UserComplaints> UserComplaints { get; set; }
    }

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}
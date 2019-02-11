using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TB.Db.Entities;

namespace TB.Db
{
    public class ToBuyContext : DbContext
    {
        public ToBuyContext()
        {
        }

        public ToBuyContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
             .HasIndex(p => p.Id);

            modelBuilder.Entity<User>()
             .HasIndex(p => p.Id);

            modelBuilder.Entity<Message>()
            .HasIndex(p => p.Id);

            modelBuilder.Entity<Mail>()
            .HasIndex(p => p.Id);

            modelBuilder.Entity<Ad>()
                .HasIndex(p => p.SearchVector)
                .ForNpgsqlHasMethod("GIN"); // Index method on the search vector (GIN or GIST)
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("BloggingDatabase");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Mail> Mails { get; set; }


    }

}

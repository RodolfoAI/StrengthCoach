using Microsoft.EntityFrameworkCore;
using StrengthCoach.Models;
using System.IO;

namespace StrengthCoach.Data
{
    public class StrengthCoachDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<PunchRecord> PunchRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databasePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "StrengthCoach",
                "database.db"
            );

            // Create directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(databasePath));

            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<PunchRecord>()
                .HasOne(p => p.Student)
                .WithMany(s => s.PunchRecords)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure column types
            modelBuilder.Entity<PunchRecord>()
                .Property(p => p.Force)
                .HasPrecision(5, 2);
        }
    }
}
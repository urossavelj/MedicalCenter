using MedicalCenter.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalCenter.Db
{
    public class Context : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<User> Users { get; set; }

        public Context()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointments>().Navigation(e => e.Patient).AutoInclude();
            modelBuilder.Entity<Appointments>().Navigation(e => e.Doctor).AutoInclude();
        }
    }
}

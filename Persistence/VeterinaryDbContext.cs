using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class VeterinaryDbContext : DbContext
    {
        public VeterinaryDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UserRol> UserRoles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineMovement> MedicineMovements { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Specie> Species { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<TypeMovement> TypeMovements { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
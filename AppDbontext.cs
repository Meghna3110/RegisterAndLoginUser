using Microsoft.EntityFrameworkCore;
using User_Login_and_Registration.Model;


//User_Login_and_Registration.Utility.Enums;

namespace User_Login_and_Registration.Data
{
    public class YourDbContext : DbContext
    {

        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public YourDbContext(DbContextOptions<YourDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>()
           .Property(a => a.AdminId)
           .IsRequired();

            modelBuilder.Entity<UserModel>()
                .Property(u => u.UserId)
                .IsRequired();


            modelBuilder.Entity<UserModel>()
                .Property(u => u.VerificationStatus)
                .HasConversion<string>(); // You can use .HasConversion<int>() for integer enums

            // Optionally, seed data (e.g., an admin) if you want to set up default users/admins
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminId = "admin123",
                    Password = "adminPassword@123"// Ensure it's hashed
                }
            );
        }
    }
}

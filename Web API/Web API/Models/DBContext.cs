using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace Web_API.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(t => t.Id);

            modelBuilder.Entity<Student>().Property(t => t.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Student>().Property(t => t.FullName).IsUnicode(false).IsRequired(false);

            modelBuilder.Entity<Student>().Property(t => t.NativeVillage).IsUnicode(false).IsRequired(false);

            modelBuilder.Entity<Student>().Property(t => t.PhoneNumber).IsUnicode(false).IsRequired(false);

            modelBuilder.Entity<Student>().Property(t => t.Sex).IsUnicode(false);

            modelBuilder.Entity<Student>().Property(t => t.Mail).IsUnicode(false).IsRequired(false);

            modelBuilder.Entity<Account>().HasKey(t => t.Id);

            modelBuilder.Entity<Account>().Property(t => t.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Account>().Property(t => t.Username).IsUnicode(false).IsRequired(false);

            modelBuilder.Entity<Account>().Property(t => t.Password).IsUnicode(false).IsRequired(false);

            modelBuilder.Entity<Account>().Property(t => t.Confirmpassword).IsUnicode(false).IsRequired(false);
        }

    }
}

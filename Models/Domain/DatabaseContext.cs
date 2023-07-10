using Microsoft.EntityFrameworkCore;

namespace Library.Models.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Genre> Genre { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja tabeli Users
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            // Dodatkowe konfiguracje dla klasy User
            // ...

            // Konfiguracje dla innych klas encji
            // ...
        }
    }
}

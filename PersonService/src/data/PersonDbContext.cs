using Microsoft.EntityFrameworkCore;
using PersonService.Entities;
namespace PersonService.Data
{
    public class PersonDbContext: DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .Property(p => p.BirthDate)
                .HasColumnType("date");
        }

        public DbSet<Person> Persons => Set<Person>();
    }
}
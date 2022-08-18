using Microsoft.EntityFrameworkCore;
using SharedDomain.Models;

namespace SharedDomain.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<PersonModel> People { get; set; }
        public DbSet<OccurrencyModel> Occurrencies { get; set; }
    }
}
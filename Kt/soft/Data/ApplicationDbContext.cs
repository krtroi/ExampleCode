using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ktsoft.Models;

namespace Ktsoft.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> o) : IdentityDbContext(o){ 
        public DbSet<Movie> Movie { get; set; } = default!;
        public DbSet<Person> Persons { get; set; } = default!;
    }
}

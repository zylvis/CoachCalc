using CoachCalcAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoachCalcAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Athletee> Athletees { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
    }
}

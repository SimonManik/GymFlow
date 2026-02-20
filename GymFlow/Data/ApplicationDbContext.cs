using Microsoft.EntityFrameworkCore;
using GymFlow.Models;

namespace GymFlow.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) {}


        public DbSet<Member> Members {  get; set; }



    }
}

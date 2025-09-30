using Microsoft.EntityFrameworkCore;
using SmartStudentQueryAPI.Models;

namespace SmartStudentQueryAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Query> Queries { get; set; }
    }
}

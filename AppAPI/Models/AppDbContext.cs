using Microsoft.EntityFrameworkCore;

namespace AppAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<SinhVien> SinhViens { get; set; }
    }
}

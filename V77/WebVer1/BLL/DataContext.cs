using KiotVietBO;
using Microsoft.EntityFrameworkCore;

namespace WebVer1.BLL
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<ChiTietHangHoaBO> ChiTietHangHoaBOs { get; set; }
    }
}
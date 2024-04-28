using Discount_gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount_gRPC.DataAccess
{
    public class CouponDbContext : DbContext
    {
        public CouponDbContext(DbContextOptions<CouponDbContext> options)
            : base(options)
        {
        }
        public DbSet<Coupon> Coupons { get; set; }

    }
}

using Discount_gRPC.DataAccess;
using Discount_gRPC.Models;
using Discount_gRPC.Protos;
using Grpc.Core;

namespace Discount_gRPC.Services
{
    public class DiscountService : DisCountProtoService.DisCountProtoServiceBase
    {
        private readonly CouponDbContext dbContext;
        public DiscountService(CouponDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = new Coupon
            {
                ProductName = request.Coupon.ProductName,
                Description = request.Coupon.Description,
                Amount = request.Coupon.Amount,
            };

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            var model = new CouponModel
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            };

            return model;
        }

        public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FindAsync(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Coupon not found."));
            }
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            return new DeleteDiscountResponse
            {
                Success = true
            };
        }


        public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FindAsync(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Coupon not found."));
            }

            return new CouponModel
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            };
        }

        public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FindAsync(request.Coupon.Id);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Coupon with ID {request.Coupon.Id} not found."));
            }

            coupon.ProductName = request.Coupon.ProductName;
            coupon.Description = request.Coupon.Description;
            coupon.Amount = request.Coupon.Amount;

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            return new CouponModel
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            };
        }
    }
}

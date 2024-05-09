using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObject;
using Ordering.Domain.Enum;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                   .HasConversion(
                       orderId => orderId.Value,
                       dbId => OrderId.Of(dbId));

            builder.Property(o => o.CustomerId)
                   .HasConversion(
                       customerId => customerId.Value,
                       value => CustomerId.Of(value));

            builder.HasOne<Customer>()  // Assumes a Customer entity exists and properly configured elsewhere
                   .WithMany()
                   .HasForeignKey("CustomerId")
                   .IsRequired();

            builder.OwnsOne(o => o.OrderName, on =>
            {
                on.Property(n => n.Value)
                  .HasColumnName("OrderName")
                  .HasMaxLength(100)
                  .IsRequired();
            });

            builder.OwnsOne(o => o.ShippingAdress, sa =>
            {
                sa.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                sa.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                sa.Property(a => a.EmailAddress).HasMaxLength(100).IsRequired();
                sa.Property(a => a.AddressLine).HasMaxLength(200).IsRequired();
                sa.Property(a => a.Country).HasMaxLength(50).IsRequired();
                sa.Property(a => a.State).HasMaxLength(50).IsRequired();
                sa.Property(a => a.ZipCode).HasMaxLength(20).IsRequired();
            });

            builder.OwnsOne(o => o.BillingAddress, ba =>
            {
                ba.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                ba.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                ba.Property(a => a.EmailAddress).HasMaxLength(100).IsRequired();
                ba.Property(a => a.AddressLine).HasMaxLength(200).IsRequired();
                ba.Property(a => a.Country).HasMaxLength(50).IsRequired();
                ba.Property(a => a.State).HasMaxLength(50).IsRequired();
                ba.Property(a => a.ZipCode).HasMaxLength(20).IsRequired();
            });

            builder.OwnsOne(o => o.Payment, p =>
            {
                p.Property(p => p.CardNumber).HasMaxLength(16).IsRequired();
                p.Property(p => p.Expiration).IsRequired();
                p.Property(p => p.CVV).HasMaxLength(3).IsRequired();
                p.Property(p => p.PaymentMethod).IsRequired();  // Assuming PaymentMethod is a string or enum that's correctly defined
            });

            builder.Property(o => o.OrderStatus)
                   .HasDefaultValue(OrderStatus.Draft)
                   .HasConversion(
                       os => os.ToString(),
                       os => (OrderStatus)Enum.Parse(typeof(OrderStatus), os));

            builder.Property(o => o.TotalPrice).IsRequired();

            builder.HasMany(o => o.OrderItems)
                   .WithOne()
                   .HasForeignKey(oi => oi.OrderId);
        }
    }
}
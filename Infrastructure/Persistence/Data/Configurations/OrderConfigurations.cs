using DomainLayer.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.Property(o => o.subTotal)
                   .HasColumnType("decimal(8,2)");
            //------------------------------     
            builder.HasMany(o => o.items)
                 .WithOne();
            //------------------------------
            builder.HasOne(o => o.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId);
            //------------------------------
            builder.OwnsOne(o => o.shipToAddress);

        }
    }
}

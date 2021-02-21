using FiledPaymentProcessor.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiledPaymentProcessor.Data.Configurations
{
    public class PaymentStatusConfiguration
    {
        public PaymentStatusConfiguration(EntityTypeBuilder<PaymentStatus> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.Property(x => x.Id).IsRequired();
            entity.Property(x => x.State).IsRequired();
            entity.Property(x => x.PaymentDetailId).IsRequired();

            entity.Property(x => x.CreatedOn).IsRequired();
            entity.Property(x => x.UpdatedOn).IsRequired();
        }
    }
}

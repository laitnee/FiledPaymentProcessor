using FiledPaymentProcessor.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiledPaymentProcessor.Data.Configurations
{
    public class PaymentDetailConfiguration
    {
        public PaymentDetailConfiguration(EntityTypeBuilder<PaymentDetail> entity)
        {
            entity.HasKey(x => x.Id);
            entity.HasOne(x => x.Status).WithOne(x => x.PaymentDetail).HasForeignKey<PaymentStatus>(x => x.PaymentDetailId);

            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.ExpirationDate).IsRequired();
            entity.Property(x => x.Amount).IsRequired();
            entity.Property(x => x.CardHolder).IsRequired().HasMaxLength(200);
            entity.Property(x => x.CreditCardNumber).IsRequired().HasMaxLength(50);
            entity.Property(x => x.SecurityCode).HasMaxLength(3);

            entity.Property(x => x.CreatedOn).IsRequired();
            entity.Property(x => x.UpdatedOn).IsRequired();



        }
    }
}

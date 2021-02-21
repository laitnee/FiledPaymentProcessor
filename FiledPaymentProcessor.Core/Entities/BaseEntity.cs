using System;
using System.Collections.Generic;
using System.Text;

namespace FiledPaymentProcessor.Core.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}

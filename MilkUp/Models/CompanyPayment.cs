using MilkUp.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilkUp.Models
{
    public class CompanyPayment : EntityBase
    {
        public int CompanyID { get; set; }
        public Company Company {  get; set; }

        public DateTime DeadlineDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public EPaymentStatus IsPaid { get; set; }
    }
}

using MilkUp.Enums;
using System;

namespace MilkUp.Models
{
    public class CompanyPayment : EntityBase
    {
        public int CompanyID { get; set; }
        public Company Company {  get; set; }

        public DateTime DeadlineDate { get; set; }
        public decimal Price { get; set; }

        public EPaymentStatus IsPaid { get; set; }
    }
}

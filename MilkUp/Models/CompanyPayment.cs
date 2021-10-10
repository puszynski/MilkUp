using MilkUp.Enums;
using System;

namespace MilkUp.Models
{
    public class CompanyPayment : EntityBase
    {
        //todo in future - company payments for yours 'team'
        public int ID { get; set; }

        public int CompanyID { get; set; }
        public Company Company {  get; set; }

        public DateTime DeadlineDate { get; set; }
        public decimal Price { get; set; }

        public EPaymentStatus IsPaid { get; set; }
    }
}

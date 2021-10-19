using MilkUp.Enums;
using MilkUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MilkUp.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Cows.Any())
                return;   // DB has been seeded

            var farm1 = new Farm() { Name = "Puszyczkowo demo", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow, CompanyID = 1 };
            var farm2 = new Farm() { Name = "Gdańsk demo", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow, CompanyID = 1 };

            var farms = new List<Farm>();
            farms.Add(farm1);
            farms.Add(farm2);

            var payment_8_2021 = new CompanyPayment() { Price = 500, CompanyID = 1, DeadlineDate = new DateTime(2021, 8, 10), IsPaid = EPaymentStatus.Paid, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };
            var payment_9_2021 = new CompanyPayment() { Price = 500, CompanyID = 1, DeadlineDate = new DateTime(2021, 9, 10), IsPaid = EPaymentStatus.Paid, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };
            var payment_10_2021 = new CompanyPayment() { Price = 500, CompanyID = 1, DeadlineDate = new DateTime(2021, 10, 10), IsPaid = EPaymentStatus.NotPaid, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };

            var companyPayments = new List<CompanyPayment>();
            companyPayments.Add(payment_8_2021);
            companyPayments.Add(payment_9_2021);
            companyPayments.Add(payment_10_2021);

            var company1 = new Company() { Name = "Firma demo", Farms = farms, CompanyPayments = companyPayments, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };


            var lactation_07_2021 = new Lactation() { CowID = 1, DayOfLactationing = 56, LitersCollected = 20, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };
            var lactation_08_2021 = new Lactation() { CowID = 1, DayOfLactationing = 66, LitersCollected = 21, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };
            var lactation_09_2021 = new Lactation() { CowID = 1, DayOfLactationing = 75, LitersCollected = 20, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };
            var lactation_10_2021 = new Lactation() { CowID = 1, DayOfLactationing = 7, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };

            var lactations = new List<Lactation>();
            lactations.Add(lactation_07_2021);
            lactations.Add(lactation_08_2021);
            lactations.Add(lactation_09_2021);
            lactations.Add(lactation_10_2021);

            var cowGroup1 = new CowGroup() { Name = "Grupa A - dorosłe", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };
            var cowGroup2 = new CowGroup() { Name = "Grupa B - młode", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };


            var cow1 = new Cow() { NameOnFarm = 2001, EarringNumber = 221, TransponderNumber = 123, Farm = farm1, ParentCowID = null, IsMale = false, BirthDate = new DateTime(2020, 01, 27), Lactations = lactations, CowGroupID = 1, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };

            var bull = new Bull() { DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };

            var cow2 = new Cow() { NameOnFarm = 2002, EarringNumber = 222, TransponderNumber = 124, Farm = farm1, ParentCowID = 1, ParentBullID = 1, IsMale = false, BirthDate = new DateTime(2021, 10, 1), Lactations = null, CowGroupID = 2, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };

            var cow3 = new Cow() { NameOnFarm = 2003, EarringNumber = 223, TransponderNumber = 125, Farm = farm1, ParentCowID = 1, ParentBullID = 1, IsMale = true, BirthDate = new DateTime(2021, 10, 1), Lactations = null, CowGroupID = 2, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };
        }
    }
}

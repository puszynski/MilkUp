using Microsoft.AspNetCore.Identity;
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

            foreach (var farm in farms)
                context.Farms.Add(farm);

            var payment_8_2021 = new CompanyPayment() { Price = 500, CompanyID = 1, DeadlineDate = new DateTime(2021, 8, 10), IsPaid = EPaymentStatus.Paid, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };
            var payment_9_2021 = new CompanyPayment() { Price = 500, CompanyID = 1, DeadlineDate = new DateTime(2021, 9, 10), IsPaid = EPaymentStatus.Paid, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };
            var payment_10_2021 = new CompanyPayment() { Price = 500, CompanyID = 1, DeadlineDate = new DateTime(2021, 10, 10), IsPaid = EPaymentStatus.NotPaid, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };

            var companyPayments = new List<CompanyPayment>();
            companyPayments.Add(payment_8_2021);
            companyPayments.Add(payment_9_2021);
            companyPayments.Add(payment_10_2021);

            foreach (var companyPayment in companyPayments)
                context.CompanyPayments.Add(companyPayment);

            var company1 = new Company() { Name = "Firma demo", Farms = farms, CompanyPayments = companyPayments, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };

            context.Companies.Add(company1);

            var lactation_07_2021 = new Lactation() { CowID = 1, DayOfLactationing = 56, LitersCollected = 20, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };
            var lactation_08_2021 = new Lactation() { CowID = 1, DayOfLactationing = 66, LitersCollected = 21, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };
            var lactation_09_2021 = new Lactation() { CowID = 1, DayOfLactationing = 75, LitersCollected = 20, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };
            var lactation_10_2021 = new Lactation() { CowID = 1, DayOfLactationing = 7, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };

            var lactations = new List<Lactation>();
            lactations.Add(lactation_07_2021);
            lactations.Add(lactation_08_2021);
            lactations.Add(lactation_09_2021);
            lactations.Add(lactation_10_2021);

            foreach (var lactation in lactations)
                context.Lactations.Add(lactation);

            var cowGroup1 = new CowGroup() { Name = "Grupa A - dorosłe", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };
            var cowGroup2 = new CowGroup() { Name = "Grupa B - młode", DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };

            context.CowGroups.Add(cowGroup1);
            context.CowGroups.Add(cowGroup2);

            var bull = new Bull() { DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };

            context.Bulls.Add(bull);

            var cow1 = new Cow() { NameOnFarm = 2001, EarringNumber = 221, TransponderNumber = 123, Farm = farm1, ParentCowID = null, IsMale = false, BirthDate = new DateTime(2020, 01, 27), Lactations = lactations, CowGroupID = 1, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };


            var cow2 = new Cow() { NameOnFarm = 2002, EarringNumber = 222, TransponderNumber = 124, Farm = farm1, ParentCowID = 1, ParentBullID = 1, IsMale = false, BirthDate = new DateTime(2021, 10, 1), Lactations = null, CowGroupID = 2, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };

            var cow3 = new Cow() { NameOnFarm = 2003, EarringNumber = 223, TransponderNumber = 125, Farm = farm1, ParentCowID = 1, ParentBullID = 1, IsMale = true, BirthDate = new DateTime(2021, 10, 1), Lactations = null, CowGroupID = 2, DateAdded = DateTime.UtcNow, DateUpdated = DateTime.UtcNow };

            context.Cows.Add(cow1);
            context.Cows.Add(cow2);
            context.Cows.Add(cow3);

            var role1 = new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "49932ed9-9a0a-4ed9-8a63-f0d99b410059" };
            var role2 = new IdentityRole() { Name = "SuperUser", NormalizedName = "SUPERUSER", ConcurrencyStamp = "081709ef-0af9-4301-b4ce-fbf039b92d33" };
            var role3 = new IdentityRole() { Name = "Regular", NormalizedName = "REGULAR", ConcurrencyStamp = "0ab1e61f-3617-4313-82af-1cd23760ceb1" };
            context.Add(role1);
            context.Add(role2);
            context.Add(role3);

            var superUser = new ApplicationUser() { UserName = "puszynski@gmail.com", NormalizedUserName = "PUSZYNSKI@GMAIL.COM", Email = "puszynski@gmail.com", NormalizedEmail = "PUSZYNSKI@GMAIL.COM", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEIZg7ZVp5HeYTYH7jxHf/lV3rjBTIuwlO15U7bTL4qIQt93u0uKXmxz2CQ3c+bJESA==", SecurityStamp = "POFF4BUQ4AB625RRCA7Q5G5WIHCZLRAF", ConcurrencyStamp = "6511aabf-0527-4d9b-9e4e-658b99463f20", LockoutEnabled = true };
            context.Add(superUser);

            context.SaveChanges();
        }
    }
}

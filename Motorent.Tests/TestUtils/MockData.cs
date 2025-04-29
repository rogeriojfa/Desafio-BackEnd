using MotoRent.Domain.Entities;
using MotoRent.Domain.Enums;

namespace MotoRent.Tests.TestUtils
{
    public static class MockData
    {
        public static User CreateMockUser()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                PasswordHash = "Adm@123",
                Name = "Test User",
                Email = "test.user@example.com",
                Role = UserRole.Admin
            };
        }

        public static Rental CreateMockRental()
        {
            return new Rental
            {
                Id = Guid.NewGuid(),
                DeliverymanId = Guid.NewGuid(),
                MotoId = Guid.NewGuid(),
                StartDate = DateTime.UtcNow,
                ExpectedEndDate = DateTime.UtcNow.AddDays(1),
                PlanDays = 1,
                DailyRate = (decimal)100.0
            };
        }

        public static Moto CreateMockMoto()
        {
            return new Moto
            {
                Id = Guid.NewGuid(),
                Year = 2022,
                Model = "Yamaha MT-07",
                Plate = "XYZ5678",
                IsAvailable = true
            };
        }
    }
}

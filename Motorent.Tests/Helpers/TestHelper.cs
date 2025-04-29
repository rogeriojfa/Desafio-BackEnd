using MotoRent.Domain.Enums;
using MotoRent.DTOs;

namespace MotoRent.Tests.Helpers
{
    public static class TestHelper
    {
        public static CreateUserDto CreateTestUser()
        {
            return new CreateUserDto
            {
                Name = "Test User",
                Email = "test.user@example.com",
                Password = "password123",
                Role = UserRole.Deliveryman
            };
        }

        public static CreateMotoRequest CreateTestMoto()
        {
            return new CreateMotoRequest
            {
                Year = 2022,
                Model = "Yamaha MT-07",
                Plate = "XYZ5678"
            };
        }
    }
}

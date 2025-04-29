using Moq;
using FluentAssertions;
using MotoRent.Application.DTOs;
using MotoRent.Application.Interfaces.Service;
using MotoRent.Application.Interfaces;
using MotoRent.Domain.Enums;

namespace MotoRent.Tests.Services
{
    public class DeliverymanServiceTests
    {
        private readonly Mock<IDeliverymanRepository> _repositoryMock;
        private readonly IDeliverymanService _service;

        public DeliverymanServiceTests()
        {
            _repositoryMock = new Mock<IDeliverymanRepository>();
            _service = new DeliverymanService(_repositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateDeliveryman()
        {
            // Arrange
            var dto = new CreateDeliverymanDto
            {
                Name = "João",
                Cnpj = "12345678901234",
                Cnh = "12345678900",
                CnhType = CnhType.A,
                BirthDate = DateTime.UtcNow.AddYears(-25)
            };

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Deliveryman>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllDeliverymen()
        {
            // Arrange
            var deliverymen = new List<Deliveryman>
            {
                new() { Id = Guid.NewGuid(), Name = "Fulano" },
                new() { Id = Guid.NewGuid(), Name = "Ciclano" }
            };

            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(deliverymen);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDeliveryman_WhenFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var deliveryman = new Deliveryman { Id = id, Name = "João" };

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(deliveryman);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateDeliveryman()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existing = new Deliveryman { Id = id, Name = "João" };

            _repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existing);

            var dto = new UpdateDeliverymanDto { Name = "João Atualizado" };

            // Act
            await _service.UpdateAsync(id, dto);

            // Assert
            existing.Name.Should().Be(dto.Name);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Deliveryman>()), Times.Never);
        }
    }
}

using Moq;
using Microsoft.AspNetCore.Mvc;
using MotoRent.Application.DTOs;
using MotoRent.Domain.Entities;
using MotoRent.Application.Interfaces;

namespace MotoRent.Tests
{
    public class MotosControllerTests
    {
        private readonly Mock<IMotoService> _motoServiceMock;
        private readonly Mock<IRentalService> _rentalServiceMock;
        private readonly Mock<IMotoRepository> _motoRepositoryMock;
        private readonly MotosController _controller;

        public MotosControllerTests()
        {
            _motoServiceMock = new Mock<IMotoService>();
            _rentalServiceMock = new Mock<IRentalService>();
            _motoRepositoryMock = new Mock<IMotoRepository>();
            _controller = new MotosController(_motoServiceMock.Object, _motoRepositoryMock.Object, _rentalServiceMock.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnCreatedResult_WhenMotoIsSuccessfullyCreated()
        {
            // Arrange
            var request = new CreateMotoRequest
            {
                Year = 2021,
                Model = "Yamaha",
                Plate = "XYZ1234"
            };

            // Act
            var result = await _controller.Register(request);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var moto = Assert.IsType<Moto>(actionResult.Value);
            Assert.Equal(request.Plate, moto.Plate);
            _motoServiceMock.Verify(service => service.AddAsync(It.IsAny<Moto>()), Times.Once);
        }

        [Fact]
        public async Task GetByLicensePlate_ShouldReturnBadRequest_WhenLicensePlateIsNullOrEmpty()
        {
            // Act
            var result = await _controller.GetByLicensePlate(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("License plate is required.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetByLicensePlate_ShouldReturnOkResult_WhenMotoIsFound()
        {
            // Arrange
            var licensePlate = "XYZ1234";
            var moto = new Moto { Plate = licensePlate, Year = 2021, Model = "Yamaha" };
            _motoRepositoryMock.Setup(repo => repo.GetByLicensePlateAsync(licensePlate.ToUpper()))
                .ReturnsAsync(moto);

            // Act
            var result = await _controller.GetByLicensePlate(licensePlate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(moto, okResult.Value);
        }

        [Fact]
        public async Task GetByLicensePlate_ShouldReturnNotFound_WhenMotoIsNotFound()
        {
            // Arrange
            var licensePlate = "XYZ1234";
            _motoRepositoryMock.Setup(repo => repo.GetByLicensePlateAsync(licensePlate.ToUpper()))
                .ReturnsAsync((Moto)null);

            // Act
            var result = await _controller.GetByLicensePlate(licensePlate);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Moto com placa {licensePlate} não encontrada.", notFoundResult.Value);
        }

        [Fact]
        public async Task CompleteRental_ShouldReturnOkResult_WhenRentalIsCompletedSuccessfully()
        {
            // Arrange
            var rentalId = Guid.NewGuid();
             var returnDate = DateTime.Now;
            var request = new CompleteRentalRequest(returnDate);
            var rentalResponse = new RentalResponse
            {
                Id = rentalId,
                TotalPrice = (decimal?)100.0
            };

            _rentalServiceMock.Setup(service => service.CompleteRentalAsync(rentalId, request.ReturnDate))
                .ReturnsAsync(rentalResponse);

            // Act
            var result = await _controller.CompleteRental(rentalId, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(rentalResponse, okResult.Value);
        }

        [Fact]
        public async Task CompleteRental_ShouldReturnBadRequest_WhenAnErrorOccurs()
        {
            // Arrange
            var rentalId = Guid.NewGuid();
            var request = new CompleteRentalRequest(DateTime.Now);
            var exceptionMessage = "An error occurred during rental completion.";

            _rentalServiceMock.Setup(service => service.CompleteRentalAsync(rentalId, request.ReturnDate))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.CompleteRental(rentalId, request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(exceptionMessage, badRequestResult.Value);
        }
    }
}

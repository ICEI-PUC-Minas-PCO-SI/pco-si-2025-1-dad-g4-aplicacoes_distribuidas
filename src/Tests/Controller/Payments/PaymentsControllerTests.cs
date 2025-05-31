using API.Controllers;
using API.Data;
using Model.Payments;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Controllers
{
    public class PaymentsControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PaymentsTestDb")
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CreatePayment_ShouldAddPaymentAndReturnCreated()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new PaymentsController(context);

            var payment = new Payments
            {
                OrderId = 1,
                Amount = 100.50m,
                PaymentMethod = "Pix"
            };

            // Act
            var result = await controller.CreatePayment(payment);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedPayment = Assert.IsType<Payments>(createdResult.Value);
            Assert.Equal("Pending", returnedPayment.Status);
            Assert.Equal(payment.OrderId, returnedPayment.OrderId);
            Assert.True(returnedPayment.Id > 0);
        }

        [Fact]
        public async Task GetPaymentById_ShouldReturnPaymentIfExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var payment = new Payments
            {
                OrderId = 2,
                Amount = 200m,
                PaymentMethod = "CreditCard",
                Status = "Pending"
            };
            context.Payments.Add(payment);
            await context.SaveChangesAsync();

            var controller = new PaymentsController(context);

            // Act
            var result = await controller.GetPaymentById(payment.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPayment = Assert.IsType<Payments>(okResult.Value);
            Assert.Equal(payment.Id, returnedPayment.Id);
        }

        [Fact]
        public async Task GetPaymentById_ShouldReturnNotFoundIfNotExists()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new PaymentsController(context);

            // Act
            var result = await controller.GetPaymentById(999); // Id inexistente

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}

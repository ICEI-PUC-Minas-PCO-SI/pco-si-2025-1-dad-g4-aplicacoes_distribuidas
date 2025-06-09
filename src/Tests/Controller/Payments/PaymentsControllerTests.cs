using API.Controllers;
using API.Data;
using Model.Payments;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Tests.Controllers
{
    public class PaymentsControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString()) // Isola cada teste
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
            var result = await controller.GetPaymentById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetAllPayments_ShouldReturnAllPayments()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.Payments.Add(new Payments { OrderId = 1, Amount = 50m, PaymentMethod = "Pix", Status = "Pending" });
            context.Payments.Add(new Payments { OrderId = 2, Amount = 75m, PaymentMethod = "Boleto", Status = "Paid" });
            await context.SaveChangesAsync();

            var controller = new PaymentsController(context);

            // Act
            var result = await controller.GetAllPayments();

            // Assert
            var payments = Assert.IsAssignableFrom<System.Collections.Generic.IEnumerable<Payments>>(result.Value);
            Assert.Equal(2, payments.Count());
        }

        [Fact]
        public async Task UpdatePayment_ShouldModifyExistingPayment()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var payment = new Payments { OrderId = 3, Amount = 150m, PaymentMethod = "CreditCard", Status = "Pending" };
            context.Payments.Add(payment);
            await context.SaveChangesAsync();

            var controller = new PaymentsController(context);

            var updatedPayment = new Payments
            {
                Id = payment.Id,
                OrderId = 3,
                Amount = 175m,
                PaymentMethod = "Pix",
                Status = "Paid",
                PaidAt = System.DateTime.UtcNow
            };

            // Act
            var result = await controller.UpdatePayment(payment.Id, updatedPayment);

            // Assert
            Assert.IsType<NoContentResult>(result);

            var modifiedPayment = await context.Payments.FindAsync(payment.Id);
            Assert.Equal(175m, modifiedPayment.Amount);
            Assert.Equal("Pix", modifiedPayment.PaymentMethod);
            Assert.Equal("Paid", modifiedPayment.Status);
        }

        [Fact]
        public async Task DeletePayment_ShouldRemovePayment()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var payment = new Payments { OrderId = 4, Amount = 300m, PaymentMethod = "Boleto", Status = "Pending" };
            context.Payments.Add(payment);
            await context.SaveChangesAsync();

            var controller = new PaymentsController(context);

            // Act
            var result = await controller.DeletePayment(payment.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var deletedPayment = await context.Payments.FindAsync(payment.Id);
            Assert.Null(deletedPayment);
        }

        [Fact]
        public async Task DeletePayment_ShouldReturnNotFoundIfPaymentDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new PaymentsController(context);

            // Act
            var result = await controller.DeletePayment(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}

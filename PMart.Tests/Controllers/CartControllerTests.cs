using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PMart.Controllers;
using PMart.Data;
using PMart.Models;

namespace PMart.Tests.Controllers
{
	[TestClass]
	public class CartControllerTests
	{
		private DbContextOptions<ApplicationDbContext> _options;
		private ApplicationDbContext? _context;
		private Mock<ILogger<CartController>>? _loggerMock;

		[TestInitialize]
		public void Setup()
		{

			_options = new DbContextOptionsBuilder<ApplicationDbContext>()
			   .UseInMemoryDatabase(databaseName: "TestDatabase")
			   .Options;
			_context = new ApplicationDbContext(_options);
			_loggerMock = new Mock<ILogger<CartController>>();



		}

		[TestMethod]
		public void CartBothNull()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new CartController(null!, null!));

		}

		[TestMethod]
		public void CartNullContext()
		{
			if (_loggerMock != null && _context == null)
			{
				Assert.ThrowsException<ArgumentNullException>(() => new CartController(null!, _loggerMock.Object));
			}
		}

		[TestMethod]
		public void CartNullLogger()
		{
			if (_loggerMock == null && _context != null)
			{
				Assert.ThrowsException<ArgumentNullException>(() => new CartController(_context, null!));
			}
		}



		[TestMethod]
		public void CartInitizationValid()
		{
			if (_context != null && _loggerMock != null)
			{
				var controller = new CartController(_context, _loggerMock.Object);
				Assert.IsNotNull(controller);
			}
		}

		[TestMethod]
		public async Task CartAddItemValid()
		{

			var context = new ApplicationDbContext(_options);
			context.SaveChanges();

			var controller = new CartController(context!, _loggerMock!.Object);
			var addItem = new Item("Item2", 5.99, 10);

			var addedItem = await controller.AddItem(addItem);

			var result = addedItem as OkObjectResult;
			Assert.IsNotNull(result);
			var item = result.Value as Item;
			Assert.IsNotNull(item);
			Assert.AreEqual(item.Name, addItem.Name);
			Assert.AreEqual(item.Price, addItem.Price);
			Assert.AreEqual(item.Quantity, addItem.Quantity);
		}


		[TestMethod]
		public async Task CartAddInvalidItem()
		{

			var context = new ApplicationDbContext(_options);
			context.SaveChanges();

			var controller = new CartController(context!, _loggerMock!.Object);

			var error = await controller.AddItem(null!);
			var result = error as BadRequestObjectResult;
			Assert.IsNotNull(result);
			Assert.AreEqual(result.Value, "Item is null");
		}
	}
}
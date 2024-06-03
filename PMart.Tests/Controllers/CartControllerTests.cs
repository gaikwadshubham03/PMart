#region Header
// Author:      Shubham Gaikwad
// Date:        06/01/2024
#endregion

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PMart.Controllers;
using PMart.Data;
using PMart.Models;

namespace PMart.Tests.Controllers
{
    /// <summary>
    /// Unit tests for the <see cref="CartController"/>.
    /// </summary>
    [TestClass]
    public class CartControllerTests
    {
        private DbContextOptions<ApplicationDbContext>? _options;
        private ApplicationDbContext? _context;
        private Mock<ILogger<CartController>>? _loggerMock;

        /// <summary>
        /// Initializes the test setup.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(_options);
            _loggerMock = new Mock<ILogger<CartController>>();
        }

        /// <summary>
        /// Tests that the constructor throws an exception if both context and logger are null.
        /// </summary>
        [TestMethod]
        public void CartBothNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new CartController(null!, null!));
        }

        /// <summary>
        /// Tests that the constructor throws an exception if the context is null.
        /// </summary>
        [TestMethod]
        public void CartNullContext()
        {
            if (_loggerMock != null && _context == null)
            {
                Assert.ThrowsException<ArgumentNullException>(
                    () => new CartController(null!, _loggerMock.Object)
                );
            }
        }

        /// <summary>
        /// Tests that the constructor throws an exception if the logger is null.
        /// </summary>
        [TestMethod]
        public void CartNullLogger()
        {
            if (_loggerMock == null && _context != null)
            {
                Assert.ThrowsException<ArgumentNullException>(
                    () => new CartController(_context, null!)
                );
            }
        }

        /// <summary>
        /// Tests that the constructor initializes successfully with valid parameters.
        /// </summary>
        [TestMethod]
        public void CartInitizationValid()
        {
            if (_context != null && _loggerMock != null)
            {
                var controller = new CartController(_context, _loggerMock.Object);
                Assert.IsNotNull(controller);
            }
        }

        /// <summary>
        /// Tests adding a valid item to the cart.
        /// </summary>
        [TestMethod]
        public async Task CartAddItemValid()
        {
            var context = new ApplicationDbContext(_options!);
            context.SaveChanges();

            var controller = new CartController(context!, _loggerMock!.Object);
            var addItem = new ItemDTO("Item2", 5.99, 10);

            var addedItem = await controller.AddItem(addItem);

            var result = addedItem as OkObjectResult;
            Assert.IsNotNull(result);
            var item = result.Value as ItemDTO;
            Assert.IsNotNull(item);
            Assert.AreEqual(item.Name, addItem.Name);
            Assert.AreEqual(item.Price, addItem.Price);
            Assert.AreEqual(item.Quantity, addItem.Quantity);
        }

        /// <summary>
        /// Tests adding an invalid item (null) to the cart.
        /// </summary>
        [TestMethod]
        public async Task CartAddInvalidItem()
        {
            var context = new ApplicationDbContext(_options!);
            context.SaveChanges();

            var controller = new CartController(context!, _loggerMock!.Object);

            var error = await controller.AddItem(null!);
            var result = error as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Value, "Item is null");
        }

        /// <summary>
        /// Tests retrieving items from an empty cart.
        /// </summary>
        [TestMethod]
        public async Task GetEmptyCardItem()
        {
            var context = new ApplicationDbContext(_options!);
            context.SaveChanges();

            var controller = new CartController(context!, _loggerMock!.Object);

            var list = await controller.GetItems();

            var listItems = (list as OkObjectResult);

            Assert.IsNotNull(listItems);
            var items = (listItems.Value) as List<Item>;
            Assert.IsNotNull(items);
            Assert.AreEqual(items.Count, 0);
        }

        /// <summary>
        /// Integration test for adding an item and then retrieving it from the cart.
        /// </summary>
        [TestMethod]
        public async Task GetCardItem()
        {
            var context = new ApplicationDbContext(_options!);
            context.SaveChanges();

            var controller = new CartController(context!, _loggerMock!.Object);
            var addItem = new ItemDTO("Item1", 5.99, 10);

            var addedItem = await controller.AddItem(addItem);

            var list = await controller.GetItems();

            var listItems = (list as OkObjectResult);

            Assert.IsNotNull(listItems);
            var items = (listItems.Value) as List<Item>;
            Assert.IsNotNull(items);
            var item = items[0];
            Assert.AreEqual(item.Name, addItem.Name);
            Assert.AreEqual(item.Quantity, addItem.Quantity);
            Assert.AreEqual(item.Price, addItem.Price);
        }
    }
}

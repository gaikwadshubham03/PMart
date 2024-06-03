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
    [TestClass]
    public class CartControllerTests
    {
        private DbContextOptions<ApplicationDbContext>? _options;
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
                Assert.ThrowsException<ArgumentNullException>(
                    () => new CartController(null!, _loggerMock.Object)
                );
            }
        }

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

        [TestMethod]
        public async Task GetEmptyCartItem()
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

        [TestMethod]
        public async Task GetSingleCartItem()
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

        [TestMethod]
        public async Task GetMultipleCartItem()
        {
            var context = new ApplicationDbContext(_options!);
            context.SaveChanges();

            var controller = new CartController(context!, _loggerMock!.Object);
            var item1 = new ItemDTO("Item1", 5.99, 10);
            var item2 = new ItemDTO("Item2", 6, 10);
            var item3 = new ItemDTO("Item 3", 7, 10);

            var result1 = await controller.AddItem(item1);
            var result2 = await controller.AddItem(item2);
            var result3 = await controller.AddItem(item3);

            var list = await controller.GetItems();

            var listItems = (list as OkObjectResult);

            Assert.IsNotNull(listItems);
            var items = (listItems.Value) as List<Item>;
            Assert.IsNotNull(items);
            var item = items[2];
            Assert.AreEqual(item.Name, item3.Name);
            Assert.AreEqual(item.Quantity, item3.Quantity);
            Assert.AreEqual(item.Price, item3.Price);
        }
    }
}

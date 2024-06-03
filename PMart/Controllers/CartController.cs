#region Header
// Author:      Shubham Gaikwad
// Date:        06/01/2024
#endregion

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMart.Data;
using PMart.Models;

namespace PMart.Controllers
{
    /// <summary>
    /// Controller for managing cart-related operations.
    /// </summary>
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        #region Non-Public Data Members
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CartController> _logger;
        #endregion

        #region Non-Public Properties/Methods
        #endregion


        #region Public Properties/Methods

        public CartController(ApplicationDbContext context, ILogger<CartController> logger)
        {
            if (logger == null || context == null)
                throw new ArgumentNullException(nameof(logger));

            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new item to the cart.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        /// <returns>A response indicating the result of the operation.</returns>
        [HttpPost]
        [Route("additem")]
        public async Task<IActionResult> AddItem([FromBody] ItemDTO item)
        {
            if (item == null)
            {
                return BadRequest("Item is null");
            }

            var newItem = new Item(item.Name, item.Price, item.Quantity);

            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();
            _logger.LogInformation(
                $"Item added to cart: {newItem.Name}, Quantity: {newItem.Quantity}, Price: {newItem.Price}"
            );
            return Ok(newItem);
        }

        /// <summary>
        /// Gets all items in the cart.
        /// </summary>
        /// <returns>A response with the list of items.</returns>
        [HttpGet]
        [Route("items")]
        public async Task<IActionResult> GetItems()
        {
            _logger.LogInformation("Fetching all items in the cart.");
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }
        #endregion
    }
}

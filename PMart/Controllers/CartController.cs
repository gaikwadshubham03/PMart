// Author:      Shubham Gaikwad
// Date:        06/01/2024

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMart.Data;
using PMart.Models;


namespace PMart.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CartController : ControllerBase
	{
		//private static List<Item> cartItems = new List<Item>();
		private readonly ApplicationDbContext _context;
		private readonly ILogger<CartController> _logger;

		public CartController(ApplicationDbContext context, ILogger<CartController> logger)
		{
			if (logger == null)
				throw new ArgumentNullException(nameof(logger));

			_context = context;
			_logger = logger;
		}


		[HttpPost]
		public async Task<IActionResult> AddItem([FromBody] Item item)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogWarning("Invalid item data received.");
				return BadRequest(ModelState);
			}
			_context.Items.Add(item);
			await _context.SaveChangesAsync();
			_logger.LogInformation($"Item added to cart: {item.Name}, Quantity: {item.Quantity}, Price: {item.Price}");
			return Ok(item);
		}


		[HttpGet]
		public async Task<IActionResult> GetItems()
		{
			_logger.LogInformation("Fetching all items in the cart.");
			var items = await _context.Items.ToListAsync();
			return Ok(items);
		}
	}
}

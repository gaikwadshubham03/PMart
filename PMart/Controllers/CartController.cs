using Microsoft.AspNetCore.Mvc;
using PMart.Models;


namespace PMart.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CartController : ControllerBase
	{
		private static List<Item> cartItems = new List<Item>();
		private readonly ILogger<CartController> _logger;

		public CartController(ILogger<CartController> logger)
		{
			_logger = logger;
		}


		[HttpPost]
		public IActionResult AddItem([FromBody] Item item)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogWarning("Invalid item data received.");
				return BadRequest(ModelState);
			}
			cartItems.Add(item);
			_logger.LogInformation($"Item added to cart: {item.Name}, Quantity: {item.Quantity}, Price: {item.Price}");
			return Ok(item);
		}


		[HttpGet]
		public IActionResult GetItems()
		{
			_logger.LogInformation("Fetching all items in the cart.");
			return Ok(cartItems);
		}
	}
}

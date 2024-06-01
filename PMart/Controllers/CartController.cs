using Microsoft.AspNetCore.Mvc;
using PMart.Models;


namespace PMart.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CartController : ControllerBase
	{
		private static List<Item> cartItems = new List<Item>();

		[HttpPost]
		public IActionResult AddItem([FromBody] Item item)
		{
			if (item == null || item.Quantity <= 0)
			{
				return BadRequest("Invalid item data");
			}
			cartItems.Add(item);
			return Ok(item);
		}


		[HttpGet]
		public IActionResult GetItems()
		{
			return Ok(cartItems);
		}
	}
}

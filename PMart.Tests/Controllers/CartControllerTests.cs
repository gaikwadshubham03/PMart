using PMart.Controllers;

namespace PMart.Tests.Controllers
{
	[TestClass]
	public class CartControllerTests
	{
		[TestMethod]
		public void CartInitializatonNullLogger()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new CartController(null!));

		}

		// Add more testcase
	}
}
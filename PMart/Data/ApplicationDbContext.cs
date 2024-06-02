using Microsoft.EntityFrameworkCore;
using PMart.Models;

namespace PMart.Data
{
	public class ApplicationDbContext : DbContext
	{
		// it is like a abstraction layer which interacts with database to perform any operations
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Item> Items { get; set; }

	}
}

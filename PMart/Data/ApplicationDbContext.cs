using Microsoft.EntityFrameworkCore;
using PMart.Models;

namespace PMart.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Item> Items { get; set; }

	}
}

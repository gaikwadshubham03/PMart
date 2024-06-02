using Microsoft.EntityFrameworkCore;
using PMart.Models;

namespace PMart.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<ItemDTO> Items { get; set; }

	}
}

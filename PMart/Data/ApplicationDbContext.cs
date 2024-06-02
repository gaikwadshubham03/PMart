#region Header
// Author:      Shubham Gaikwad
// Date:        06/01/2024
#endregion

using Microsoft.EntityFrameworkCore;
using PMart.Models;

namespace PMart.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }


		// represent database table for ItemDTO class
		public DbSet<Item> Items { get; set; }
	}
}

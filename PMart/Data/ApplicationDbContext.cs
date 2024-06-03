#region Header
// Author:      Shubham Gaikwad
// Date:        06/01/2024
#endregion

using Microsoft.EntityFrameworkCore;
using PMart.Models;

namespace PMart.Data
{
	/// <summary>
    /// Represents the database context for the PMart application.
    /// </summary>
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }

		/// <summary>
        /// Represents Item table in database
        /// </summary>
		public DbSet<Item> Items { get; set; }
	}
}

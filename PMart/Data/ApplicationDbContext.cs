#region Header
// Author:      Shubham Gaikwad
// Date:        06/01/2024
#endregion

using Microsoft.EntityFrameworkCore;
using PMart.Models;

namespace PMart.Data
{
    // Represents the database context for the PMart application.
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Represents Item table in database
        public DbSet<Item> Items { get; set; }
    }
}

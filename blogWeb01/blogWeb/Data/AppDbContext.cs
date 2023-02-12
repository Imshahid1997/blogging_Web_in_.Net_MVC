using blogWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace blogWeb.Data
{
	public class AppDbContext:DbContext
	{
		

		public AppDbContext(DbContextOptions options):base(options)
		{

		}
		public DbSet<Post> Tbl_Post { get; set; }
		public DbSet<Profile> Tbl_Profile { get; set; }
	}
}

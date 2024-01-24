global using Microsoft.EntityFrameworkCore;
using MinmalUserAPI.Moduels;

namespace MinmalUserAPI.Data
{
	public class DataContext: DbContext
	{

        public DbSet<Users> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
		{ 
		
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer("");
		}
	}
}


using Microsoft.EntityFrameworkCore;
using Progra_Api.Models;

namespace Progra_Api.Data
{
	public class PrograDBContext: DbContext
	{
		public PrograDBContext(DbContextOptions <PrograDBContext> options)
			: base(options)
		{

		}

		public DbSet<Language> Languages { get; set; }
	}
}

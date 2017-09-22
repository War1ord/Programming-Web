using Report_Manager.WebSite.Models;
using System.Data.Entity;

namespace Report_Manager.WebSite.Data
{
	public class DataContext : DbContext
	{
		public DbSet<DatabaseConnection> DatabaseConnections { get; set; }
		public DbSet<Report> Reports { get; set; }
		public DbSet<ReportHistory> ReportHistory { get; set; }
		public DbSet<ReportSchedule> ReportSchedules { get; set; }
		public DbSet<ReportUserLink> ReportUserLinks { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
	}
}
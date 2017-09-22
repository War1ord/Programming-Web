using Report_Manager.WebSite.Data.Models;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;

namespace Report_Manager.WebSite.Data
{
	public class DataContext : DbContext
	{
		public static DataContext I { get { return new DataContext(); } }

		public DataContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString) { }
		public DataContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
		public DataContext(DbConnection connection, bool contextOwnsConnection) : base(connection, contextOwnsConnection) { }

		public void Initialize() { using (var db = I) { new MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>().InitializeDatabase(db); } }

		public DbSet<DatabaseConnection> DatabaseConnections { get; set; }
		public DbSet<Report> Reports { get; set; }
		public DbSet<ReportHistory> ReportHistory { get; set; }
		public DbSet<ReportSchedule> ReportSchedules { get; set; }
		public DbSet<ReportUserLink> ReportUserLinks { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<Error> Errors { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasRequired(u => u.CreatedBy).WithMany();
			modelBuilder.Entity<User>().HasOptional(u => u.ModifiedBy).WithOptionalDependent();
		}
	}
}
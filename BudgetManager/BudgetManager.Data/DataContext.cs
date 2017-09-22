using System.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using BudgetManager.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BudgetManager.Models.Static;
using BudgetManager.Models.User;

namespace BudgetManager.Data
{
	/// <summary>
	/// The DataContext Class containing the data access layer
	/// </summary>
	public class DataContext : DbContext
	{
		#region Properties

		/// <summary>
		/// Gets the connection string.
		/// </summary>
		/// <value>
		/// The connection string.
		/// </value>
		/// <exception cref="System.Exception">Cannot resolve connection settings to the database.</exception>
		public static string ConnectionString
		{
			get
			{
				ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
				if (settings == null)
				{
					throw new Exception("Cannot resolve connection settings to the database.");
				}
				return settings.ConnectionString;
			}
		}

		#endregion

		#region Contructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DataContext"/> class.
		/// </summary>
		public DataContext()
		{
			Configuration.LazyLoadingEnabled = false;
			Configuration.ProxyCreationEnabled = false;
			//get connection string
			Database.Connection.ConnectionString = ConnectionString;
			((IObjectContextAdapter) this).ObjectContext.CommandTimeout = 180;
		}

		#endregion

		#region Methods

		public static void Initialize()
		{
            using (var datacontext = new Data.DataContext())
            {
                var initializer = new Data.DatabaseInitializer<DataContext>(new IndexInitializer<DataContext>(), new MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>());
                initializer.InitializeDatabase(datacontext);
                try
                {
                }
                catch
                {
                    /* ignore, no need to break the db creation because of the system user*/
                }
            }
        }

		#endregion

		#region Overrides

		/// <summary>
		/// This method is called when the model for a derived context has been initialized, but
		/// before the model has been locked down and used to initialize the context.  The default
		/// implementation of this method does nothing, but it can be overridden in a derived class
		/// such that the model can be further configured before it is locked down.
		/// </summary>
		/// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
		/// <remarks>
		/// Typically, this method is called only once when the first instance of a derived context
		/// is created.  The model for that context is then cached and is for all further instances of
		/// the context in the app domain.  This caching can be disabled by setting the ModelCaching
		/// property on the given ModelBuidler, but note that this can seriously degrade performance.
		/// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
		/// classes directly.
		/// </remarks>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
			base.OnModelCreating(modelBuilder);
		}

		#endregion

		#region Entities

		/// <summary>
		/// Gets or sets the bank transactions.
		/// </summary>
		/// <value>
		/// The bank transactions.
		/// </value>
		public DbSet<BankTransaction> BankTransactions { get; set; }
		/// <summary>
		/// Gets or sets the bank transaction groups.
		/// </summary>
		/// <value>
		/// The bank transaction groups.
		/// </value>
		public DbSet<BankTransactionGroup> BankTransactionGroups { get; set; }
		/// <summary>
		/// Gets or sets the bank transaction rules.
		/// </summary>
		/// <value>
		/// The bank transaction rules.
		/// </value>
		public DbSet<BankTransactionRule> BankTransactionRules { get; set; }
		/// <summary>
		/// Gets or sets the budget template item rule links.
		/// </summary>
		/// <value>
		/// The budget template item rule links.
		/// </value>
		public DbSet<BudgetTemplateItemRuleLink> BudgetTemplateItemRuleLinks { get; set; }
		/// <summary>
		/// Gets or sets the budget template items.
		/// </summary>
		/// <value>
		/// The budget template items.
		/// </value>
		public DbSet<BudgetTemplateItem> BudgetTemplateItems { get; set; }
		/// <summary>
		/// Gets or sets the budget row items.
		/// </summary>
		/// <value>
		/// The budget row items.
		/// </value>
		public DbSet<BudgetRowItem> BudgetRowItems { get; set; }
		/// <summary>
		/// Gets or sets the budget type dates.
		/// </summary>
		/// <value>
		/// The budget type dates.
		/// </value>
		public DbSet<BudgetTypeDate> BudgetTypeDates { get; set; }
		/// <summary>
		/// Gets or sets the bank accounts.
		/// </summary>
		/// <value>
		/// The bank accounts.
		/// </value>
		public DbSet<BankAccount> BankAccounts { get; set; }
		/// <summary>
		/// Gets or sets the banks.
		/// </summary>
		/// <value>
		/// The banks.
		/// </value>
		public DbSet<Bank> Banks { get; set; }
		/// <summary>
		/// Gets or sets the users.
		/// </summary>
		/// <value>
		/// The users.
		/// </value>
		public DbSet<User> Users { get; set; }
		/// <summary>
		/// Gets or sets the errors.
		/// </summary>
		/// <value>
		/// The errors.
		/// </value>
		public DbSet<Error> Errors { get; set; }

		#endregion
	}
}
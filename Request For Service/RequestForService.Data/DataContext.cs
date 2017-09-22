using RequestForService.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace RequestForService.Data
{
	public class DataContext : DbContext
	{
		public DataContext() : base("DefaultConnection") { }
		public static void Initialize()
		{
			using (var db = new DataContext())
			{
				new MigrateDatabaseToLatestVersion<DataContext, Configuration>().InitializeDatabase(db);
			}
		}
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			ConfigureModels<Models.Base.ModelBase>(modelBuilder);
			base.OnModelCreating(modelBuilder);
		}

		/// <summary>
		/// Configures the models.
		/// </summary>
		/// <typeparam name="T">Configures models based on type</typeparam>
		/// <param name="modelBuilder">The model builder.</param>
        public void ConfigureModels<T>(DbModelBuilder modelBuilder) where T : Models.Base.ModelBase 
		{
			// Remove Cascade Delete Convention
			modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.ManyToManyCascadeDeleteConvention>();

			// custom fluent api for users under a business entity
			modelBuilder.Entity<Models.Users.User>()
				.HasOptional(user => user.BusinessEntity)
				.WithMany(businessEntity => businessEntity.Users)
				.HasForeignKey(user => user.BusinessEntityId);

			// build entities 
			Assembly assembly = Assembly.GetAssembly(typeof(T));
			var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");
            // get types to build entities for
			var types = assembly.GetTypes()
				.Where(type => 
					!type.IsAbstract && type.IsClass
					&& (type.BaseType == typeof(T)// root level
						|| (type.BaseType != null && type.BaseType.BaseType == typeof(T))// level 1
						|| (type.BaseType != null && type.BaseType.BaseType != null && type.BaseType.BaseType.BaseType == typeof(T))// level 2
						|| (type.BaseType != null && type.BaseType.BaseType != null && type.BaseType.BaseType.BaseType != null && type.BaseType.BaseType.BaseType.BaseType == typeof(T))// level 3
					)
				)
				.OrderBy(type => type.Name)
				.ToList();
            //Process Generic Types
			var list = types
				.Select(type => !type.ContainsGenericParameters 
					? type : type.MakeGenericType(type.GetGenericArguments()))
				.ToList();
            //build the entities from list of types
			var results = list
				.Select(type =>
				{
					var typeArguments = !type.ContainsGenericParameters ? type : type.MakeGenericType(type);
					return new
					{
						ReturnValue = entityMethod.MakeGenericMethod(typeArguments).Invoke(modelBuilder, new object[] { }),
						typeArguments.Namespace,
						typeArguments.Name,
						typeArguments.FullName,
						typeArguments.AssemblyQualifiedName
					};
				}).ToList();
		}

		public new DbSet<TEntity> Set<TEntity>() where TEntity : Models.Base.IdBase
		{
			return base.Set<TEntity>();
		}

		public override DbSet Set(Type entityType)
		{
			return base.Set(entityType);
		}

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}

		public override Task<int> SaveChangesAsync()
		{
			return base.SaveChangesAsync();
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			return base.SaveChangesAsync(cancellationToken);
		}

		protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
		{
			return base.ValidateEntity(entityEntry, items);
		}

		protected override bool ShouldValidateEntity(DbEntityEntry entityEntry)
		{
			return base.ShouldValidateEntity(entityEntry);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

	}
}
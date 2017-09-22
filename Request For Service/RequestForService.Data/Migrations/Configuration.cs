using System.Data.Entity.Migrations;

namespace RequestForService.Data.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
	        AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DataContext db)
        {
        }
    }
}

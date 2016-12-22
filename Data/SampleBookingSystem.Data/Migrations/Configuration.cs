namespace SampleBookingSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<SampleBookingSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "SampleBookingSystem.Models.SampleBookingSystemDbContext";
        }

        protected override void Seed(SampleBookingSystemDbContext context)
        {
            DbSeeder.SeedUsers(context);
            DbSeeder.SeedRooms(context);
            DbSeeder.SeedReservations(context);
        }
    }
}

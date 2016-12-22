using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using SampleBookingSystem.Data.Common.Models;
using SampleBookingSystem.Data.Models;

namespace SampleBookingSystem.Data
{
    public class SampleBookingSystemDbContext : IdentityDbContext<User>
    {
        public SampleBookingSystemDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Room> Rooms { get; set; }

        public IDbSet<Reservation> Reservations { get; set; }

        public static SampleBookingSystemDbContext Create()
        {
            return new SampleBookingSystemDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries() // Get all entries from Entity Framework, not saved to DB
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {

                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}

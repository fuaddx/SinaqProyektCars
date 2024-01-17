using Cars.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Cars.Contexts
{
    public class DataDbContext : IdentityDbContext
    {
        public DataDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Slider>Sliders { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<EntityEntry<Slider>> entries = ChangeTracker.Entries<Slider>();
            foreach (EntityEntry<Slider> entry in entries)
            {
                if(entry.State ==EntityState.Added)
                {
                    DateTime dateTime = DateTime.UtcNow;
                    DateTime AzTime = dateTime.AddHours(4);
                    entry.Entity.CreatedTime = AzTime;
                    entry.Entity.UpdatedTime = null;
                }else if(entry.State == EntityState.Modified)
                {
                    DateTime dateTime = DateTime.UtcNow;
                    DateTime AzTime = dateTime.AddHours(4);
                    entry.Entity.UpdatedTime = AzTime;

                    var modifiedProp = entry.Properties.Where(prop => prop.IsModified && !prop.Metadata.IsPrimaryKey());
                    if (!modifiedProp.Any())
                    {
                        entry.Entity.UpdatedTime = null;
                    }
                }
            }
           return base.SaveChangesAsync(cancellationToken);
        }
    }
}

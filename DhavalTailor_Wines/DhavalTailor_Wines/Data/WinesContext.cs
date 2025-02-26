using DhavalTailor_Wines.Models;
using Microsoft.EntityFrameworkCore;

namespace DhavalTailor_Wines.Data
{
    public class WinesContext : DbContext
    {

        public WinesContext(DbContextOptions<WinesContext> options)
           : base(options)
        {
        }

        //check for names in model,its
        public DbSet<Wine> Wines { get; set; }
        public DbSet<Wine_Type> Wine_Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Deleting a Wine_Type and related Wine will also be deleted
            modelBuilder.Entity<Wine_Type>()
                .HasMany(wt => wt.Wines)
                .WithOne(w => w.Wine_Type)
                .HasForeignKey(w => w.Wine_TypeID)
                .OnDelete(DeleteBehavior.Cascade);

            //Deleting Wine record won't delete Wine_Type
            modelBuilder.Entity<Wine>()
               .HasOne(w => w.Wine_Type)
               .WithMany(wt => wt.Wines)
               .HasForeignKey(w => w.Wine_TypeID)
               .OnDelete(DeleteBehavior.Cascade);

            // Unique constraint so no two sine can be the same
            modelBuilder.Entity<Wine>()
            .HasIndex(w => new { w.WineName, w.WineYear })
            .IsUnique();

            //need a constraint not to repeat the wine type
            modelBuilder.Entity<Wine_Type>()
            .HasIndex(w => new { w.WineTypeName })
            .IsUnique();
        }

            //Auditing

            //CODE ADDED DURING AUDIT
            //To give access to IHttpContextAccessor for Audit Data with IAuditable
            private readonly IHttpContextAccessor _httpContextAccessor;

            //Property to hold the UserName value
            public string UserName
            {
                get; private set;
            }

            public WinesContext(DbContextOptions<WinesContext> options, IHttpContextAccessor httpContextAccessor)
                : base(options)
            {
                _httpContextAccessor = httpContextAccessor;
                UserName = _httpContextAccessor.HttpContext?.User.Identity.Name;
                UserName = UserName ?? "Unknown";
            }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditable trackable)
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;

                        case EntityState.Added:
                            trackable.CreatedOn = now;
                            trackable.CreatedBy = UserName;
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;
                    }
                }
            }
        }


    }

}

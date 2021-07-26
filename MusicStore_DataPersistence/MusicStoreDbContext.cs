using Microsoft.EntityFrameworkCore;
using MusicStore_Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MusicStore_Common.DBInterfaces;
using MusicStore_DataPersistence.EntityConfigurations;

namespace MusicStore_DataPersistence
{
    public class MusicStoreDbContext : DbContext
    {

        public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            AddTimestamps(DateTime.UtcNow);
            SoftDelete();

            return base.SaveChangesAsync(cancellationToken);
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var deletedRelationships = modelBuilder.Model.GetEntityTypes()
                .Where(x => typeof(ISoftDelete).IsAssignableFrom(x.ClrType));
            foreach (var relationship in deletedRelationships)
            {
                modelBuilder.AddIsDeletedColumnFilter(relationship.ClrType);
                modelBuilder.SetSoftDeleteFilter(relationship.ClrType);
            }

        }

        private void AddTimestamps(DateTime currentDate)
        {

            var entities = ChangeTracker.Entries().Where(item =>
                item.Entity is ICreatedDateStamp || item.Entity is IModifiedDateStamp &&
                (item.State == EntityState.Added || item.State == EntityState.Modified));

            foreach (var entity in entities)
            {

                if (entity.State == EntityState.Added || entity.State == EntityState.Modified)
                {
                    if (entity.Entity is IModifiedDateStamp modifiedDateStamp)
                    {
                        modifiedDateStamp.ModifiedDate = currentDate;
                    }
                }

                if (entity.State == EntityState.Added)
                {
                    if (entity.Entity is ICreatedDateStamp createdDateStamp)
                    {
                        createdDateStamp.CreatedDate = currentDate;
                    }
                }
            
            }

        }

        private void SoftDelete()
        {
            var entities = ChangeTracker.Entries()
                .Where(item => item.Entity is ISoftDelete && item.State == EntityState.Deleted);

            foreach (var entity in entities)
            {
                entity.State = EntityState.Modified;
                entity.CurrentValues[BulkConfigurationExtensions.IsDeletedColumnName] = true;
            }
        }

    }

}

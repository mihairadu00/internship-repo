using Microsoft.EntityFrameworkCore;
using MusicStore_Common.DBInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MusicStore_DataPersistence.EntityConfigurations
{
    public static class BulkConfigurationExtensions
    {

        public const string IsDeletedColumnName = "IsDeleted";

        public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            SetSoftDeleteFilterMethod.MakeGenericMethod(entityType).Invoke(null, new object[] { modelBuilder });
        }

        private static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(BulkConfigurationExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == nameof(SetSoftDeleteFilter));

        public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class, ISoftDelete
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(item =>
                !EF.Property<bool?>(item, IsDeletedColumnName).HasValue ||
                EF.Property<bool?>(item, IsDeletedColumnName) == false);
        }

        public static void AddIsDeletedColumnFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            AddIsDeletedColumnFilterMethod.MakeGenericMethod(entityType).Invoke(null, new object[] { modelBuilder });
        }

        private static readonly MethodInfo AddIsDeletedColumnFilterMethod = typeof(BulkConfigurationExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == nameof(AddIsDeletedColumnFilter));

        public static void AddIsDeletedColumnFilter<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class, ISoftDelete
        {
            modelBuilder.Entity<TEntity>().Property<bool?>(IsDeletedColumnName);
        }

    }
}

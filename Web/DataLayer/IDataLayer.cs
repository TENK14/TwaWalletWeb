using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TwaWallet.Model;

namespace TwaWallet.Web.DataLayer
{
    public interface IDataLayer
    {
        bool UserCategoryExists(ApplicationUser user, string id);
        bool UserPaymentTypeExists(ApplicationUser user, string id);
        bool UserRecordExists(ApplicationUser user, string id);

        Category GetUserCategory(ApplicationUser user, string id);
        PaymentType GetUserPaymentType(ApplicationUser user, string id);
        Record GetUserRecord(ApplicationUser user, string id);

        IQueryable<Category> GetUserCategories(ApplicationUser user);
        IQueryable<PaymentType> GetUserPaymentTypes(ApplicationUser user);
        IQueryable<Record> GetUserRecords(ApplicationUser user);
        IQueryable<RecurringPayment> GetUserRecurringPayments(ApplicationUser user);
        RecurringPayment GetUserRecurringPayment(ApplicationUser user, string id);

        IQueryable<Interval> GetIntervals();

        void Load<TEntity, TProperty>(IEnumerable<TEntity> entitiesToLoad, Expression<Func<TEntity, TProperty>> propertyPath)
            where TEntity : class
            where TProperty : class;

        void Load<TEntity, TProperty>(TEntity entityToLoad, Expression<Func<TEntity, TProperty>> propertyPath)
            where TEntity : class
            where TProperty : class;

        Task<TEntity> AddAsync<TEntity>(TEntity entity)
            where TEntity : class;

        Task<TEntity[]> AddRangeAsync<TEntity>(TEntity[] entities)
            where TEntity : class;

        Task<TEntity> UpdateAsync<TEntity>(TEntity entity)
            where TEntity : class;

        Task<TEntity> DeleteAsync<TEntity>(TEntity entity)
            where TEntity : class;

        bool UserRecurringPaymentExists(ApplicationUser user, string id);
    }
}
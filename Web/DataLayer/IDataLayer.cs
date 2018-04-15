using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TwaWallet.Model;

namespace TwaWallet.Web.DataLayer
{
    public interface IDataLayer
    {
        bool UserCategoryExists(string id);
        bool UserPaymentTypeExists(string id);
        bool UserRecordExists(string id);

        Category GetUserCategory(string id);
        PaymentType GetUserPaymentType(string id);
        Record GetUserRecord(string id);

        IQueryable<Category> GetUserCategories();
        IQueryable<PaymentType> GetUserPaymentTypes();
        IQueryable<Record> GetUserRecords();

        void Load<TEntity, TProperty>(IEnumerable<TEntity> entitiesToLoad, Expression<Func<TEntity, TProperty>> propertyPath)
            where TEntity : class
            where TProperty : class;
    }
}
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using TwaWallet.Entity;
using TwaWallet.Model;

namespace TwaWallet.Web.DataLayer
{
    /**/
    // TODO: vymysli, jak tu dostat nejak rozumne Usera.
    public class DataLayer : IDataLayer
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;


        public DataLayer(ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
             UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
            this._userManager = userManager;
        }
        public bool UserCategoryExists(string id)
        {
            return _context.Categories.Include(c => c.ApplicationUser).Any(e => e.Id == id);        
        }

        public bool UserPaymentTypeExists(string id)
        {
            return _context.PaymentTypes.Include(c => c.ApplicationUser).Any(e => e.Id == id);
        }

        public bool UserRecordExists(string id)
        {
            return _context.Records.Include(c => c.ApplicationUser).Any(e => e.Id == id);
        }

        public Category GetUserCategory(string id)
        {
            var user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;

            return _context.Categories.Include(c => c.ApplicationUser).SingleOrDefault(m => m.Id == id && m.ApplicationUser == user);
        }

        public PaymentType GetUserPaymentType(string id)
        {
            var user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;

            return _context.PaymentTypes.Include(c => c.ApplicationUser).SingleOrDefault(m => m.Id == id && m.ApplicationUser == user);
        }

        public Record GetUserRecord(string id)
        {
            var user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;

            return _context.Records.Include(c => c.ApplicationUser).SingleOrDefault(m => m.Id == id && m.ApplicationUser == user);
        }

        public IQueryable<Category> GetUserCategories()
        {
            var user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;

            var result = _context.Categories.Include(c => c.ApplicationUser).Where(c => c.ApplicationUser == user);

            return result;
        }

        public IQueryable<PaymentType> GetUserPaymentTypes()
        {
            var user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;

            var records = _context.PaymentTypes.Include(c => c.ApplicationUser).Where(c => c.ApplicationUser == user);

            return records;
        }

        public IQueryable<Record> GetUserRecords()
        {
            var user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;

            var records = _context.Records.Include(c => c.ApplicationUser).Where(c => c.ApplicationUser == user);

            return records;
        }
        
        //public IQueryable<Category> GetUserCategories()
        //{
        //    var user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;

        //    var categories = _context.Categories.Include(c => c.ApplicationUser).Where(c => c.ApplicationUser == user);

        //    //foreach (var c in categories)
        //    //{
        //    //    _context.Entry(c).Reference(cc => cc.ApplicationUser).Load();
        //    //}

        //    return categories;
        //}

        //private void LoadInternal<TEntity, TProperty>(IEnumerable<TEntity> entitiesToLoad, Expression<Func<TEntity, TProperty>> propertyPath) where TEntity : class where TProperty : class
        public void Load<TEntity, TProperty>(IEnumerable<TEntity> entitiesToLoad, Expression<Func<TEntity, TProperty>> propertyPath) where TEntity : class where TProperty : class
        {
            foreach (var e in entitiesToLoad)
            {
                _context.Entry(e).Reference(propertyPath).Load();
            }
        }


        #region Havit.DataLayer
            /*
            PropertyLoadSequenceResolver propertyLoadSequenceResolver = new PropertyLoadSequenceResolver();

            //public void Load<T,X>(T t, System.Linq.Expressions.Expression<Func<T,X> exp)
            public void Load<T, X>(T t, System.Linq.Expressions.Expression<Func<T, X> exp)
            {
                _context.Entry(t).Reference().Load();
            }

            public void Load<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] propertyPaths) where TEntity : class
            {
                Contract.Requires(propertyPaths != null, "propertyPaths != null");
                foreach (Expression<Func<TEntity, object>> propertyPath in propertyPaths)
                    this.LoadInternal<TEntity, object>((IEnumerable<TEntity>)new TEntity[1]
                    {
              entity
                    }, propertyPath);
            }

            private void LoadInternal<TEntity, TProperty>(IEnumerable<TEntity> entitiesToLoad, Expression<Func<TEntity, TProperty>> propertyPath) where TEntity : class where TProperty : class
            {
                TEntity[] array1 = entitiesToLoad.Where<TEntity>((Func<TEntity, bool>)(entity => (object)entity != null)).ToArray<TEntity>();
                if (array1.Length == 0)
                    return;
                Contract.Assert<InvalidOperationException>(((IEnumerable<TEntity>)array1).All<TEntity>((Func<TEntity, bool>)(item => this.dbContext.GetEntityState<TEntity>(item) != EntityState.Detached)), "DbDataLoader can be used only for objects tracked by a change tracker.");
                PropertyToLoad[] propertiesToLoad = this.propertyLoadSequenceResolver.GetPropertiesToLoad<TEntity, TProperty>(propertyPath);
                Array array2 = (Array)array1;
                foreach (PropertyToLoad propertyToLoad in propertiesToLoad)
                {
                    if (!propertyToLoad.IsCollection)
                        //array2 = (Array)typeof(DbDataLoader).GetMethod("LoadReferencePropertyInternal", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(propertyToLoad.SourceType, propertyToLoad.TargetType).Invoke((object)this, new object[2]
                        array2 = (Array)typeof(DbDataLoader).GetMethod("LoadReferencePropertyInternal", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(propertyToLoad.SourceType, propertyToLoad.TargetType).Invoke((object)this, new object[2]
                        {
                (object) propertyToLoad.PropertyName,
                (object) array2
                        });
                    else
                        array2 = (Array)typeof(DbDataLoader).GetMethod("LoadCollectionPropertyInternal", BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(propertyToLoad.SourceType, propertyToLoad.TargetType, propertyToLoad.CollectionItemType).Invoke((object)this, new object[2]
                        {
                (object) propertyToLoad.PropertyName,
                (object) array2
                        });
                    if (array2.Length == 0)
                        break;
                }
            }

            private TProperty[] LoadReferencePropertyInternal<TEntity, TProperty>(string propertyName, TEntity[] entities) where TEntity : class where TProperty : class
            {
                PropertyLambdaExpression<TEntity, TProperty> propertyLambdaExpression = this.lambdaExpressionManager.GetPropertyLambdaExpression<TEntity, TProperty>(propertyName);
                List<int> idsToLoadProperty = this.GetEntitiesIdsToLoadProperty<TEntity>(entities, propertyName, false);
                if (idsToLoadProperty.Count > 0)
                    this.GetLoadQuery<TEntity, TProperty>(propertyLambdaExpression.LambdaExpression, idsToLoadProperty, false).Load();
                return ((IEnumerable<TEntity>)entities).Select<TEntity, TProperty>((Func<TEntity, TProperty>)(item => propertyLambdaExpression.LambdaCompiled(item))).Where<TProperty>((Func<TProperty, bool>)(item => (object)item != null)).ToArray<TProperty>();
            }
            */
            #endregion
    } // DataLayer

    #region Havit.DataLayer
    /**
    /// <summary>Poskytuje seznam vlastností k načtení.</summary>
    public class PropertyLoadSequenceResolver //: IPropertyLoadSequenceResolver
    {
        /// <summary>
        /// Vrací z expression tree seznam vlastností, které mají být DataLoaderem načteny.
        /// </summary>
        public virtual PropertyToLoad[] GetPropertiesToLoad<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> propertyPath) where TEntity : class
        {
            return new PropertiesSequenceExpressionVisitor().GetPropertiesToLoad<TEntity, TProperty>(propertyPath);
        }
    }

    public class PropertyToLoad
    {
        public string PropertyName { get; set; }

        public Type SourceType { get; set; }

        public Type TargetType { get; set; }

        public Type CollectionItemType { get; set; }

        public bool IsCollection
        {
            get
            {
                return this.CollectionItemType != (Type)null;
            }
        }
    }

    internal class PropertiesSequenceExpressionVisitor : ExpressionVisitor
    {
        private string propertyPathString;
        private List<PropertyToLoad> propertiesToLoad;

        public PropertyToLoad[] GetPropertiesToLoad<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> propertyPath) where TEntity : class
        {
            Contract.Requires(propertyPath != null, "propertyPath != null");
            this.propertyPathString = propertyPath.ToString();
            this.propertiesToLoad = new List<PropertyToLoad>();
            this.Visit((Expression)propertyPath);
            return this.propertiesToLoad.ToArray();
        }

        public override Expression Visit(Expression node)
        {
            if (node == null)
                return (Expression)null;
            switch (node.NodeType)
            {
                case ExpressionType.MemberAccess:
                case ExpressionType.Parameter:
                case ExpressionType.Call:
                case ExpressionType.Lambda:
                    return base.Visit(node);
                default:
                    throw new NotSupportedException(string.Format("There is unsupported node \"{0}\" in the expression \"{1}\".", (object)node.NodeType, (object)this.propertyPathString));
            }
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (!(node.Method.Name == "Select") || node.Arguments.Count != 2)
                throw new NotSupportedException(string.Format("There is an unsupported method call \"{0}\" in the expression \"{1}\".", (object)node.Method.Name, (object)this.propertyPathString));
            this.Visit(node.Arguments[0]);
            this.Visit(node.Arguments[1]);
            return (Expression)node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Expression expression = base.VisitMember(node);
            if (node.NodeType != ExpressionType.MemberAccess)
                return expression;
            Type type = ((IEnumerable<Type>)((PropertyInfo)node.Member).PropertyType.GetInterfaces()).FirstOrDefault<Type>((Func<Type, bool>)(item =>
            {
                if (item.IsGenericType)
                    return item.GetGenericTypeDefinition() == typeof(IEnumerable<>);
                return false;
            }));
            if (type != (Type)null)
            {
                this.propertiesToLoad.Add(new PropertyToLoad()
                {
                    SourceType = node.Member.DeclaringType,
                    PropertyName = node.Member.Name,
                    TargetType = ((PropertyInfo)node.Member).PropertyType,
                    CollectionItemType = type.GetGenericArguments()[0]
                });
                return expression;
            }
            this.propertiesToLoad.Add(new PropertyToLoad()
            {
                SourceType = node.Member.DeclaringType,
                TargetType = ((PropertyInfo)node.Member).PropertyType,
                PropertyName = node.Member.Name
            });
            return expression;
        }
    } 
    **/
    #endregion
}

using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using APP.LMM.Common;

namespace APP.LMM.DomainObject
{
    public abstract class BaseDomainObject<T> where T : class, new()
    {
        #region implement DBContext class
        protected internal class DomainDBContext : DbContext
        {
            #region properties
            protected internal DbSet<T> dbSet { get; set; }
            #endregion

            #region constructor
            protected internal DomainDBContext() : base(CommonFunction.LMM_Connstring)
            {
                //set context as type of entity
                dbSet = this.Set<T>();

                //set timeout on execute command
                //(this as IObjectContextAdapter).ObjectContext.CommandTimeout = 300;
            }
            #endregion

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                //prevent pluralizing table name [ex: product to be : products]
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

                //set initializer to do nothing [ex: create databes if not exists, drop create database when model changes, drop create database always]
                Database.SetInitializer<DomainDBContext>(null);

                //need to register entity table names, because the entities inside different assembly
                modelBuilder.Entity<T>().ToTable(typeof(T).Name);
            }
        }
        #endregion

        #region Constructor
        protected BaseDomainObject() { }
        #endregion

        #region Private Properties
        private static DomainDBContext context = new DomainDBContext();
        private const string insertSuccess = "{0} has been save";
        private const string insertFailed = "{0} save failed";
        private const string insertFailedWithException = "{0} save failed : {1}";
        private const string updateSuccess = "update {0} data success";
        private const string updateFailed = "update {0} data failed";
        private const string updateFailedWithException = "update {0} data failed : {1}";
        private const string deleteSuccess = "{0} has been deleted";
        private const string deleteFailed = "delete {0} data failed";
        private const string deleteFailedWithException = "delete {0} data failed : {1}";
        #endregion

        #region Basic Dynamic Query
        // Summary:
        //     Select Domain Object by ID 
        public static T SelectByID(int? id)
        {
            try
            {
                using (context = new DomainDBContext())
                {
                    return context.dbSet.Find(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Summary:
        //     Select Domain Object All
        public static Collection<T> SelectAll()
        {
            try
            {
                using (context = new DomainDBContext())
                {
                    return new Collection<T>(context.dbSet.ToList());
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        // Summary:
        //     Insert Domain Object Data
        public static ResultStatus Insert(ref T entity)
        {
            ResultStatus rs = new ResultStatus();
            rs.MessageText = string.Format(insertFailed, typeof(T).Name);

            try
            {
                using (context = new DomainDBContext())
                {
                    context.dbSet.Add(entity);
                    context.SaveChanges();
                }

                rs.SetSuccessStatus(string.Format(insertSuccess, typeof(T).Name));
            }
            catch (Exception ex)
            {
                rs.MessageText = string.Format(insertFailedWithException, typeof(T).Name, ex);
            }

            return rs;
        }

        //// Summary:
        ////     Bulk Insert Domain Object Data
        public static ResultStatus Insert(Collection<T> collEntity)
        {
            ResultStatus rs = new ResultStatus();
            rs.MessageText = string.Format(insertFailed, typeof(T).Name);

            try
            {
                using (context = new DomainDBContext())
                {
                    context.dbSet.AddRange(collEntity);
                    context.SaveChanges();
                }

                rs.SetSuccessStatus(string.Format(insertSuccess, typeof(T).Name));
            }
            catch (Exception ex)
            {
                rs.MessageText = string.Format(insertFailedWithException, typeof(T).Name, ex);
            }

            return rs;
        }

        //// Summary:
        ////     Update Domain Object Data
        public static ResultStatus Update(T entity)
        {
            ResultStatus rs = new ResultStatus();
            rs.MessageText = string.Format(updateFailed, typeof(T).Name);

            try
            {
                using (context = new DomainDBContext())
                {
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                }

                rs.SetSuccessStatus(string.Format(updateSuccess, typeof(T).Name));
            }
            catch (Exception ex)
            {
                rs.MessageText = string.Format(updateFailedWithException, typeof(T).Name, ex);
            }

            return rs;
        }

        //// Summary:
        ////     Bulk Update Domain Object Data
        public static ResultStatus Update(Collection<T> collEntity)
        {
            ResultStatus rs = new ResultStatus();
            rs.MessageText = string.Format(updateFailed, typeof(T).Name);

            try
            {
                using (context = new DomainDBContext())
                {
                    collEntity.ToList().ForEach(p => context.Entry(p).State = EntityState.Modified);
                    context.SaveChanges();
                }

                rs.SetSuccessStatus(string.Format(updateSuccess, typeof(T).Name));
            }
            catch (Exception ex)
            {
                rs.MessageText = string.Format(updateFailedWithException, typeof(T).Name, ex);
            }

            return rs;
        }

        //// Summary:
        ////     Delete Domain Object Data
        public static ResultStatus Delete(T entity)
        {
            ResultStatus rs = new ResultStatus();
            rs.MessageText = string.Format(deleteFailed, typeof(T).Name);

            try
            {
                using (context = new DomainDBContext())
                {
                    context.Entry(entity).State = EntityState.Deleted;
                    context.SaveChanges();
                }

                rs.SetSuccessStatus(string.Format(deleteSuccess, typeof(T).Name));
            }
            catch (Exception ex)
            {
                rs.MessageText = string.Format(deleteFailedWithException, typeof(T).Name, ex);
            }

            return rs;
        }

        //// Summary:
        ////     Bulk Delete Domain Object Data
        public static ResultStatus Delete(Collection<T> collEntity)
        {
            ResultStatus rs = new ResultStatus();
            rs.MessageText = string.Format(deleteFailed, typeof(T).Name);

            try
            {
                using (context = new DomainDBContext())
                {
                    collEntity.ToList().ForEach(p => context.Entry(p).State = EntityState.Deleted);
                    context.SaveChanges();
                }

                rs.SetSuccessStatus(string.Format(deleteSuccess, typeof(T).Name));
            }
            catch (Exception ex)
            {
                rs.MessageText = string.Format(deleteFailedWithException, typeof(T).Name, ex);
            }

            return rs;
        }
        #endregion

        #region Custom Dynamic query
        // Summary:
        //     Select Domain Object by Field value 
        public static Collection<T> SelectByFieldName(string columnName, object value)
        {
            Collection<T> result = new Collection<T>();
            try
            {
                using (context = new DomainDBContext())
                {
                    Expression<Func<T, bool>> exp = IsMatchedExpression(columnName, value);
                    result = new Collection<T>(context.dbSet.Where(exp).ToList());
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        // Summary:
        //     Select One Domain Object by Field value 
        public static T SelectByCode(string columnName, object value)
        {
            T result = new T();
            try
            {
                using (context = new DomainDBContext())
                {
                    Expression<Func<T, bool>> exp = IsMatchedExpression(columnName, value);
                    result = context.dbSet.Where(exp).FirstOrDefault() ?? new T();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        // Summary:
        //     Select One Domain Object by Field list values
        //     to do : create expression for contains of list value
        public static Collection<T> SelectBySet(string columnName, IList values)
        {
            Collection<T> result = new Collection<T>();
            try
            {
                using (context = new DomainDBContext())
                {
                    //Expression Logic
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
        #endregion

        #region Public Method and Properties
        // Summary:
        //     Generate Domain Object From IDataReader resultset
        public static T BuildDomainFromReader(IDataReader dr)
        {
            try
            {
                T result = new T();

                Type businessEntityType = typeof(T);
                Hashtable hashtable = new Hashtable();
                PropertyInfo[] properties = businessEntityType.GetProperties();
                foreach (PropertyInfo info in properties)
                {
                    hashtable[info.Name.ToUpper()] = info;
                }

                for (int index = 0; index < dr.FieldCount; index++)
                {
                    PropertyInfo info = (PropertyInfo)hashtable[dr.GetName(index).ToUpper()];
                    if ((info != null) && info.CanWrite && !dr.IsDBNull(index))
                    {
                        info.SetValue(result, dr.GetValue(index), null);
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Private Method and Properties
        private static Expression<Func<T, bool>> IsMatchedExpression(string fieldName, object fieldValue)
        {
            var pe = Expression.Parameter(typeof(T));
            var prop = Expression.PropertyOrField(pe, fieldName);
            var propVal = Expression.Constant(fieldValue);
            return Expression.Lambda<Func<T, bool>>(Expression.Equal(prop, propVal), pe);
        }
        #endregion
    }
}

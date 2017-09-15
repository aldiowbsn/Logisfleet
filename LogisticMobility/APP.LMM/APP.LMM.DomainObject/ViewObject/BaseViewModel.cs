using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace APP.LMM.DomainObject.ViewObject
{
    public abstract class BaseViewModel<T> where T : class, new()
    {

        #region Public Method and Properties
        // Summary:
        //     Generate View Model Object From IDataReader resultset
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
    }
}

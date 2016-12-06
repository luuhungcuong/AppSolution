using AppSolution.Infrastructure.Module.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppSolution.Infrastructure.Utils
{
    public static class ObjectToDictionaryHelper
    {
        public static IDictionary<string, object> ToDictionary(this object source)
        {
            return source.ToDictionary<object>();
        }

        public static IDictionary<string, T> ToDictionary<T>(this object source)
        {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull();

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary<T>(property, source, dictionary);

            return dictionary;
        }

        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
        {
            object value = property.GetValue(source);
            if (IsOfType<T>(value))
                dictionary.Add(property.Name, (T)value);
        }

        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }
    }
    public static class DictionaryHelper
    {               
        public static dynamic ToDynamic(this Dictionary<string, object> dict)
        {
            IDictionary<string, object> eo = new ExpandoObject() as IDictionary<string, object>;
            foreach (KeyValuePair<string, object> kvp in dict)
            {
                eo.Add(kvp);
            }
            return eo;
        }
        public static T ToObject<T>(this Dictionary<string, object> dict) where T : new()
        {
            var t = new T();
            PropertyInfo[] properties = t.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                    continue;

                KeyValuePair<string, object> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

                // Find which property type (int, string, double? etc) the CURRENT property is...
                Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;

                // Fix nullables...
                Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;

                // ...and change the type
                object newA = Convert.ChangeType(item.Value, newT);
                t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
            }
            return t;
        }
    }

    public class Utils
    {
        public static string GetUserCodeValue(string tableID, string codeID, DbContextHelper<DataModel.AppSolutionDbContext> trans = null)
        {
            DbContextHelper<DataModel.AppSolutionDbContext> helper = trans ?? new DbContextHelper<DataModel.AppSolutionDbContext>();
            return helper.GetOne<DataModel.UserCode>(x => x.TableID == tableID && x.CodeID == codeID).CodeValue;

        }
        public static IQueryable<DataModel.UserCode> GetUserCodeValueList(string tableID, DbContextHelper<DataModel.AppSolutionDbContext> trans = null)
        {
            DbContextHelper<DataModel.AppSolutionDbContext> helper = trans ?? new DbContextHelper<DataModel.AppSolutionDbContext>();
            return helper.GetTable<DataModel.UserCode>().Where(x => x.TableID == tableID);
        }
    }
}

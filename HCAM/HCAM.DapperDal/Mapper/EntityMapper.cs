using System.Collections.Generic;
using System.Linq;

namespace HCAM.DapperDal.Mapper
{
    public class EntityMapper
    {
        public static PropertyContainer MapProperties<T>(T obj)
        {
            var propertyContainer = new PropertyContainer();

            var typeName = typeof(T).Name;
            var validKeyNames = new[] { "Id", $"{typeName}Id", $"{typeName}_Id" };

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                    continue;
                
                if (property.GetSetMethod() == null)
                    continue;
                
                var name = property.Name;
                var value = typeof(T).GetProperty(property.Name).GetValue(obj, null);

                if (property.IsDefined(typeof(Key), false))
                //if (property.IsDefined(typeof(DapperKey), false) || validKeyNames.Contains(name))
                {
                    propertyContainer.AddId(name, value);
                }
                else
                {
                    propertyContainer.AddValue(name, value);
                }
            }

            return propertyContainer;
        }

        public static void SetId<T>(T obj, int id, IDictionary<string, object> propertyPairs)
        {
            if (propertyPairs.Count == 1)
            {
                var propertyName = propertyPairs.Keys.First();
                var propertyInfo = obj.GetType().GetProperty(propertyName);
                if (propertyInfo.PropertyType == typeof(int))
                {
                    propertyInfo.SetValue(obj, id, null);
                }
            }
        }
        public static string GetSqlPairs(IEnumerable<string> keys, string separator = ", ")
        {
            return string.Join(separator, keys.Select(key => $"{key}=@{key}").ToList());
        }
    }
}

using System;
using System.Data;
using System.Linq;

namespace DAL.Mappers
{
    public static class SqlMapper
    {
        public static T Map<T>(DataRow row)
        {
            T item = Activator.CreateInstance<T>();

            var properties = typeof(T).GetProperties()
                .Where(p => !p.PropertyType.IsInterface);

            foreach (var prop in properties)
            {
                var value = row[prop.Name];

                if (value == DBNull.Value)
                {
                    value = null;
                }

                prop.SetValue(item, value);
            }

            return item;
        }
    }
}

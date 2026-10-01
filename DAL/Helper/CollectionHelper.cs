using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DAL.Helper
{
    public static class CollectionHelper
    {
        public static List<T> ConvertDataTableToList<T>(DataTable dt) where T : new()
        {
            var list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T obj = new T();
                foreach (var prop in obj.GetType().GetProperties())
                {
                    if (dt.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                    {
                        prop.SetValue(obj, row[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Study.Common
{
    public static class DataTableExtension
    {
        public static IEnumerable<T> ToEnumerable<T>(this DataTable dt) where T : class, new()
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            T[] ts = new T[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                T t = new T();
                foreach (PropertyInfo p in propertyInfos)
                {
                    if (dt.Columns.IndexOf(p.Name) != -1 && row[p.Name] != DBNull.Value)
                    {
                        if (p.Name.ToUpper() == "IsValid".ToUpper())
                        {
                            p.SetValue(t, Convert.ToBoolean(row[p.Name]), null);
                        }
                        else
                            p.SetValue(t, row[p.Name], null);
                    }

                }
                ts[i] = t;
                i++;
            }
            return ts;
        }


        public static List<T> ToList<T>(this DbDataReader dr) where T : class, new()
        {
            var result = new List<T>();
            var properties = typeof(T).GetProperties().ToList();
            while (dr.Read())
            {
                var obj = new T();

                foreach (var property in properties)
                {
                    try
                    {
                        //Oracle字段为大写
                        var id = dr.GetOrdinal(property.Name.ToUpper());
                        if (!dr.IsDBNull(id))
                        {
                            if (dr.GetValue(id) != DBNull.Value)
                            {
                                var val = dr.GetValue(id);
                                if (property.Name.ToUpper() == "IsValid".ToUpper())
                                {
                                    property.SetValue(obj, Convert.ToBoolean(val));
                                }
                                else
                                    property.SetValue(obj, val);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                result.Add(obj);
            }
            return result;
        }
    }
}

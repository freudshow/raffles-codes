using System;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using System.ComponentModel;
using DreamStu.Common;

namespace DetailInfo
{
    public class EntityBase<T>
    {
        /// <summary>
        /// 返回存储某类型的所有属性的集合。
        /// </summary>
        /// <param name="objType">类型（类、接口、枚举等）</param>
        /// <returns>属性集合</returns>
        public static List<PropertyInfo> GetPropertyInfoList(Type objType)
        {
            string cachekey = string.Format("LST_{0}", objType.FullName);
            List<PropertyInfo> propList = (List<PropertyInfo>)DSCache.Get(cachekey);

            if (propList == null)
            {
                propList = new List<PropertyInfo>();

                foreach (PropertyInfo objProperty in objType.GetProperties())
                {
                    propList.Add(objProperty);
                }
                DSCache.Insert(cachekey, propList);
            }

            return propList;
        }
        /// <summary>
        /// 由IDataReader获得实体类
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <returns>实体类</returns>
        public static T DReaderToEntity(IDataReader reader)
        {
            T tInstance = default(T);
            if (reader.Read())
                tInstance = DrToEnt(reader);
            reader.Close();
            return tInstance;
        }
        /// <summary>
        /// 由IDataReader获得实体类
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <returns>实体类</returns>
        protected static T DrToEnt(IDataReader reader)
        {
            T tInstance = Activator.CreateInstance<T>();
            Dictionary<string, PropertyInfo> propDic = GetPropertyInfoDic(typeof(T));

            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (propDic.ContainsKey(reader.GetName(i)))
                {
                    //try
                    //{
                    if (reader.GetValue(i) == null || reader.GetValue(i) == DBNull.Value) continue;
                    PropertyInfo pi = propDic[reader.GetName(i)];
                    pi.SetValue(tInstance, ChangeType(reader.GetValue(i), pi.PropertyType), null);
                    //}
                    //catch (Exception e)
                    //{
                    //    DreamStu.Common.Log.WebLogger.AddInfo(reader.GetName(i) + "\n" + i + "\n" + e.Message.ToString() + "\n" );
                    //}
                }
            }
            return tInstance;
        }
        /// <summary>
        /// 返回存储某类型的所有属性和自定义属性(BindingFieldAttribute)的Dic。
        /// </summary>
        /// <param name="objType">类型（类、接口、枚举等）</param>
        /// <returns>属性和自定义属性(BindingFieldAttribute)的集合</returns>
        public static Dictionary<string, PropertyInfo> GetPropertyInfoDic(Type objType)
        {
            string cachekey = string.Format("DIC_{0}", objType.FullName);
            Dictionary<string, PropertyInfo> propDic = (Dictionary<string, PropertyInfo>)DSCache.Get(cachekey);
            if (propDic == null)
            {
                propDic = new Dictionary<string, PropertyInfo>();
                List<PropertyInfo> propList = GetPropertyInfoList(objType);
                foreach (PropertyInfo pi in propList)
                {
                    object[] customAtts = pi.GetCustomAttributes(typeof(BindingFieldAttribute), true);
                    if (customAtts != null && customAtts.Length > 0)
                    {
                        string key = ((BindingFieldAttribute)customAtts[0]).FieldName;
                        if (string.IsNullOrEmpty(key)) key = pi.Name.ToUpper();
                        propDic.Add(key, pi);
                    }
                }
                DSCache.Insert(cachekey, propDic);
            }
            return propDic;
        }
        public static object ChangeType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null) return null;
                if (value.ToString() == string.Empty) return null;

                conversionType = new NullableConverter(conversionType).UnderlyingType;
            }

            if (conversionType.IsEnum)
                return Enum.ToObject(conversionType, value);
            else
                return Convert.ChangeType(value, conversionType);
        }
        /// <summary>
        /// 由IDataReader获得实体类列表
        /// </summary>
        /// <param name="dr">IDataReader</param>
        /// <returns>实体类列表</returns>
        public static List<T> DReaderToEntityList(IDataReader dr)
        {
            List<T> list = new List<T>();
            using (dr)
            {
                while (dr.Read())
                {
                    list.Add(DrToEnt(dr));
                }
                dr.Close();
            }
            return list;
        }

    }
}


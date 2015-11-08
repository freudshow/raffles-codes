using System;
using System.Reflection;

namespace DetailInfo
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BindingFieldAttribute : Attribute
    {
        private string fieldName;
        /// <summary>
        /// 获取或设置数据库字段名称。
        /// </summary>
        public string FieldName
        {
            get
            {
                return fieldName;
            }
            set
            {
                fieldName = value;
            }
        }
    }
}


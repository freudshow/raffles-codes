using System;
using System.Collections.Generic;
using System.Text;

namespace DetailInfo
{
    public enum SqlOperator
    {
        /// <summary>
        /// Like 模糊查询
        /// </summary>
        Like,

        /// <summary>
        /// ＝ is equal to 等于号 
        /// </summary>
        Equal,

        /// <summary>
        /// <> (≠) is not equal to 不等于号
        /// </summary>
        NotEqual,

        /// <summary>
        /// ＞ is more than 大于号
        /// </summary>
        MoreThan,

        /// <summary>
        /// ＜ is less than 小于号 
        /// </summary>
        LessThan,

        /// <summary>
        /// ≥ is more than or equal to 大于或等于号 
        /// </summary>
        MoreThanOrEqual,

        /// <summary>
        /// ≤ is less than or equal to 小于或等于号
        /// </summary>
        LessThanOrEqual,

        
        /// <summary>
        /// 在某个值的中间，拆成两个符号 >= 和 <=
        /// </summary>
        Between,

    }
}
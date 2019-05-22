using System;
using System.Collections.Generic;
using System.Text;

namespace ExportDrawbackManagement.Framework.Common
{
    /// <summary>
    /// 标志枚举工具类
    /// </summary>
    /// <typeparam name="T">标志枚举类型</typeparam>
    public class FlagsEnumHelper<T> where T : struct
    {
        T srcFlag;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="srcFlag">标志枚举值</param>
        public FlagsEnumHelper(T srcFlag)
        {
            this.srcFlag = srcFlag;
        }

        #region Private Method
        int ToInt(T flag)
        {
            return Convert.ToInt32(flag);
        }

        T ToEnum(int flag)
        {
            return (T)Enum.ToObject(typeof(T), flag);
            //return (T)Convert.ChangeType(flag, typeof(T));
        }
        #endregion

        #region 添加删除标志
        /// <summary>
        /// 添加标志枚举值
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T Add(T flag, params T[] flags)
        {
            int tmp = ToInt(srcFlag);
            tmp |= ToInt(flag);
            foreach (T f in flags)
            {
                tmp |= ToInt(f);
            }
            srcFlag = ToEnum(tmp);
            return srcFlag;
        }

        /// <summary>
        /// 移除标志枚举值
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T Remove(T flag, params T[] flags)
        {
            int tmp = ToInt(flag);
            foreach (T f in flags)
            {
                tmp += ToInt(f);
                //srcFlag = Remove(flag);
            }
            srcFlag = ToEnum(~tmp & ToInt(srcFlag));
            return srcFlag;
        }

        /// <summary>
        /// 移除全部标志
        /// </summary>
        /// <returns></returns>
        public T RemoveAll()
        {
            srcFlag = ToEnum(0);
            return srcFlag;
        }

        /// <summary>
        /// 添加全部标志
        /// </summary>
        /// <returns></returns>
        public T AddAll()
        {
            srcFlag = ToEnum(-1);
            return srcFlag;
        }
        #endregion

        #region 判断存在
        /// <summary>
        /// 判断标志枚举值是否存在全部参数
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool Exists(T flag, params T[] flags)
        {
            int tmp = ToInt(flag);
            foreach (T f in flags)
            {
                tmp += ToInt(f);
            }
            return (tmp & ToInt(srcFlag)) == tmp;
        }

        /// <summary>
        /// 判断标志枚举值是否存在参数之一
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool ExistsAny(T flag, params T[] flags)
        {
            int tmp = ToInt(flag);
            foreach (T f in flags)
            {
                tmp += ToInt(f);
            }
            return (tmp & ToInt(srcFlag)) != 0;
        }

        /// <summary>
        /// 判断标志枚举值是否仅存在全部参数
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool ExistsOnly(T flag, params T[] flags)
        {
            int tmp = ToInt(flag);
            foreach (T f in flags)
            {
                tmp += ToInt(f);
            }
            return tmp == ToInt(srcFlag);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace DncEthic.Core.Helper
{
    public static class EnumExtension
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            string desc = string.Empty;

            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        MemberInfo[] memInfo = type.GetMember(type.GetEnumName(val));
                        object[] descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (descriptionAttributes.Length > 0)
                        {
                            // we're only getting the first description we find
                            // others will be ignored
                            desc = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                        }
                        break;
                    }
                }
            }
            return desc;
        }
        /// <summary>
        /// 根据值得到中文备注
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Type e, int? value)
        {
            FieldInfo[] fields = e.GetFields();
            for (int i = 1, count = fields.Length; i < count; i++)
            {
                if ((int)System.Enum.Parse(e, fields[i].Name) == value)
                {
                    DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])fields[i].
                GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (EnumAttributes.Length > 0)
                    {
                        return EnumAttributes[0].Description;
                    }
                }
            }
            return "";
        }
        /// <summary>
               /// 获取描述信息
               /// </summary>
               /// <param name="envalue">枚举值的集合</param>
               /// <returns>枚举值对应的描述集合</returns>
        public static List<String> descriptions<T>(List<int> envalue)
        {


            Type type = typeof(T);


            List<String> deslist = new List<String>();


            foreach (FieldInfo x in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                T item = (T)x.GetValue(null);
                if (envalue.Find((int e) => { return e == Convert.ToInt32(item); }) > 0)
                {
                    deslist.Add(description<T>(item));
                }
            }

            return deslist;
        }
        public static string description<T>(T en)
        {


            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }
        private static void ShowSquares(int val)
        {
            Console.WriteLine("{0:d} squared = {1:d}", val, val * val);
        }
    }
}

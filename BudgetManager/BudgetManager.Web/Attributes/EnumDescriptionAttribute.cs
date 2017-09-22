using System;
using System.Reflection;

namespace BudgetManager.Web.Attributes
{
    //code from http://www.codeproject.com/Articles/14800/NET-Enum-The-Next-Level

    public class EnumDescriptionAttribute : Attribute
    {
        private readonly string m_strDescription;

        public EnumDescriptionAttribute(string strPrinterName)
        {
            m_strDescription = strPrinterName;
        }

        public string Description
        {
            get { return m_strDescription; }
        }
    }

    public static class EnumDescriptionExtentions
    {
        public static string GetEnumDescription(this Enum enumObj)
        {
            FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());
            object[] attribArray = fieldInfo.GetCustomAttributes(false);
            if (attribArray.Length > 0)
            {
                var attrib = attribArray[0] as EnumDescriptionAttribute;
                return attrib.Description;
            }
            return String.Empty;
        }
    }
}
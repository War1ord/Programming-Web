using System;

namespace BudgetManager.Enums.Attributes
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
}
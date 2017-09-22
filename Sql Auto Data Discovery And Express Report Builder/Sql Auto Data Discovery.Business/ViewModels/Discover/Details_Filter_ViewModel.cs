using System.Collections.Generic;

namespace Sql_Auto_Data_Discovery.Business.ViewModels.Discover
{
    public struct Details_Filter_ViewModel
    {
        public int Top { get; set; }

        public string[] ColumnsToShow { get; set; }
    }
}
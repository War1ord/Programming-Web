using System.Collections.Generic;

namespace Sql_Auto_Data_Discovery.Business.Models
{
    public class TableQueryData
    {
        public List<KeyValuePair<string, string>> Filters { get; set; }
        public List<KeyValuePair<string, string>> Columns { get; set; }
        public List<KeyValuePair<string, bool>> SortBy { get; set; }
    }
}
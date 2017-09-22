using Sql_Auto_Data_Discovery.Business.Models.Sql;

namespace Sql_Auto_Data_Discovery.Business.Models
{
    public class SqlObjectsModel
    {
        public int object_id { get; set; }
        public string name { get; set; }
        public string type_desc { get; set; }
    }
}
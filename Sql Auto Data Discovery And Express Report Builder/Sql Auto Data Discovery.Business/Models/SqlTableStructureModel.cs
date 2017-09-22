using System;

namespace Sql_Auto_Data_Discovery.Business.Models
{
    public class SqlTableStructureModel
    {                                               // Column_name        Type        Computed    Length  Prec    Scale   Nullable
        public Int32 column_id { get; set; }        // column_id	        int	        no	        4	    10      0       no
        public string name { get; set; }            // name	            sysname	    no	        256	                    yes
        public Int16 max_length { get; set; }       // max_length	        smallint    no	        2	    5       0       no
        public bool? is_nullable { get; set; }      // is_nullable	    bit	        no	        1	                    yes
        public bool is_identity { get; set; }       // is_identity	    bit	        no	        1	                    no
        public bool is_computed { get; set; }       // is_computed	    bit	        no	        1	                    no
    }
}




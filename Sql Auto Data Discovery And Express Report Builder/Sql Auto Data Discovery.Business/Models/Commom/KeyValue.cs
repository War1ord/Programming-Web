namespace Sql_Auto_Data_Discovery.Business.Models.Commom
{
    public class KeyValue
    {
        private const string Equals_Default = "=";               // = (Equals)
        private const string Greater_Than = ">";                 // > (Greater Than)
        private const string Less_Than = "<";                    // < (Less Than)
        private const string Greater_Than_Or_Equal_To = ">=";    // >= (Greater Than or Equal To)
        private const string Less_Than_Or_Equal_To = "<=";       // <= (Less Than or Equal To)
        private const string Not_Equal_To = "<>";                // <> (Not Equal To)
        private const string Not_Less_Than = "!<";               // !< (Not Less Than) (not ISO standard)
        private const string Not_Greater_Than = "!>";            // !> (Not Greater Than) (not ISO standard)

        private KeyValue(){}

        public KeyValue(string key, object value)
        {
            Key = key;
            Value = value;
            OperatorKey = Operators.Equals;
        }

        public KeyValue(object key, object value)
        {
            Key = key.ToString();
            Value = value;
            OperatorKey = Operators.Equals;
        }

        public KeyValue(object key, Operators operatorKey, object value)
        {
            Key = key.ToString();
            Value = value;
            OperatorKey = operatorKey;
        }

        public static string Parse(Operators operatorKey)
        {
            switch (operatorKey)
            {
                case Operators.Equals: return Equals_Default;                               // = (Equals)
                case Operators.Greater_Than: return Greater_Than;                           // > (Greater Than)
                case Operators.Less_Than: return Less_Than;                                 // < (Less Than)
                case Operators.Greater_Than_Or_Equal_To: return Greater_Than_Or_Equal_To;   // >= (Greater Than or Equal To)
                case Operators.Less_Than_Or_Equal_To: return Less_Than_Or_Equal_To;         // <= (Less Than or Equal To)
                case Operators.Not_Equal_To: return Not_Equal_To;                           // <> (Not Equal To)
                case Operators.Not_Less_Than:return Not_Less_Than;                          // !< (Not Less Than) (not ISO standard)
                case Operators.Not_Greater_Than: return Not_Greater_Than;                   // !> (Not Greater Than) (not ISO standard)
                default: return Equals_Default;
            }
        }

        public string Key { get; set; }
        public object Value { get; set; }
        public Operators OperatorKey { get; set; }
        public string OperatorValue { get { return Parse(OperatorKey); }}

    }

    public enum Operators
    {
          Equals                    // = (Equals)
        , Greater_Than              // > (Greater Than)
        , Less_Than                 // < (Less Than)
        , Greater_Than_Or_Equal_To  // >= (Greater Than or Equal To)
        , Less_Than_Or_Equal_To     // <= (Less Than or Equal To)
        , Not_Equal_To              // <> (Not Equal To)
        , Not_Less_Than             // !< (Not Less Than) (not ISO standard)
        , Not_Greater_Than          // !> (Not Greater Than) (not ISO standard)

        , Like                      // LIKE '%' + @value + '%' NOTE: not yet implemented
        , Not_Like                  // NOT LIKE '%' + @value + '%' NOTE: not yet implemented
        , Like_Right                // LIKE @value + '%' NOTE: not yet implemented
        , Not_Like_Right            // NOT LIKE @value + '%' NOTE: not yet implemented
        , Like_Left                 // LIKE '%' + @value NOTE: not yet implemented
        , Not_Like_Left             // NOT LIKE '%' + @value NOTE: not yet implemented

        , Is_Null                   // IS NULL NOTE: not yet implemented
        , Is_Not_Null               // IS NOT NULL NOTE: not yet implemented

        , In                        // IN (@values)  NOTE: not yet implemented
        , Not_In                    // NOT IN (@values) NOTE: not yet implemented

    }
}
namespace Sql_Auto_Data_Discovery.Business.Extentions
{
    public static class ObjectExtensions
    {
        public static bool IsSet(this object obj)
        {
            return obj != null;
        }
        public static bool IsNotSet(this object obj)
        {
            return obj == null;
        }
    }
}
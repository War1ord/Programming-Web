namespace Sql_Auto_Data_Discovery.Business.ViewModels.Discover
{
    public class Details_ViewModel
    {
        public Business.ViewModels.Discover.Details_Filter_ViewModel Filter { get; set; }

        public Business.ViewModels.Discover.Details_OrderBy_ViewModel OrderBy { get; set; }

        public System.Data.DataTable Data { get; set; }

        public string[] Columns { get; set; }
    }
}
using System.Data;
using Sql_Auto_Data_Discovery.Business.Data;
using Sql_Auto_Data_Discovery.Business.Extentions;
using Sql_Auto_Data_Discovery.Business.ViewModels.Discover;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Sql_Auto_Data_Discovery.WebApplication.Controllers
{
    /// <summary>
    /// The discover controller.
    /// </summary>
    public class DiscoverController : Controller
    {
        public ActionResult Index()
        {
            const string title = "Database";
            ViewBag.Title = title;
            ViewBag.Header = title;
            return View("Databases", Connections.List);
        }

        public ActionResult Objects(string c)
        {
            var title = string.Format("Objects in {0}", c);
            ViewBag.Title = title;
            ViewBag.Header = title;
            var connection = Connections.List.FirstOrDefault(i => i.Key.Equals(c, StringComparison.InvariantCultureIgnoreCase));
            if (connection.Value.IsSet())
            {
                Connections.Selected = connection;
                using (var db = new Database(connection.Value.ConnectionString))
                {
                    return View("Objects", db.GetListOfViewableObjects());
                }
            }
            else
            {
                Connections.Selected = new KeyValuePair<string, SqlConnectionStringBuilder>();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(string t, int? top, string[] columnsToShow, string[] columnsToOrderBy)
        {
            var title = string.Format("Details of table {0}", t);
            ViewBag.Title = title;
            ViewBag.Header = title;
            if (Connections.Selected.Key.IsSet() && t.IsSet())
            {
                using (var db = new Database(Connections.Selected.Value.ConnectionString))
                {
                    var filter = new Details_Filter_ViewModel
                    {
                        Top = top ?? 10,
                        ColumnsToShow = columnsToShow,
                    };
                    var orderBy = new Details_OrderBy_ViewModel
                    {
                        ColumnsToOrderBy = columnsToOrderBy,
                    };
                    var data = db.GetDataTable(t, filter, orderBy);
                    var columns = db.GetColumnNames(t)
                                    .AsEnumerable()
                                    .OrderBy(i => i["object_id"].ToString().AsInt())
                                    .Select(i => i["name"].ToString())
                                    .ToArray();
                    var model = new Details_ViewModel
                    {
                        Filter = filter,
                        OrderBy = orderBy,
                        Data = data,
                        Columns = columns
                    };
                    return View("Details", model);
                }
            }
            else
            {
                Connections.Selected = new KeyValuePair<string, SqlConnectionStringBuilder>();
                return RedirectToAction("Index");
            }
        }

    }
}
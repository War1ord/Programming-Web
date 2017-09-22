using System;
using System.Web.Mvc;

namespace RequestForService.Web.Controllers.WorkOrderNotes
{
    public partial class WorkOrderNotesController : Base.AuthenticationBaseController
    {
        public ActionResult Add(Guid id)
        {
            return View();
        }
    }
}
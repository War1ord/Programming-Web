using System;
using System.Web.Mvc;

namespace RequestForService.Web.Controllers.WorkItems
{
    public class WorkItemsController : Base.AuthenticationBaseController
    {
        /// <summary>
        /// Adds a new Work item for id={work order}
        /// </summary>
        /// <param name="id">The work orders id.</param>
        public ActionResult Add(Guid id)
        {
            return View();
        }


    }
}
using System.Collections.Generic;
using RequestForService.Business.Services.Errors;
using RequestForService.Business.Services.WorkOrders;
using RequestForService.Web.ViewModels.WorkOrderComments;
using System;
using System.Web.Mvc;

namespace RequestForService.Web.Controllers.WorkOrderComments
{
    public class WorkOrderCommentsController : Base.AuthenticationBaseController
    {
        #region fields
        private WorkOrderCommentsService _service; 
        #endregion

        private WorkOrderCommentsService Service
        {
            get { return _service ?? (_service = new WorkOrderCommentsService(UserId)); }
            set { _service = value; }
        }

        public ActionResult Add(Guid id)
        {
            var result = Service.GetWorkOrderByIdIncludingProperties(id);
            return View(new WorkOrderComments_Add_ViewModel
            {
                Result = result,
                WorkOrder = result.Entity,
            });
        }

        [HttpPost]
        public ActionResult Add(WorkOrderComments_Add_ViewModel model)
        {
            var list_to_remove = new List<string>();
            var enumerator = ModelState.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Key.StartsWith("WorkOrder"))
                {
                    list_to_remove.Add(enumerator.Current.Key);
                }
            }
            foreach (var item in list_to_remove)
            {
                var remove = ModelState.Remove(item);
            }
            if (ModelState.IsValid)
            {
                if (model.Comment != null)
                {
                    model.Comment.WorkOrderId = model.WorkOrder.Id;
                    if (UserId.HasValue)
                    {
                        model.Comment.CreatedByUserId = UserId.Value; 
                    }
                }
                model.Result = Service.CreateEntity(model.Comment);
                if (model.Result.IsSuccessful)
                {
                    return RedirectToAction("Details", "WorkOrders", new {id = model.WorkOrder.Id});
                }
            }
            model.WorkOrder = Service.GetWorkOrderByIdIncludingProperties(model.WorkOrder.Id).Entity;
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (Service != null)
            {
                Service.Dispose();
                Service = null;
            }
            base.Dispose(disposing);
        }
    }
}
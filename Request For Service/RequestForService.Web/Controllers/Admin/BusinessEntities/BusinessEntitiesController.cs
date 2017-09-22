using RequestForService.Business.Services.Admin;
using RequestForService.Models.BusinessEntities;
using RequestForService.Web.ViewModels.BusinessEntities;
using System;
using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Admin.BusinessEntities
{
	public partial class BusinessEntitiesController : Base.AuthenticationBaseController
	{
		private BusinessEntitiesService _business;
		private BusinessEntitiesService Business
		{
			get { return _business ?? (_business = new BusinessEntitiesService(UserId)); }
		}

		public ActionResult Index(Guid? id = null)
		{
			BusinessEntity businessEntity = null;
			if (id.HasValue)
			{
				var result = Business.GetBusinessEntity(id.Value);
				if (result.IsValidEntity)
				{
					businessEntity = result.Entity;
				}
			}
			var model = new BusinessEntitiesListViewModel
			{
				List = Business.GetBusinessEntitiesListByParent(id).Entity,
				ParentEntityId = id,
				ParentParentEntityId = businessEntity != null ? businessEntity.ParentEntityId : null,
				ParentParentEntityName = businessEntity != null ? businessEntity.Name : null,
			};
			return View(model);
		}
		public ActionResult Add(Guid? id = null)
		{
			var model = new BusinessEntityItemViewModel
			{
				ParentEntityId = id
			};
			ViewBag.BusinessEntitiesList = Business.GetBusinessEntitiesListByParent(id).Entity;
			ViewBag.IndustryLevelList = Business.GetEntityList<IndustryLevel, string>(i => i.Name, false).Entity;
			return View(model);
		}
		public ActionResult Update(Guid id)
		{
			var model = new BusinessEntityItemViewModel
			{
				BusinessEntity = Business.GetBusinessEntity(id).Entity
			};
			ViewBag.BusinessEntitiesList = Business.GetBusinessEntitiesListByParent(id).Entity;
			ViewBag.IndustryLevelList = Business.GetEntityList<IndustryLevel, string>(i => i.Name, false).Entity;
			return View(model);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_business != null)
				{
					_business.Dispose();
					_business = null;
				}
			}
			base.Dispose(disposing);
		}
	}
}
using System;
using System.Collections.Generic;
using RequestForService.Business.Models;
using RequestForService.Models.WorkOrders;
using RequestForService.Data;

namespace RequestForService.Business.Services.WorkOrders
{
	public class WorkOrderTypeService : Base.DataManagerBase
	{
		public WorkOrderTypeService(Guid? userId) : base(userId) {}
		public WorkOrderTypeService(DataContext db, Guid? userId) : base(db, userId) {}

		public Result CreateType(WorkOrderType workOrderType, Guid? businessEntityId)
		{
			try
			{
				if (UserId.HasValue)
				{
					if (businessEntityId.HasValue)
					{
						workOrderType.CreatedByUserId = UserId.Value;
						workOrderType.BusinessEntityId = businessEntityId.Value;
						return base.CreateEntity(workOrderType);
					}
					else
					{
						return Results.ErrorResult("You need to be linked to a business entity to add a hourly rate.");
					}
				}
				else
				{
					return Results.ErrorResult();
				}
			}
			catch (Exception exception)
			{
				Errors.Logs.Log(exception, UserId);
				return Results.ErrorResult();
			}
		}

		public Result<List<WorkOrderType>> GetList(bool isDeleted = false)
		{
			return base.GetEntityList<WorkOrderType>(isDeleted);
		}

		public Result UpdateNameAndDescription(WorkOrderType workOrderType)
		{
			try
			{
				return UpdateEntityProperties<WorkOrderType>(workOrderType.Id, type => new WorkOrderType
				{
				    Name = workOrderType.Name,
				    Description = workOrderType.Description,
				});
			}
			catch (Exception exception)
			{
				Errors.Logs.Log(exception, UserId);
				return Results.ErrorResult();
			}
		}
	}
}
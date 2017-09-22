using RequestForService.Business.Models;
using RequestForService.Data;
using RequestForService.DataTypes.Enums;
using RequestForService.Models.WorkOrders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RequestForService.Business.Services.WorkOrders
{
    public class WorkOrderService : Base.DataManagerBase
	{
		public WorkOrderService(Guid? userId) : base(userId){}
		public WorkOrderService(DataContext db, Guid? userId) : base(db, userId){}

		public Result<Guid?> Create(WorkOrder workorder)
		{
			try
			{
				if (workorder == null) throw new ArgumentNullException("workorder");
				if (!UserId.HasValue) throw new ArgumentException("The user id had no value.");
				workorder.CreatedByUserId = UserId.Value;
                workorder.Status = WorkOrderStatus.Created;
				return CreateEntityAndReturnId(workorder);
			}
			catch (Exception exception)
			{
				Errors.Logs.Log(exception, UserId);
				return Results.ErrorResult(null as Guid?);
			}
		}
		public Result<WorkOrder> GetWorkOrderById(Guid id, bool isDeleted = false)
		{
			try
			{
				return GetEntity<WorkOrder>(id);
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult(new WorkOrder());
			}
		}
        public Result<WorkOrder> GetWorkOrderByIdIncludingProperties(Guid id, bool isDeleted = false)
		{
			try
			{
			    var w = Db.Set<WorkOrder>()
					.Where(i => i.IsDeleted == isDeleted && i.Id == id)
                    .Include(i => i.AssignedToUser)
                    .Include(i => i.CreatedByUser)
                    .Include(i => i.HourlyRate)
                    .Include(i => i.WorkOrderType)
                    .Include(i => i.Attachments)
                    .Include(i => i.Comments)
                    .Include(i => i.Notes)
                    .Include(i => i.Estimations)
                    .Include(i => i.Invoices)
                    .Include(i => i.WorkItems)
					.FirstOrDefault();
			    return w == null 
                    ? Results.ErrorResult<WorkOrder>("Work order not found.") 
                    : Results.NoneResult(w);
			}
			catch (Exception e)
			{
				LogException(e);
                return Results.ErrorResult<WorkOrder>();
			}
		}
		public Result<List<WorkOrder>> GetWorkOrderSummaryList(WorkOrderSummaryParams parameters, bool isDeleted = false)
		{
			try
			{
				var searchText = parameters.SearchText != null ? parameters.SearchText.ToLower().Trim() : string.Empty;
				var fromDate = parameters.FromDate;
				var toDate = parameters.ToDate;
				var selectedUserId = parameters.SelectedUserId;
				var selectedBusinessEntityId = parameters.SelectedBusinessEntityId;
				var sortBy = parameters.SortBy;
				//TODO: assigned to user still need to be integrated
				Expression<Func<WorkOrder, bool>> filter = w => 
					w.IsDeleted == isDeleted
					&& w.DateCreated >= fromDate
					&& w.DateCreated <= toDate
					&& (
						w.Reference.ToLower().Contains(searchText) ||
						w.Title.ToLower().Contains(searchText) ||
						w.Description.ToLower().Contains(searchText)
					) 
					&& (!selectedBusinessEntityId.HasValue // if businesses selected is all 
						? (!selectedUserId.HasValue // then if users selected is not assigned
							? (w.AssignedToUserId == null) // then filter where assigned user is null
							: (selectedUserId.Value == Guid.Empty // else if users selected is all 
								? (w.AssignedToUserId != null)//then filter where assigned user is assigned (/*(1==1) // then filter none*/)
								: (w.AssignedToUserId == selectedUserId.Value) // else filter by assigned user 
							)
						)
						: (!selectedUserId.HasValue // else if users selected is not assigned 
							? (w.AssignedToUserId == null) // then filter where assigned user is null
							: (selectedUserId.Value == Guid.Empty // else if users selected is all 
								? (w.AssignedToUser.BusinessEntityId == selectedBusinessEntityId.Value) // then filter by assigned user's BusinessEntityId
								: (w.AssignedToUserId == selectedUserId.Value) // else filter by assigned user 
							)
						)
					)
				;
				switch (sortBy)
				{
					case Enums.WorkOrderSortBy.Created:
						return GetEntityList(filter, w => w.DateCreated, true );
					case Enums.WorkOrderSortBy.ID:
						return GetEntityList(filter, w => w.Id, true);
					case Enums.WorkOrderSortBy.Title:
						return GetEntityList(filter, w => w.Title, true);
					case Enums.WorkOrderSortBy.Reference:
						return GetEntityList(filter, w => w.Reference, true);
					default:
						return GetEntityList(filter, w => w.DateCreated, true);
				}
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult<List<WorkOrder>>();
			}
		}
		public Result<List<EntityDisplay>> GetBusinessEntitiesListByParent(Guid? businessEntityId)
		{
			try
			{
				var list = Db.Set<RequestForService.Models.BusinessEntities.BusinessEntity>()
					.Where(i => i.ParentEntityId == businessEntityId)
					.Select(i => new EntityDisplay { Id = i.Id, Value = i.Name })
					.ToList();
				return Results.NoneResult(list);
			}
			catch (Exception exception)
			{
				LogException(exception);
				return Results.ErrorResult(new List<EntityDisplay>());
			}
		}
		public Result<List<EntityDisplay>> GetUsersList(Guid? businessEntityId)
		{
			try
			{
				if (businessEntityId.HasValue)
				{
					var list = Db.Set<RequestForService.Models.Users.User>()
						.Where(i => i.BusinessEntityId == businessEntityId)
						.Select(i => new EntityDisplay {Id = i.Id, Value = i.Person.FullNames})
						.ToList();
					var returnList = new List<EntityDisplay>
					{
						new EntityDisplay
						{
							Id = Guid.Empty,
							Value = "All Assigned",
						}
					}.Union(list).ToList();
					return Results.NoneResult(returnList);
				}
				else
				{
					return Results.NoneResult(new List<EntityDisplay>
					{
						new EntityDisplay
						{
							Id = Guid.Empty,
							Value = "All Assigned",
						}
					});
				}
			}
			catch (Exception exception)
			{
				LogException(exception);
				return Results.ErrorResult(new List<EntityDisplay>());
			}
		}

	}
}
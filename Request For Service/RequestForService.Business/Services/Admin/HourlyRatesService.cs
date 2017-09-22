using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RequestForService.Models.WorkOrders;
using RequestForService.Business.Models;
using RequestForService.Data;

namespace RequestForService.Business.Services.Admin
{
	public class HourlyRatesService : Base.DataManagerBase
	{
		public HourlyRatesService(Guid? userId) : base(userId){}
		public HourlyRatesService(DataContext db, Guid? userId) : base(db, userId){}

		public Result<HourlyRate> GetEntity(Guid id, bool isDeleted = false)
		{
			return base.GetEntity<HourlyRate>(id, isDeleted);
		}
		public Result<List<HourlyRate>> GetEntityList<TProperty>(Expression<Func<HourlyRate, TProperty>> sort, bool isSortDescending, bool isDeleted = false)
		{
			return base.GetEntityList(sort, isSortDescending, isDeleted);
		}
		public Result CreateEntity(HourlyRate entity, Guid? businessEntityId)
		{
			if (UserId.HasValue)
			{
				if (businessEntityId.HasValue)
				{
					entity.CreatedByUserId = UserId.Value;
					entity.BusinessEntityId = businessEntityId.Value;
					return base.CreateEntity(entity); 
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
		public Result UpdateEntityProperties(HourlyRate hourlyRate)
		{
			return base.UpdateEntityProperties<HourlyRate>(hourlyRate.Id, i => new HourlyRate
			{
				Price = hourlyRate.Price,
				Description = hourlyRate.Description,
			});
		}
	}
}
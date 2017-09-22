using RequestForService.Business.Models;
using RequestForService.Data;
using RequestForService.Models.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RequestForService.Business.Services.Admin
{
	public class BusinessEntitiesService : Base.DataManagerBase
	{
		public BusinessEntitiesService(Guid? userId) : base(userId){}
		public BusinessEntitiesService(DataContext db, Guid? userId) : base(db, userId){}

		public Result AddBusinessEntity(BusinessEntity businessEntity)
		{
			try
			{
				if (businessEntity == null) throw new ArgumentNullException("businessEntity");

				if (UserId.HasValue)
				{
					businessEntity.CreatedByUserId = UserId.Value;
					Add(businessEntity);
					Db.SaveChanges();
					var hasBusinessEntity = Db
						.Set<RequestForService.Models.Users.User>()
						.Where(u => u.Id == UserId)
						.All(u => u.BusinessEntityId.HasValue);
					if (!hasBusinessEntity)
					{
						var businessEntityId = businessEntity.Id;
						UpdateEntityProperties<RequestForService.Models.Users.User>(
							UserId.Value,
							u => new RequestForService.Models.Users.User
							     {
								     BusinessEntityId = businessEntityId
							     });
					}
					return Results.SuccessResult();
				}
				else
				{
					return Results.ErrorResult();
				}
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}

		public Result<BusinessEntity> GetBusinessEntity(Guid id)
		{
			try
			{
				return GetEntity<BusinessEntity>(id);
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult<BusinessEntity>();
			}
		}

		public Result<List<BusinessEntity>> GetBusinessEntitiesListByParent(Guid? businessEntityId = null)
		{
			try
			{
				return GetEntityList<BusinessEntity>(b => b.ParentEntityId == businessEntityId);
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult<List<BusinessEntity>>();
			}
		}

		public Result UpdateBusinessEntity(BusinessEntity businessEntity)
		{
			try
			{
				if (businessEntity == null) throw new ArgumentNullException("businessEntity");
				if (businessEntity.Id == Guid.Empty) throw new ArgumentException("Invalid Business Entity Identifier.");
				return UpdateEntityProperties<BusinessEntity>(businessEntity.Id,
					b => new BusinessEntity
					     {
							 Description = businessEntity.Description,
							 Name = businessEntity.Name,
							 IndustryLevelId = businessEntity.IndustryLevelId,
							 ParentEntityId = businessEntity.ParentEntityId
					     });
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}
	}
}
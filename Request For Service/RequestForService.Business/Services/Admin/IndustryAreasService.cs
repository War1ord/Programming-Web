using System;
using System.Collections.Generic;
using RequestForService.Models.BusinessEntities;
using RequestForService.Business.Models;
using RequestForService.Data;

namespace RequestForService.Business.Services.Admin
{
	public class IndustryAreasService : Base.DataManagerBase
	{
		public IndustryAreasService(Guid? userId) : base(userId){}
		public IndustryAreasService(DataContext db, Guid? userId) : base(db, userId){}

		public Result<List<IndustryArea>> GetList()
		{
			return GetEntityList<IndustryArea>();
		}
		public Result<IndustryArea> GetIndustryArea(Guid id)
		{
			return GetEntity<IndustryArea>(id);
		}
		public Result AddIndustryArea(IndustryArea industryArea)
		{
			if (UserId.HasValue)
			{
				industryArea.CreatedByUserId = UserId.Value;
				return CreateEntity(industryArea); 
			}
			//TODO: please test exception logging. 
			LogException(new ApplicationException("The user id may not be null."));
			return Results.ErrorResult();
		}
		public Result UpdateIndustryArea(IndustryArea industryArea)
		{
			try
			{
				if (industryArea == null) throw new ArgumentNullException("industryArea");
				if (industryArea.Id == Guid.Empty) throw new InvalidOperationException("Identifier is invalid.");

				return UpdateEntityProperties<IndustryArea>(
					industryArea.Id,
					i => new IndustryArea
					     {
						     Description = industryArea.Description,
						     Name = industryArea.Name,
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
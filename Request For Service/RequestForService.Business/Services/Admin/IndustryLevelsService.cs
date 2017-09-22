using System;
using System.Collections.Generic;
using RequestForService.Models.BusinessEntities;
using RequestForService.Business.Models;
using RequestForService.Data;

namespace RequestForService.Business.Services.Admin
{
	public class IndustryLevelsService : Base.DataManagerBase
	{
		public IndustryLevelsService(Guid? userId) : base(userId){}
		public IndustryLevelsService(DataContext db, Guid? userId) : base(db, userId){}

		public Result<List<IndustryLevel>> GetList()
		{
			return GetEntityList<IndustryLevel, int>(i => i.LevelOrder, false);
		}
		public Result CreateIndustryLevel(IndustryLevel industryLevel, Guid userid)
		{
			try
			{
				if (industryLevel == null) throw new ArgumentNullException("industryLevel");
				industryLevel.CreatedByUserId = userid;
				return CreateEntity(industryLevel);
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}
		public Result UpdateIndustryLevel(IndustryLevel industryLevel)
		{
			try
			{
				if (industryLevel == null) throw new ArgumentNullException("industryLevel");
				if (industryLevel.Id == Guid.Empty) throw new InvalidOperationException("Identifier is invalid.");

				return UpdateEntityProperties<IndustryLevel>(
					industryLevel.Id,
					i => new IndustryLevel
					{
						Description = industryLevel.Description,
						Name = industryLevel.Name,
						LevelOrder = industryLevel.LevelOrder,
						IndustryAreaId = industryLevel.IndustryAreaId,
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
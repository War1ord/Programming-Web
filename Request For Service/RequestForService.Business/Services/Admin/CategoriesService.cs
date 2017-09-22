using RequestForService.Business.Models;
using RequestForService.Data;
using RequestForService.Models.WorkOrders;
using System;
using System.Collections.Generic;

namespace RequestForService.Business.Services.Admin
{
	public class CategoriesService : Base.DataManagerBase
	{
		public CategoriesService(Guid? userId) : base(userId) { }
		public CategoriesService(DataContext db, Guid? userId) : base(db, userId) { }

		public Result<List<Category>> GetCategories(Guid? businessEntityId)
		{
			try
			{
				if (businessEntityId != null)
				{
					return GetEntityList<Category, string>(i => i.BusinessEntityId == businessEntityId, i => i.Name, isSortDescending: false);
				}
				else
				{
					return Results.ErrorResult<List<Category>>(
						"You are not allowed to add Categories if your not associated with a business entity.");
				}
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult<List<Category>>();
			}
		}
		public Result<Category> GetCategory(Guid id)
		{
			try
			{
				return GetEntity<Category>(id);
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult<Category>();
			}
		}
		public Result Add(Category category, Guid? businessEntityId)
		{
			try
			{
				if (category == null) throw new ArgumentNullException("category");
				if (businessEntityId.HasValue && UserId.HasValue)
				{
					category.BusinessEntityId = businessEntityId.Value;
					category.CreatedByUserId = UserId.Value;
					return CreateEntity(category);
				}
				else
				{
					return Results.ErrorResult("You are not allowed to add Categories if your not associated with a business entity.");
				}
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}
		public Result Update(Category category)
		{
			try
			{
				if (category == null) throw new ArgumentNullException("category");
				var name = category.Name;
				var description = category.Description;
				return UpdateEntityProperties<Category>(category.Id, 
					c => new Category
					{
						Name = name,
						Description = description,
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
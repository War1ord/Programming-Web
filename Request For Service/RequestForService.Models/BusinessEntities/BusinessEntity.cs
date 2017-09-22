using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.BusinessEntities
{
	public class BusinessEntity : Base.CreatedByBase
	{
		private bool? isParent;
		private List<Users.User> users;
		private List<BusinessEntity> childEntities;

		[Required]
		public string Name { get; set; }
		public string Description { get; set; }

		[Display(Name = "Parent Entity")]
		public Guid? ParentEntityId { get; set; }
		[Display(Name = "Industry Level")]
		public Guid IndustryLevelId { get; set; }

		[ForeignKey("ParentEntityId")]
		public BusinessEntity ParentEntity { get; set; }
		[ForeignKey("IndustryLevelId")]
		public IndustryLevel IndustryLevel { get; set; }

		public List<Users.User> Users
		{
			get { return users ?? (users = new List<Users.User>()); }
			set { users = value; }
		}
		public List<BusinessEntity> ChildEntities
		{
			get { return childEntities ?? (childEntities = new List<BusinessEntity>()); }
			set { childEntities = value; }
		}

		[NotMapped]
		[Display(Name = "Is Root Entity")]
		public bool IsRootEntity
		{
			get { return isParent ?? (isParent = ParentEntityId == null).Value; }
			set { isParent = value; }
		}
	}
}
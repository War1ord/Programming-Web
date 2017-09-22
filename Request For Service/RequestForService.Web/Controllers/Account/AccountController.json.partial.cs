using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using RequestForService.Common.Extensions;

namespace RequestForService.Web.Controllers.Account
{
	public partial class AccountController
	{
		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult SaveUserPersonTitle(string id, string value)
		{
			return SaveField<RequestForService.Models.Users.User, DataTypes.Enums.Title?>(id, value, u => u.Person.Title);
		}
		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult SaveUserPersonGender(string id, string value)
		{
			return SaveField<RequestForService.Models.Users.User, DataTypes.Enums.Gender?>(id, value, u => u.Person.Gender);
		}
		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult SaveUserJobDetailsJobTitle(string id, string value)
		{
			return SaveField<RequestForService.Models.Users.User, DataTypes.Enums.JobTitle?>(id, value, u => u.JobDetails.JobTitle);
		}
		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult SaveUserReceiveNewsletters(string id, string value)
		{
			return SaveField<RequestForService.Models.Users.User, bool>(id, value, u => u.ReceiveNewsletters);
		}
		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult SaveUserPersonFirstName(string id, string value)
		{
			return SaveField<RequestForService.Models.Users.User, string>(id, value, u => u.Person.FirstName);
		}
		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult SaveUserPersonMiddleName(string id, string value)
		{
			return SaveField<RequestForService.Models.Users.User, string>(id, value, u => u.Person.MiddleName);
		}
		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult SaveUserPersonLastName(string id, string value)
		{
			return SaveField<RequestForService.Models.Users.User, string>(id, value, u => u.Person.LastName);
		}
		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult SaveUserContactDetailsOffice(string id, string value)
		{
			return SaveField<RequestForService.Models.Users.User, string>(id, value, u => u.ContactDetails.Office);
		}
		[HttpPost, ValidateAntiForgeryToken]
		public JsonResult SaveUserContactDetailsMobile(string id, string value)
		{
			return SaveField<RequestForService.Models.Users.User, string>(id, value, u => u.ContactDetails.Mobile);
		}

		private JsonResult SaveField<T, TProp>(string id, string value, Expression<Func<T, TProp>> propertyExpression)
			where T : RequestForService.Models.Base.IdBase
		{
			Guid idValue;
			var isValidId = Guid.TryParse(id, out idValue);
			TProp propertyValue;
			var isValidValue = value.TryTo(out propertyValue);
			if (isValidId && isValidValue)
			{
				var result = Business.UpdateEntityProperty(idValue, propertyValue, propertyExpression);
				return Json(result, JsonRequestBehavior.AllowGet);
			}
			else
			{
				if (Nullable.GetUnderlyingType(typeof(TProp)) != null)
				{
					var result = Business.UpdateEntityProperty(idValue, default(TProp), propertyExpression);
					return Json(result, JsonRequestBehavior.AllowGet);
				}
				else
				{
					return Json(RequestForService.Business.Models.Results.ErrorResult("Invalid value."),
						JsonRequestBehavior.AllowGet);
				}
			}
		}

	}
}
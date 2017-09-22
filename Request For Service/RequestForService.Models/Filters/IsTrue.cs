using System.ComponentModel.DataAnnotations;

namespace RequestForService.Models.Filters
{
	public class IsTrue : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			if (value == null) return false;
			if (value.GetType() != typeof(bool)) throw new System.InvalidOperationException("Can only be used on boolean properties.");
			return (bool)value;
		}
	}
}
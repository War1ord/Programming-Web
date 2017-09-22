using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Base
{
	public abstract class EnumBase<T> : ModelBase where T : struct
	{
		public EnumBase()
		{
			//Set default values here.
		}
		public EnumBase(T key, string value, string description)
		{
			Key = key;
			Value = value;
			Description = description;
		}

		[Display(Name = nameof(Key))]
		[Index(IsUnique = true)]
		public T Key { get; set; }
		[Display(Name = nameof(Value))]
		[StringLength(150, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
		public string Value { get; set; }
		[Display(Name = nameof(Description))]
		[StringLength(250, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
		public string Description { get; set; }

		public override string ToString()
		{
			return !string.IsNullOrWhiteSpace(Description)
					? Description
					: !string.IsNullOrWhiteSpace(Value)
						? Value
						: null;
		}
	}
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.Base
{
	public abstract class EnumBase<T> : IdBase where T : struct 
	{
		public EnumBase()
		{
			//Set default values here.
		}
		public EnumBase(T key, string keyValue, string keyDescription)
		{
			Key = key;
			KeyValue = keyValue;
			KeyDescription = keyDescription;
		}

		[Display(Name = "Key")]
		[Index(IsUnique = true)]
		public T Key { get; set; }
		[Display(Name = "Value")]
		[StringLength(150, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
		public string KeyValue { get; set; }
		[Display(Name = "Description")]
		[StringLength(250, ErrorMessage = "The {0} cannot be longer than {1} characters.")]
		public string KeyDescription { get; set; }

		public override string ToString()
		{
			return KeyDescription;
		}
	}
}
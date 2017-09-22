using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.Errors
{
	/// <summary>
	/// an Entity to contain all exceptions thrown in the database
	/// </summary>
	public class ErrorLog : Base.IdBase
	{
		/// <summary>
		/// Gets or sets the host.
		/// </summary>
		/// <value>
		/// The host.
		/// </value>
		public string Host { get; set; }
		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>
		/// The version.
		/// </value>
		public string Version { get; set; }
		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>
		/// The message.
		/// </value>
		[DataType(DataType.MultilineText)]
		public string Message { get; set; }
		/// <summary>
		/// Gets or sets the namespace.
		/// </summary>
		/// <value>
		/// The namespace.
		/// </value>
		public string Namespace { get; set; }
		/// <summary>
		/// Gets or sets the class.
		/// </summary>
		/// <value>
		/// The class.
		/// </value>
		public string Class { get; set; }
		/// <summary>
		/// Gets or sets the method.
		/// </summary>
		/// <value>
		/// The method.
		/// </value>
		public string Method { get; set; }
		/// <summary>
		/// Gets or sets the exception.
		/// </summary>
		/// <value>
		/// The exception.
		/// </value>
		[DataType(DataType.MultilineText)]
		public string StackTrace { get; set; }
		/// <summary>
		/// Gets or sets the created by user identifier.
		/// </summary>
		/// <value>
		/// The created by user identifier.
		/// </value>
		[Display(Name = "Created By")]
		public Guid? CreatedByUserId { get; set; }
		/// <summary>
		/// Gets or sets the created by user.
		/// </summary>
		/// <value>
		/// The created by user.
		/// </value>
		[ForeignKey("CreatedByUserId")]
		public Users.User CreatedByUser { get; set; }
	}
}
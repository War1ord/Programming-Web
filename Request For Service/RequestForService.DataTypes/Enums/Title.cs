using System.ComponentModel;

namespace RequestForService.DataTypes.Enums
{
	public enum Title
	{
		[Description("Mr")]
		Mr = 1,
		[Description("Mrs")]
		Mrs = 2,
		[Description("Miss")]
		Miss = 3,
		[Description("Ms")]
		Ms = 5,
		[Description("DR")]
		Dr = 6,
		[Description("Prof")]
		Prof = 7,
		[Description("Ds")]
		Ds = 8,
		[Description("Other")]
		Other = 99,
	}
}
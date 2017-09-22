namespace RequestForService.Web.ViewModels.BusinessEntities
{
	public class BusinessEntityItemViewModel : Base.ViewModelBase
	{
		private RequestForService.Models.BusinessEntities.BusinessEntity _businessEntity;

		public System.Guid? ParentEntityId { get; set; }

		public RequestForService.Models.BusinessEntities.BusinessEntity BusinessEntity
		{
			get
			{
				if (_businessEntity != null)
				{
					_businessEntity.ParentEntityId = ParentEntityId;
				}
				return _businessEntity;
			}
			set
			{
				_businessEntity = value;
				if (_businessEntity != null)
				{
					_businessEntity.ParentEntityId = ParentEntityId;
				}
			}
		}
	}
}
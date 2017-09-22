using System;
using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Admin.HourlyRates
{
    public partial class HourlyRatesController : Base.AuthenticationBaseController
    {
	    private Business.Services.Admin.HourlyRatesService _business;
		private Business.Services.Admin.HourlyRatesService Business
	    {
			get { return _business ?? (_business = new Business.Services.Admin.HourlyRatesService(UserId)); }
	    }

	    public ActionResult Index()
	    {
		    var model = new ViewModels.HourlyRates.HourlyRatesListViewModel
		    {
			    List = Business.GetEntityList(i => i.Price, false).Entity
		    };
			return View(model);
        }

	    public ActionResult Add()
	    {
			return View();
		}

	    public ActionResult Update(Guid id)
	    {
		    var model = new ViewModels.HourlyRates.HourlyRateItemViewModel
		    {
				Item = Business.GetEntity(id).Entity
		    };
			return View(model);
		}

	    protected override void Dispose(bool disposing)
	    {
		    if (disposing)
		    {
			    if (_business != null)
			    {
				    _business.Dispose();
				    _business = null;
			    }
		    }
		    base.Dispose(disposing);
	    }
    }
}
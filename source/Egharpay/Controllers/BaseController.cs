using System.Web;
using System.Web.Mvc;
using Egharpay.Business;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Egharpay.Business.Interfaces;
using Egharpay.Entity.Dto;
using Egharpay.Models;
using Egharpay.Models.Authorization;

namespace Egharpay.Controllers
{
    public class BaseController : Controller
    {
        private IEgharpayBusinessService _EgharpayBusinessService;
        private ApplicationUserManager _userManager;
        private ApplicationUser _applicationUser;

        protected IEgharpayBusinessService EgharpayBusinessService
        {
            get
            {
                return _EgharpayBusinessService;
            }
        }

        public BaseController()
        {
        }

        public BaseController(IEgharpayBusinessService EgharpayBusinessService)
        {
            _EgharpayBusinessService = EgharpayBusinessService;
        }



        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        protected ApplicationUser ApplicationUser
        {
            get
            {
                return _applicationUser ?? UserManager.FindById(User?.Identity?.GetUserId());
            }
            set
            {
                _applicationUser = value;
            }
        }

        protected TenantOrganisation Organisation => UserManager.TenantOrganisation;


        protected int UserOrganisationId => ApplicationUser?.OrganisationId ?? 0;
        protected int UserPersonnelId => ApplicationUser?.PersonnelId ?? 0;
        protected int UserCentreId => ApplicationUser?.CentreId ?? 0;
        // protected int UserEnquiryId => ApplicationUser?.EnquiryId?? 0;

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewModel = filterContext.Controller.ViewData.Model as BaseViewModel;

            if (viewModel != null)
            {
                var organisation = UserManager.TenantOrganisation;
                viewModel.OrganisationName = organisation?.Name ?? string.Empty;
                viewModel.CentreName = EgharpayBusinessService.RetrieveCentre(UserOrganisationId, UserCentreId, e => true)?.Name ?? viewModel.OrganisationName;
                viewModel.PersonnelId = UserPersonnelId;
                viewModel.CentreId = UserCentreId;
                // viewModel.EnquiryId = UserEnquiryId;

            }

            base.OnActionExecuted(filterContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_EgharpayBusinessService != null)
                    _EgharpayBusinessService = null;

                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
                if (_applicationUser != null)
                    _applicationUser = null;

            }

            base.Dispose(disposing);
        }
    }
}
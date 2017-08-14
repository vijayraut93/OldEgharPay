using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Extensions;
using Egharpay.Models;


namespace Egharpay.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(IEgharpayBusinessService egharpayBusinessService) : base(egharpayBusinessService)
        {
        }

        public ActionResult Index()

        {
            var organisationId = UserOrganisationId;
            var personnelId = UserPersonnelId;
            var centreId = UserCentreId;
            bool isSuperAdmin = User.IsSuperAdmin();
            var permissions = EgharpayBusinessService.RetrievePersonnelPermissions(User.IsInRole("Admin"), organisationId, personnelId);
            if (User.IsInRole("User") && !permissions.IsManager)
                return RedirectToAction("Profile", "Personnel", new { id = personnelId });

            var viewModel = new HomeViewModel
            {
                Permissions = permissions,
            };

            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View(new BaseViewModel());
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View(new BaseViewModel());
        }

        [HttpPost]
        public ActionResult GetCentres()
        {
            var data = EgharpayBusinessService.RetrieveCentres(UserOrganisationId);
            return this.JsonNet(data);
        }
    }
}
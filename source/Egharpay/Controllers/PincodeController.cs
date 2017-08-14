using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Egharpay.Business.Interfaces;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;

namespace Egharpay.Controllers
{
    public class PincodeController : BaseController
    {
        private IPincodeBusinessService _pincodeBusinessService;
        private IEgharpayBusinessService _egharpayBusinessService;

        public PincodeController(IPincodeBusinessService pincodeBusinessService, IEgharpayBusinessService egharpayBusinessService) : base(egharpayBusinessService)
        {
            _pincodeBusinessService = pincodeBusinessService;
            _egharpayBusinessService = egharpayBusinessService;
        }

        // GET: Pincode
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _pincodeBusinessService.RetrievePincodes(orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(await _pincodeBusinessService.Search(searchKeyword, orderBy, paging));
        }
    }
}
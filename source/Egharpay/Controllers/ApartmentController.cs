using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Egharpay.Attributes;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;

namespace Egharpay.Controllers
{
    [Authorize]
    public class ApartmentController : BaseController
    {
        private IApartmentBusinessService _apartmentBusinessService;
        private IEgharpayBusinessService _egharpayBusinessService;
        public ApartmentController(IApartmentBusinessService apartmentBusinessService, IEgharpayBusinessService egharpayBusinessService) : base(egharpayBusinessService)
        {
            _apartmentBusinessService = apartmentBusinessService;
            _egharpayBusinessService = egharpayBusinessService;
        }

        // GET: Apartment
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }

        // GET: Apartment/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            var city = await _apartmentBusinessService.RetrieveCities();
            var cities = city.Items.ToList();
            var state = await _apartmentBusinessService.RetrieveStates(null,null);
            var states = state.Items.ToList();
            var municipalCorporation = await _apartmentBusinessService.RetrieveMunicipalCorporations();
            var municipalCorporations = municipalCorporation.Items.ToList();
            var viewModel = new ApartmentViewModel
            {
                Apartment = new Apartment(),
                Cities = new SelectList(cities, "CityId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                MunicipalCorporations = new SelectList(municipalCorporations, "MunicipalCorporationId", "Name")
            };
            return View(viewModel);
        }

        // POST: Apartment/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApartmentViewModel apartmentViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create Apartment
                apartmentViewModel.Apartment.CreatedDate=DateTime.UtcNow.Date;
                apartmentViewModel.Apartment.CreatedBy = UserPersonnelId;
                var result = await _apartmentBusinessService.CreateApartment(apartmentViewModel.Apartment);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Exception);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(apartmentViewModel);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var apartment = await _apartmentBusinessService.RetrieveApartment(id.Value);
            var city = await _apartmentBusinessService.RetrieveCities();
            var cities = city.Items.ToList();
            var state = await _apartmentBusinessService.RetrieveStates(null, null);
            var states = state.Items.ToList();
            var municipalCorporation = await _apartmentBusinessService.RetrieveMunicipalCorporations();
            var municipalCorporations = municipalCorporation.Items.ToList();
            if (apartment == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ApartmentViewModel
            {
                Apartment = apartment,
                Cities = new SelectList(cities, "CityId", "Name"),
                States = new SelectList(states, "StateId", "Name"),
                MunicipalCorporations = new SelectList(municipalCorporations, "MunicipalCorporationId", "Name")
            };
            return View(viewModel);
        }

        // POST: Apartment/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApartmentViewModel apartmentViewModel)
        {
            if (ModelState.IsValid)
            {
                //Create Apartment
                var result = await _apartmentBusinessService.UpdateApartment(apartmentViewModel.Apartment);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Exception);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(apartmentViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> List(Paging paging, List<OrderBy> orderBy)
        {
            var data = await _apartmentBusinessService.RetrieveApartments(orderBy, paging);
            return this.JsonNet(data);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string searchKeyword, Paging paging, List<OrderBy> orderBy)
        {
            return this.JsonNet(await _apartmentBusinessService.Search(searchKeyword, orderBy, paging));
        }
    }
}
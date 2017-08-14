using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Services
{
    public partial class ApartmentBusinessService : IApartmentBusinessService
    {
        protected IApartmentDataService _dataService;

        public ApartmentBusinessService(IApartmentDataService dataService)
        {
            _dataService = dataService;
        }

        #region Create

        public async Task<ValidationResult<Apartment>> CreateApartment(Apartment apartment)
        {
            ValidationResult<Apartment> validationResult = new ValidationResult<Apartment>();
            try
            {
                await _dataService.CreateAsync(apartment);
                validationResult.Entity = apartment;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        #endregion

        #region Retrieve

        public async Task<Apartment> RetrieveApartment(int apartmentId)
        {
            var apartments = await _dataService.RetrieveAsync<Apartment>(a => a.ApartmentId == apartmentId);
            return apartments.FirstOrDefault();
        }

        public async Task<PagedResult<ApartmentDataGrid>> RetrieveApartments(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var apartment = await _dataService.RetrievePagedResultAsync<ApartmentDataGrid>(a => true, orderBy, paging);
            return apartment;
        }

        public async Task<PagedResult<ApartmentDataGrid>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return await _dataService.RetrievePagedResultAsync<ApartmentDataGrid>(a => a.SearchField.ToLower().Contains(term.ToLower()), orderBy, paging);
        }

        public async Task<PagedResult<City>> RetrieveCities(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var city = await _dataService.RetrievePagedResultAsync<City>(a => true, orderBy, paging);
            return city;
        }

        public async Task<PagedResult<State>> RetrieveStates(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var state = await _dataService.RetrievePagedResultAsync<State>(a => true, orderBy, paging);
            return state;
        }

        public async Task<PagedResult<MunicipalCorporation>> RetrieveMunicipalCorporations(List<OrderBy> orderBy = null, Paging paging = null)
        {
            var municipalCorporation = await _dataService.RetrievePagedResultAsync<MunicipalCorporation>(a => true, orderBy, paging);
            return municipalCorporation;
        }

        #endregion

        #region Update

        public async Task<ValidationResult<Apartment>> UpdateApartment(Apartment apartment)
        {
            ValidationResult<Apartment> validationResult = new ValidationResult<Apartment>();
            try
            {
                await _dataService.UpdateAsync(apartment);
                validationResult.Entity = apartment;
            }
            catch (Exception ex)
            {
                validationResult.Succeeded = false;
                validationResult.Errors = new List<string> { ex.InnerMessage() };
                validationResult.Exception = ex;
            }
            return validationResult;
        }

        #endregion

        #region Delete

        public async Task<bool> CanDeleteApartment(int apartmentId)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteApartment(int apartmentId)
        {
            try
            {
                await _dataService.DeleteByIdAsync<Apartment>(apartmentId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}

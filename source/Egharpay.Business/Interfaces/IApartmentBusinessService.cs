using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IApartmentBusinessService
    {
        //Create
        Task<ValidationResult<Apartment>> CreateApartment(Apartment apartment);

        //Retrieve
        Task<bool> CanDeleteApartment(int apartmentId);
        Task<Apartment> RetrieveApartment(int apartmentId);
        Task<PagedResult<ApartmentDataGrid>> RetrieveApartments(List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<ApartmentDataGrid>> Search(string term, List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<City>> RetrieveCities(List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<State>> RetrieveStates(List<OrderBy> orderBy = null, Paging paging = null);
        Task<PagedResult<MunicipalCorporation>> RetrieveMunicipalCorporations(List<OrderBy> orderBy = null, Paging paging = null);

        //Update
        Task<ValidationResult<Apartment>> UpdateApartment(Apartment apartment);

        //Delete
        Task<bool> DeleteApartment(int apartmentId);
    }
}

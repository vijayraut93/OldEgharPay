using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Data.Interfaces;

namespace Egharpay.Business.Services
{
    public partial class PersonnelBusinessService : IPersonnelBusinessService
    {
        protected IPersonnelTestDataService _dataService;

        public PersonnelBusinessService(IPersonnelTestDataService dataService)
        {
            _dataService = dataService;
        }

        #region Create

        public async Task<ValidationResult<Personnel>> CreatePersonnel(Personnel personnel)
        {
            var validationResult = await PersonnelAlreadyExists(personnel.Email);
            if (!validationResult.Succeeded)
            {
                return validationResult;
            }
            try
            {
                await _dataService.CreateAsync(personnel);
                validationResult.Entity = personnel;
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

        public async Task<bool> CanDeletePersonnel(int personnelId)
        {
            return await Task.FromResult(true);
        }

        public async Task<Personnel> RetrievePersonnel(int centreId, int personnelId)
        {
            var personnels = await _dataService.RetrieveAsync<Personnel>(a => a.PersonnelId == personnelId && (a.CentreId == centreId));
            return personnels.FirstOrDefault();
        }


        public async Task<PagedResult<PersonnelGrid>> RetrievePersonnels(int centreId, List<OrderBy> orderBy = null, Paging paging = null)
        {
            var personnel = await _dataService.RetrievePagedResultAsync<PersonnelGrid>(a => a.CentreId == centreId, orderBy, paging);
            return personnel;
        }

        private async Task<ValidationResult<Personnel>> PersonnelAlreadyExists(string email)
        {
            var personnels = await _dataService.RetrieveAsync<Personnel>(a => a.Email.Trim().ToLower() == email.Trim().ToLower());
            var alreadyExists = personnels.Any();
            return new ValidationResult<Personnel>
            {
                Succeeded = !alreadyExists,
                Errors = alreadyExists ? new List<string> { "Personnel already exists." } : null
            };
        }

        public async Task<PagedResult<PersonnelGrid>> Search(int centreId,string term, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return await _dataService.RetrievePagedResultAsync<PersonnelGrid>(a => a.CentreId == centreId && a.SearchField.ToLower().Contains(term.ToLower()), orderBy, paging);
        }


        #endregion

        #region Update

        public async Task<ValidationResult<Personnel>> UpdatePersonnel(Personnel personnel)
        {
            var validationResult = await PersonnelAlreadyExists(personnel.Email);
            if (!validationResult.Succeeded)
            {
                return validationResult;
            }
            try
            {
                await _dataService.UpdateAsync(personnel);
                validationResult.Entity = personnel;
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

        public async Task<bool> DeletePersonnel(int id)
        {
            try
            {
                await _dataService.DeleteByIdAsync<Personnel>(id);
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

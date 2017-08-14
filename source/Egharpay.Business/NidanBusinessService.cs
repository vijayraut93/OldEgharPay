using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Egharpay.Business.Interfaces;
using Egharpay.Data.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;


namespace Egharpay.Business
{
    public partial class EgharpayBusinessService : IEgharpayBusinessService, ITenantOrganisationService
    {
        private readonly IPersonnelDataService _personnelDataService;
        private readonly ICacheProvider _cacheProvider;
        private ITemplateService _templateService;
        private IEmailService _emailService;

        //   private IDocumentServiceRestClient _documentServiceAPI;
        private enum ShowColour
        {
            Company = 1,
            Division,
            Department
        };

        private const string OrganisationCacheKey = "Organisations";
        private const string OrganisationEmploymentsTreeKey = "OrganisationEmploymentsTree";
        private const string AbsenceStatusTemplateKey = "HRAbsenceStatus";
        private object lockObject = new object();
        //readonly string PersonnelPhotoKey = "PersonnelPhoto";
        //readonly string PersonnelProfileCategory = "ProfileImage";

        private readonly DateTime _today = new DateTime(DateTime.UtcNow.Date.Year, DateTime.UtcNow.Date.Month,
            DateTime.UtcNow.Date.Day, 0, 0, 0);

        public EgharpayBusinessService(IPersonnelDataService egharpayDataService, ICacheProvider cacheProvider,
            ITemplateService templateService, IEmailService emailService)
        {
            _personnelDataService = egharpayDataService;
            _cacheProvider = cacheProvider;
            _templateService = templateService;
            _emailService = emailService;
            //_documentServiceAPI = documentServiceAPI;
        }

        #region // Create

        public Personnel CreatePersonnel(int organisationId, Personnel personnel)
        {
            return _personnelDataService.CreatePersonnel(organisationId, personnel);
        }

        public Centre CreateCentre(int organisationId, Centre centre)
        {
            return _personnelDataService.CreateCentre(organisationId, centre);
        }

        #endregion

        #region // Retrieve

        public Personnel RetrievePersonnel(int organisationId, int personnelId)
        {
            var personnel = _personnelDataService.RetrievePersonnel(organisationId, personnelId, p => true);
            return personnel;
        }

        public PagedResult<Personnel> RetrievePersonnel(int organisationId, int centreId, List<OrderBy> orderBy,
            Paging paging)
        {
            return _personnelDataService.RetrievePersonnel(organisationId, p => p.CentreId == centreId, orderBy, paging);
        }

        public Organisation RetrieveOrganisation(int organisationId)
        {
            EnsureOrganisationCache();
            var organisation = (List<Organisation>)_cacheProvider.Get(OrganisationCacheKey);
            return organisation.FirstOrDefault(o => o.OrganisationId == organisationId);
        }

        public Organisation RetrieveOrganisation(string name)
        {
            EnsureOrganisationCache();
            var organisation = (List<Organisation>)_cacheProvider.Get(OrganisationCacheKey);
            return organisation.FirstOrDefault(o => o.Name == name);
        }

        private void EnsureOrganisationCache()
        {
            lock (lockObject)
            {
                if (_cacheProvider.IsSet(OrganisationCacheKey))
                    return;

                var organisation = _personnelDataService.RetrieveOrganisations().ToList();
                _cacheProvider.Set(OrganisationCacheKey, organisation, ConfigHelper.CacheTimeout);
            }
        }

        public PagedResult<PersonnelSearchField> RetrievePersonnelBySearchKeyword(int organisationId,
            string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _personnelDataService.RetrievePersonnelBySearchKeyword(organisationId, searchKeyword, orderBy, paging);
        }

        public IEnumerable<Personnel> RetrieveReportsToPersonnel(int organisationId, int personnelId)
        {
            return _personnelDataService.RetrievePersonnel(organisationId, p => p.PersonnelId != personnelId).Items;
        }

        public IEnumerable<TenantOrganisation> RetrieveTenantOrganisations()
        {
            var hosts = _personnelDataService.RetrieveHosts();

            return hosts
                .Select(h => new TenantOrganisation
                {
                    OrganisationId = h.OrganisationId,
                    Name = h.Organisation.Name,
                    HostName = h.HostName
                })
                .ToList();
        }

        public IAuthorisation RetrieveUserAuthorisation(string aspNetUserId)
        {
            var userAuthorisation = _personnelDataService.RetrieveUserAuthorisation(aspNetUserId);
            if (userAuthorisation == null)
                return null;

            return new Authorisation
            {
                OrganisationId = userAuthorisation.OrganisationId,
                RoleId = int.Parse(userAuthorisation.RoleId)
            };
        }

        public Permissions RetrievePersonnelPermissions(bool isAdmin, int organisationId, int userPersonnelId,
            int? personnelId = null)
        {
            var isManagerOf = true;
            var isPerson = userPersonnelId == personnelId;
            var personnelIsTerminated = false;

            return new Permissions
            {
                IsAdmin = isAdmin,
                IsManager = isManagerOf,
                CanViewProfile = isAdmin || isManagerOf || isPerson,
                CanEditProfile = isAdmin || (!personnelIsTerminated && isPerson),
                CanCreateAbsence = isAdmin || (!personnelIsTerminated && (isManagerOf || isPerson)),
                CanEditAbsence = isAdmin || isManagerOf || (!personnelIsTerminated && isPerson),
                CanCancelAbsence = isAdmin || isManagerOf || (!personnelIsTerminated && isPerson),
                CanApproveAbsence = isAdmin || isManagerOf,
                CanEditEntitlements = isAdmin,
                CanEditEmployments = isAdmin
            };
        }

        public Centre RetrieveCentre(int organisationId, int centreId, Expression<Func<Centre, bool>> predicate)
        {
            var centre = _personnelDataService.RetrieveCentre(organisationId, centreId, p => true);
            return centre;
        }

        public Centre RetrieveCentre(int organisationId, int id)
        {
            return _personnelDataService.RetrieveCentre(organisationId, id, p => true);
        }

        public List<Centre> RetrieveCentres(int organisationId, Expression<Func<Centre, bool>> predicate)
        {
            return _personnelDataService.RetrieveCentres(organisationId, e => true).Items.ToList();
        }

        public PagedResult<Centre> RetrieveCentres(int organisationId, List<OrderBy> orderBy = null,
            Paging paging = null)
        {
            return _personnelDataService.RetrieveCentres(organisationId, p => true, orderBy, paging);
        }
        #endregion

        #region // Update


        //Update

        public Personnel UpdatePersonnel(int organisationId, Personnel personnel)
        {
            return _personnelDataService.UpdateOrganisationEntityEntry(organisationId, personnel);
        }

        #endregion

        #region //Delete

        //Delete
        public void DeletePersonnel(int organisationId, int personnelId)
        {
            _personnelDataService.Delete<Personnel>(organisationId, e => e.PersonnelId == personnelId);
        }

        #endregion

        //Document

        #region Document

        public List<DocumentType> RetrieveDocumentTypes(int organisationId)
        {
            return _personnelDataService.RetrieveDocumentTypes(organisationId).ToList();
        }

        public Document CreateDocument(int organisationId, int centreId, int documentTypeId, string filePath,
            string studentCode, string studentName, string description, string fileName, Guid guid)
        {
            var document = new Document()
            {
                OrganisationId = organisationId,
                CentreId = centreId,
                DocumentTypeId = documentTypeId,
                StudentCode = studentCode,
                CreatedDateTime = DateTime.UtcNow.Date,
                Description = description,
                FileName = fileName,
                Location = filePath,
                StudentName = studentName,
                Guid = guid
            };

            return _personnelDataService.Create<Document>(organisationId, document);
        }

        public PagedResult<Document> RetrieveDocuments(int organisationId, Expression<Func<Document, bool>> predicate,
            List<OrderBy> orderBy = null, Paging paging = null)
        {
            return _personnelDataService.RetrieveDocuments(organisationId, predicate, orderBy, paging);
        }

        public Document RetrieveDocument(int organisationId, Guid documentGuid)
        {
            return _personnelDataService.RetrieveDocument(organisationId, documentGuid);
        }

        #endregion

        //Template

    }
}


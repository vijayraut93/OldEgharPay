using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Egharpay.Business.Models;
using Egharpay.Entity;
using Egharpay.Entity.Dto;

namespace Egharpay.Business.Interfaces
{
    public interface IEgharpayBusinessService
    {
        //Create
        Personnel CreatePersonnel(int organisationId, Personnel personnel);
        Centre CreateCentre(int organisationId, Centre centre);
        Organisation RetrieveOrganisation(int organisationId);
        IAuthorisation RetrieveUserAuthorisation(string aspNetUserId);
        Permissions RetrievePersonnelPermissions(bool isAdmin, int organisationId, int userPersonnelId, int? personnelId = null);
        PagedResult<Personnel> RetrievePersonnel(int organisationId, int centreId, List<OrderBy> orderBy, Paging paging);
        Personnel RetrievePersonnel(int organisationId, int id);
        PagedResult<PersonnelSearchField> RetrievePersonnelBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null);
        PagedResult<Centre> RetrieveCentres(int organisationId, List<OrderBy> orderBy = null, Paging paging = null);
        Centre RetrieveCentre(int organisationId, int centreId, Expression<Func<Centre, bool>> predicate);
        Centre RetrieveCentre(int organisationId, int id);
        List<Centre> RetrieveCentres(int organisationId, Expression<Func<Centre, bool>> predicate);

        // Update
        Personnel UpdatePersonnel(int organisationId, Personnel personnel);
        //Delete
        void DeletePersonnel(int organisationId, int personnelId);
        
        //Document
        List<DocumentType> RetrieveDocumentTypes(int organisationId);
        PagedResult<Document> RetrieveDocuments(int organisationId, Expression<Func<Document, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null);
        Document RetrieveDocument(int organisationId, Guid documentGuid);

    }
}

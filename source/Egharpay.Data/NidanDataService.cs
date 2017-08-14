using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Egharpay.Entity;
using Egharpay.Data.Extensions;
using Egharpay.Data.Interfaces;
using Egharpay.Entity.Dto;
using System.Configuration;
using Egharpay.Data.Models;

namespace Egharpay.Data
{
    public partial class PersonnelDataService : IPersonnelDataService
    {
        protected readonly IEgharpayDatabaseFactory<EgharpayDatabase> _databaseFactory;
        private TransactionScope ReadUncommitedTransactionScope => new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted });

        public PersonnelDataService(IEgharpayDatabaseFactory<EgharpayDatabase> factory)
        {
            _databaseFactory = factory;
        }

        #region // Create

        public Centre CreateCentre(int organisationId, Centre centre)
        {
            using (var context = _databaseFactory.Create())
            {
                centre = context.Centres.Add(centre);
                context.SaveChanges();

                return centre;
            }
        }

        public Personnel CreatePersonnel(int organisationId, Personnel personnel)
        {
            using (var context = _databaseFactory.Create())
            {
                personnel = context.Personnels.Add(personnel);
                context.SaveChanges();

                return personnel;
            }

        }

        public T Create<T>(int organisationId, T t) where T : class
        {
            using (var context = _databaseFactory.Create())
            {
                t = context.Set<T>().Add(t);
                context.SaveChanges();
                return t;
            }
        }

        public void Create<T>(int organisationId, IEnumerable<T> t) where T : class
        {
            using (var context = _databaseFactory.Create())
            {
                context.Set<T>().AddRange(t);
                context.SaveChanges();
            }
        }
        #endregion

        #region // Retrieve

        public bool PersonnelEmploymentHasAbsences(int organisationId, int personnelId, int employmentId)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                return true;
            }
        }

        public IEnumerable<Host> RetrieveHosts()
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.CreateContext())
            {
                return context
                    .Hosts
                    .Include(o => o.Organisation)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public IEnumerable<Organisation> RetrieveOrganisations()
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                return context
                    .Organisations
                    .Include(o => o.Hosts)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public Personnel RetrievePersonnel(int organisationId, int personnelId, Expression<Func<Personnel, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                return context
                    .Personnels
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.PersonnelId == personnelId);

            }
        }

        public IEnumerable<Personnel> RetrievePersonnel(int organisationId, IEnumerable<int> companyIds, IEnumerable<int> departmentIds, IEnumerable<int> divisionIds)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                var personnel = context
                    .Personnels
                    //.Include(p => p.Employments.Select(e => e.Division))
                    .AsNoTracking();

                //if (companyIds != null && companyIds.Any())
                //    personnel = personnel.Where(p => companyIds.Contains(p.Employments.OrderByDescending(by => by.StartDate).FirstOrDefault().Division.CompanyId));

                //if (departmentIds != null && departmentIds.Any())
                //    personnel = personnel.Where(p => departmentIds.Contains(p.Employments.OrderByDescending(by => by.StartDate).FirstOrDefault().DepartmentId));

                //if (divisionIds != null && divisionIds.Any())
                //    personnel = personnel.Where(p => divisionIds.Contains(p.Employments.OrderByDescending(by => by.StartDate).FirstOrDefault().DivisionId));

                return personnel.ToList();
            }
        }

        public PagedResult<Personnel> RetrievePersonnel(int organisationId, Expression<Func<Personnel, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {

                return context
                    .Personnels
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "Forenames",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public UserAuthorisationFilter RetrieveUserAuthorisation(string aspNetUserId)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                return context
                    .UserAuthorisationFilters
                    .AsNoTracking()
                    .FirstOrDefault(u => u.AspNetUsersId == aspNetUserId);
            }
        }

        public PagedResult<PersonnelSearchField> RetrievePersonnelBySearchKeyword(int organisationId, string searchKeyword, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                var category = new SqlParameter("@SearchKeyword", searchKeyword);

                return context.Database
                    .SqlQuery<PersonnelSearchField>("SearchPersonnel @SearchKeyword", category).ToList().AsQueryable().
                    OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "Forenames",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public List<T> Retrieve<T>(int organisationId, Expression<Func<T, bool>> predicate) where T : class
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                var returnItems = context.Set<T>().Where(predicate).ToList();
                return returnItems;
            }
        }

        public Centre RetrieveCentre(int organisationId, int centreId, Expression<Func<Centre, bool>> predicate)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                return context
                    .Centres
                    .AsNoTracking()
                    .Where(predicate)
                    .SingleOrDefault(p => p.CentreId == centreId);

            }
        }

        public PagedResult<Centre> RetrieveCentres(int organisationId, Expression<Func<Centre, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {

                return context
                    .Centres
                    .Include(p => p.Organisation)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "Name",
                            Direction = System.ComponentModel.ListSortDirection.Ascending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Template RetrieveTemplateDetails(int organisationId, string name)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                var template = context
                    .Templates
                    .AsNoTracking()
                    .SingleOrDefault(p => p.Name.ToLower() == name.ToLower());

                if (template != null)
                {
                    return new Template
                    {
                        Name = template.Name,
                        FileName = template.FileName,
                        Type = template.Type,
                        FilePath = Path.Combine(ConfigurationManager.AppSettings["TemplateRootFilePath"], template.FileName)
                    };
                }
                return null;

            }
        }
        #endregion

        #region // Update


        public T UpdateEntityEntry<T>(T t) where T : class
        {
            using (var context = _databaseFactory.Create())
            {
                context.Entry(t).State = EntityState.Modified;
                context.SaveChanges();

                return t;
            }
        }

        public T UpdateOrganisationEntityEntry<T>(int organisationId, T t) where T : class
        {
            using (var context = _databaseFactory.Create())
            {
                //context.Set<T>().Attach(t);
                context.Entry(t).State = EntityState.Modified;
                context.SaveChanges();

                return t;
            }
        }

        #endregion

        #region //Delete
        public void Delete<T>(int organisationId, Expression<Func<T, bool>> predicate) where T : class
        {
            using (var context = _databaseFactory.Create())
            {
                var items = context.Set<T>().Where(predicate).FirstOrDefault();
                if (items != null) context.Set<T>().Remove(items);
                context.SaveChanges();
            }
        }

        #endregion

        //Document

        public IEnumerable<DocumentType> RetrieveDocumentTypes(int organisationId)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                return context
                    .DocumentTypes
                    .AsNoTracking().ToList();
            }
        }

        public IEnumerable<Document> RetrieveDocuments(int organisationId, int centreId, string category, string studentCode)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                return context
                    .Documents
                    .Include(e => e.DocumentType)
                    .AsNoTracking()
                    .Where(e =>
                     e.CentreId == centreId && e.DocumentType.Name.ToLower() == category.ToLower() &&
                     e.StudentCode == studentCode);
            }
        }

        public PagedResult<Document> RetrieveDocuments(int organisationId, Expression<Func<Document, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {

                return context
                    .Documents
                    .Include(p => p.Organisation)
                    .Include(p => p.DocumentType)
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(orderBy ?? new List<OrderBy>
                    {
                        new OrderBy
                        {
                            Property = "CreatedDateTime",
                            Direction = System.ComponentModel.ListSortDirection.Descending
                        }
                    })
                    .Paginate(paging);
            }
        }

        public Document RetrieveDocument(int organisationId, Guid documentGuid)
        {
            using (ReadUncommitedTransactionScope)
            using (var context = _databaseFactory.Create())
            {
                return context
                    .Documents
                    .FirstOrDefault(e => e.Guid == documentGuid);

            }
        }

    }
}

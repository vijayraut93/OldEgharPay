using System;
using Microsoft.Practices.Unity;
using Egharpay.Business;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Services;
using Egharpay.Data;
using Egharpay.Data.Interfaces;
using Egharpay.Data.Models;
using Egharpay.Data.Services;
using Egharpay.Interfaces;
using Egharpay.Document.Interfaces;
using Egharpay.Document;
using System.Linq;
using System.Data.Entity;

namespace Egharpay
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<IEgharpayDatabaseFactory<EgharpayDatabase>, EgharpayDatabaseFactory>(new InjectionConstructor(
                    new InjectionParameter<string>(ConfigHelper.DefaultConnection)
                 ));

            //container.RegisterType<IDocumentServiceRestClient, DocumentServiceRestClient>(
            //    new InjectionConstructor(
            //        new InjectionParameter<Uri>(new Uri(ConfigurationManager.AppSettings["DocumentRESTApiAddress"])),
            //        new InjectionParameter<string>(ConfigurationManager.AppSettings["Username"]),
            //        new InjectionParameter<string>(ConfigurationManager.AppSettings["Password"])
            //    ));

            // let's enforce a singleton on CacheProvider, even though it accesses a static MemoryCache.Default
            container.RegisterType<ICacheProvider, MemoryCacheProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPersonnelDataService, PersonnelDataService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IApartmentDataService, ApartmentDataService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPersonnelTestDataService, PersonnelTestDataService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPersonnelBusinessService, PersonnelBusinessService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IApartmentBusinessService, ApartmentBusinessService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEgharpayBusinessService, EgharpayBusinessService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITenantOrganisationService, EgharpayBusinessService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IGenericDataService<DbContext>, EntityFrameworkGenericDataService>();
            // container.RegisterType<ITemplateService, TemplateService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailService, EmailService>(new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                    new InjectionParameter<string>(ConfigHelper.OverrideEmailAddresses)
                )
            );

            // Register everything in these namespaces based on convention:
            var conventionBasedMappings = new[]
            {
                "Egharpay.Data.Services",
                "Egharpay.Data.Interfaces",
                "Egharpay.Business.Services",
                "Egharpay.Business.Interfaces",
            };

            container.RegisterTypes(
               AllClasses.FromLoadedAssemblies().Where(tt => conventionBasedMappings.Any(n => n == tt.Namespace)),
               WithMappings.FromMatchingInterface,
               //getName: new Func<Type, string>(t => t.Name)
               WithName.Default
            );

            container.RegisterType<IDocumentService, DocumentService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITenantsService, TenantsService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPdfService, PdfService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRazorService, RazorService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITemplateService, TemplateService>(new ContainerControlledLifetimeManager());
        }
    }
}

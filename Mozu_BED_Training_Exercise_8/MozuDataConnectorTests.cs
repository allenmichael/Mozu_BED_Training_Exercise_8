using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mozu.Api;
using Autofac;
using Mozu.Api.ToolKit.Config;

namespace Mozu_BED_Training_Exercise_8
{
    [TestClass]
    public class MozuDataConnectorTests
    {
        private IApiContext _apiContext;
        private IContainer _container;

        [TestInitialize]
        public void Init()
        {
            _container = new Bootstrapper().Bootstrap().Container;
            var appSetting = _container.Resolve<IAppSetting>();
            var tenantId = int.Parse(appSetting.Settings["TenantId"].ToString());
            var siteId = int.Parse(appSetting.Settings["SiteId"].ToString());

            _apiContext = new ApiContext(tenantId, siteId);
        }

        [TestMethod]
        public void Exercise_8_Get_Tenant()
        {
            /* --Create a New Tenant Resource
             *     Resources are used to leverage the methods provided by the SDK to talk to the Mozu service
             *     via the Mozu REST API. Every resource takes an ApiContext object as a parameter.
             */
            var tenantResource = new Mozu.Api.Resources.Platform.TenantResource(_apiContext);

            /*
             * --Utilize the Tenant Resource to Get Your Tenant
             *     Your tenant represents your overall Mozu store
             *     —the following properties are accessible from a Tenant object:
             *     tenant.Domain -- string
             *     tenant.Id -- int
             *     tenant.MasterCatalogs -- List<MasterCatalog>
             *     tenant.Sites -- List<Site>
             *     tenant.Name -- string
             *     
             *     See this site for more info:
             *     http://developer.mozu.com/content/api/APIResources/platform/platform.tenants/platform.tenants.htm
             */
            var tenant = tenantResource.GetTenantAsync(_apiContext.TenantId).Result;

            //Add Your Code: 
            //Write Tenant name

            System.Diagnostics.Debug.WriteLine(string.Format("Tenant Name[{0}]: {1}", tenant.Id, tenant.Name));

            //Add Your Code: 
            //Write Tenant domain

            System.Diagnostics.Debug.WriteLine(string.Format("Tenant Domain[{0}]: {1}", tenant.Id, tenant.Domain));

            //Add Your Code: 
            foreach (var masterCatalog in tenant.MasterCatalogs)
            {
                //Write Master Catalog info
                System.Diagnostics.Debug.WriteLine(string.Format("Master Catalog[{0}]: {1}", masterCatalog.Id, masterCatalog.Name));

                foreach (var catalog in masterCatalog.Catalogs)
                {
                    //Write Catalog info
                    System.Diagnostics.Debug.WriteLine(string.Format("\tCatalog[{0}]: {1}", catalog.Id, catalog.Name));
                }

            }
            //Add Your Code: 
            foreach (var site in tenant.Sites)
            {
                //Get Site and write Site info
                System.Diagnostics.Debug.WriteLine(string.Format("Site[{0}]: {1}", site.Id, site.Name));
            }

        }
    }
}

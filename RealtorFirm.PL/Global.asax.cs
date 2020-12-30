using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RealtorFirm.BLL.Infrastructure;
using Ninject.Modules;
using Ninject.Web.Mvc;
using RealtorFirm.PL.Util;
using Ninject;

namespace RealtorFirm.PL
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule bindModule = new BindModule();
            NinjectModule serviceModule = new ServiceModule("AppartmentContext");
            var kernel = new StandardKernel(bindModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}

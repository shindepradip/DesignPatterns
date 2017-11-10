using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;
using WebApplication1.Controllers;
using System.Web.Hosting;
using System.Xml.Linq;
using System.Linq;

namespace WebApplication1
{
    public class MycontrollerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            

            IController cntrl = new HomeController();
            return cntrl;

            //typeData = Type.GetType(controllerName);
            //controllerType = (IController)Activator.CreateInstance(typeData);
            //return controllerType;

        }
    }

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            HomeController cntr = new HomeController();

            ControllerBuilder.Current.SetControllerFactory(typeof(MycontrollerFactory));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

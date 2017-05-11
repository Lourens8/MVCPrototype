using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<ApplicationDbContext>();
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<ApplicationSignInManager>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
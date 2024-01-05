using MyApp.Core;
using MyApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace WebApp1.App_Start
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Registrasi layanan di sini
            container.RegisterType<IRoleRepository, RoleRepository>();
        /*    container.RegisterType<RoleService>();*/

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
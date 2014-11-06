using DrWatson.CatExp.Data;
using DrWatson.CatExp.Data.Infrastructura;
using DrWatson.CatExp.Data.Interfaces;
using DrWatson.CatExp.Web.Controllers;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrWatson.CatExp.Web.Infrastructura
{
    public class ContainerBootstrapper
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IProveedorRepositorios, ProveedorRepositorios>();
            container.RegisterType<ICatExpUow, CatExpUow>();
            container.RegisterType<FactoriaRepositorios>(
                new InjectionConstructor());

            // Controllers
            System.Type typeICatExpUow = typeof(ICatExpUow);
            container.RegisterType<ExportadoresController>(
                new InjectionConstructor(typeICatExpUow));

        }
    }
}

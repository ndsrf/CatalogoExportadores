using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DrWatson.CatExp.Web;
using DrWatson.CatExp.Web.Controllers;
using Microsoft.Practices.Unity;
using DrWatson.CatExp.Web.Infrastructura;
using DrWatson.CatExp.Data.Interfaces;
using System.Collections.Generic;
using DrWatson.CatExp.Model;

namespace DrWatson.CatExp.WebApi.Tests.Controllers
{
    [TestClass]
    public class ExportadoresControllerTest
    {
        ICatExpUow catExpUow;
        ExportadoresController controller;

        [TestInitialize]
        public void Initialise()
        {
            var unityContainer = new UnityContainer();
            ContainerBootstrapper.RegisterTypes(unityContainer);
            catExpUow = unityContainer.Resolve<ICatExpUow>();
            controller = new ExportadoresController(catExpUow);
        }

        [TestMethod]
        public void ExportadorControllerGetAll()
        {

            IEnumerable<Exportador> listaExportadores = controller.Get();

            // Assert
            Assert.IsNotNull(listaExportadores);
            Assert.AreEqual(3, System.Linq.Enumerable.Count(listaExportadores));

            //var x = listaExportadores.GetEnumerator();
            //x.Reset();
            //x.MoveNext();
            //string st = x.Current.Nombre;
            //Assert.AreEqual("DrWatson", st);
        }

        [TestMethod]
        public void ExportadorControllerGetOne()
        {
            Exportador exp = controller.Get(1);

            // Assert
            Assert.IsNotNull(exp);
            Assert.AreEqual("DrWatson", exp.Nombre);
        }

        [TestMethod]
        public void ExportadorControllerPut()
        {
            Exportador exp = new Exportador { ExportadorId = 1, Nombre = "Test Put 1", Telefono = "999999" };
            controller.Put(1, exp);

            Exportador expGet = controller.Get(1);

            Assert.AreEqual(exp, expGet);
        }

        [TestMethod]
        public void ExportadorControllerDelete()
        {
            controller.Delete(3);

            Exportador exp2 = controller.Get(3);

            Assert.IsNull(exp2);
        }

        [TestMethod]
        public void ExportadorControllerPost()
        {
            Exportador exp = new Exportador { ExportadorId = -1, Nombre = "Anadido desde Post", Telefono = "999999" };
            controller.Post(exp);

            Exportador exp2 = controller.Get(exp.ExportadorId);

            Assert.AreEqual(exp, exp2);
        }
    }
}

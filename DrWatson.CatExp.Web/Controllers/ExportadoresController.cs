using Breeze.WebApi2;
using DrWatson.CatExp.Data.Interfaces;
using DrWatson.CatExp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DrWatson.CatExp.Web.Controllers
{
    [BreezeController]
    public class ExportadoresController : ApiControllerBase
    {

        public ExportadoresController(ICatExpUow uow)
        {
            Uow = uow;
        }

        [HttpGet]
        public string Metadata()
        {
            return Uow.Metadata;
        }

        [HttpGet]
        // GET api/exportadores
        public IQueryable<Exportador> Exportadores()
        {
            return Uow.Exportadores.GetAll();
            //return new string[] { "value1", "value2" };
        }

        [HttpGet]
        // GET api/exportadores
        public Exportador ExportadoresById(int id)
        {
            return Uow.Exportadores.GetById(id);
            //return new string[] { "value1", "value2" };
        }

        [HttpGet]
        // GET api/exportadores
        public object Lookups()
        {
            var businesscodes = Uow.BusinessCodes.GetAll().ToList();
            var provincias = Uow.Provincias.GetAll().ToList();

            var lkup = new { businesscodes, provincias };
            return lkup;
            //return new string[] { "value1", "value2" };
        }

        //// GET api/exportadores/5
        //public Exportador Get(int id)
        //{
        //    return Uow.Exportadores.GetById(id);
        //}

        // POST api/exportadores
        public HttpResponseMessage Post(Exportador exportador)
        {
            HttpResponseMessage response = null;

            Uow.Exportadores.Add(exportador);
            Uow.Commit();
            if (Request != null)
            {
                response = Request.CreateResponse<Exportador>(HttpStatusCode.Created, exportador);
                string uri = Url.Route(null, new { id = exportador.ExportadorId });
                response.Headers.Location = new Uri(Request.RequestUri, uri);
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.Created);
            }
            return response;
        }

        // PUT api/exportadores/5
        public void Put(int id, Exportador exportador)
        {
            Uow.Exportadores.Update(exportador);
            Uow.Commit();
            //throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        // DELETE api/exportadores/5
        public void Delete(int id)
        {
            Uow.Exportadores.Delete(id);
            Uow.Commit();
        }
    }
}

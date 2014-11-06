using DrWatson.CatExp.Data;
using DrWatson.CatExp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrWatson.CatExp.WebApi.Tests.DBInitialiser
{
    class DBInitialiserForUnitTesting : System.Data.Entity.DropCreateDatabaseAlways<CatExpContext>
    {
        protected override void Seed(CatExpContext context)
        {
            var exportadores = new List<Exportador>
            {
                new Exportador { Nombre = "DrWatson", Telefono = "952789019" },
                new Exportador { Nombre = "Sequel", Telefono = "81818181" },
                new Exportador { Nombre = "Secano", Telefono = "71717171" }
            };

            exportadores.ForEach(exp => context.Exportadores.Add(exp));
            context.SaveChanges();
        }
    }
}

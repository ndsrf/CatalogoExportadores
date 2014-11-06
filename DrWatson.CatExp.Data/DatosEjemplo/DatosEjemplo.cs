using DrWatson.CatExp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrWatson.CatExp.Data
{
    public class DatosEjemplo  : System.Data.Entity.DropCreateDatabaseIfModelChanges<CatExpContext>
    {
        protected override void Seed(CatExpContext context)
        {
            BusinessCode actividadPadre = new BusinessCode { Nombre = "Actividad" };
            BusinessCode activiProgramacion = new BusinessCode { Nombre = "Programación", Padre = actividadPadre };
            BusinessCode activiDiseno = new BusinessCode { Nombre = "Diseño Gráfico", Padre = actividadPadre };
            context.BusinessCodes.Add(actividadPadre);
            context.BusinessCodes.Add(activiProgramacion);
            context.BusinessCodes.Add(activiDiseno);
            context.SaveChanges();

            Provincia almeria = new Provincia { Nombre = "Almería" };
            Provincia sevilla = new Provincia { Nombre = "Sevilla" };
            Provincia cadiz = new Provincia { Nombre = "Cádiz" };
            Provincia malaga = new Provincia { Nombre = "Málaga" };
            Provincia cordoba = new Provincia { Nombre = "Córdoba" };
            Provincia jaen = new Provincia { Nombre = "Jaén" };
            Provincia granada = new Provincia { Nombre = "Granada" };
            Provincia huelva = new Provincia { Nombre = "Huelva" };
            var provincias = new List<Provincia> {
                almeria,
                sevilla,
                cadiz,
                malaga,
                cordoba,
                jaen,
                granada,
                huelva
            };
            provincias.ForEach(pro => context.Provincias.Add(pro));
            context.SaveChanges();

            var exportadores = new List<Exportador>
            {
                new Exportador { Nombre = "DrWatson", Telefono = "952789019", Provincia =  malaga, Actividad = activiDiseno },
                new Exportador { Nombre = "Sequel", Telefono = "81818181", Provincia = malaga, Actividad = activiProgramacion},
                new Exportador { Nombre = "Secano", Telefono = "71717171", Provincia = sevilla, Actividad = activiDiseno }
            };
            exportadores.ForEach(exp => context.Exportadores.Add(exp));
            context.SaveChanges();
        }
    }
}

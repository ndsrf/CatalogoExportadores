using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrWatson.CatExp.Data
{
    public class ExportadoresRepositorio : IExportadoresRepositorio
    {
        private CatExpContext context;

        public ExportadoresRepositorio(CatExpContext context)
        {
            this.context = context;
        }

        public IEnumerable<Model.Exportador> GetExportadores()
        {
            return context.Exportadores.ToList();
        }

        public Model.Exportador GetExportadorById(int exportadorId)
        {
            return context.Exportadores.Find(exportadorId);
        }

        public void InsertExportador(Model.Exportador exp)
        {
            context.Exportadores.Add(exp);
        }

        public void UpdateExportador(Model.Exportador exp)
        {
            context.Entry(exp).State = System.Data.Entity.EntityState.Modified;
        }

        public void DeleteExportador(int exportadorId)
        {
            Model.Exportador exp = context.Exportadores.Find(exportadorId);
            if (exp != null)
                context.Exportadores.Remove(exp);
            else
                throw new Exception("Exportador no encontrado cuando se trataba de eliminarlo de la BBDD");
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}

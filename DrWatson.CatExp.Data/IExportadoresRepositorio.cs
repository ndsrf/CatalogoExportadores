using DrWatson.CatExp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrWatson.CatExp.Data
{
    public interface IExportadoresRepositorio
    {
        IEnumerable<Exportador> GetExportadores();
        Exportador GetExportadorById(int exportadorId);
        void InsertExportador(Exportador exp);
        void UpdateExportador(Exportador exp);
        void DeleteExportador(int exportadorId);
        void Save();
    }
}

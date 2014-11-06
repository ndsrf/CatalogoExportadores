using DrWatson.CatExp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrWatson.CatExp.Data.Interfaces
{
    public interface ICatExpUow
    {
        void Commit();

        // Repositorios
        IRepositorio<Exportador> Exportadores { get; }
        IRepositorio<BusinessCode> BusinessCodes { get; }
        IRepositorio<Provincia> Provincias { get; }
        string Metadata { get; }
    }
}

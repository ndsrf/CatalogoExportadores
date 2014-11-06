using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrWatson.CatExp.Model
{
    public class Exportador
    {
        [Key]
        public int ExportadorId { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public Provincia Provincia { get; set; }

        public BusinessCode Actividad { get; set; }

    }
}

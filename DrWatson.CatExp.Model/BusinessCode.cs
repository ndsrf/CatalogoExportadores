using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrWatson.CatExp.Model
{
    public class BusinessCode
    {
        [Key]
        public int BusinessCodeId { get; set; }
        public BusinessCode Padre { get; set; }
        public string Nombre { get; set; }
    }
}

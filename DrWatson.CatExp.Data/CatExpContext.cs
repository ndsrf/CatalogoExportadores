using DrWatson.CatExp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrWatson.CatExp.Data
{
    public class CatExpContext : DbContext
    {
        public CatExpContext() : base(nameOrConnectionString: "CatExpDatabase")
        {
        }

        public CatExpContext(String dbName)
            : base(nameOrConnectionString: dbName)
        {
        }

        public DbSet<Exportador> Exportadores { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<BusinessCode> BusinessCodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

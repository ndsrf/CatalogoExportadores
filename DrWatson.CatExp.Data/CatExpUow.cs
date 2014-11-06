using DrWatson.CatExp.Data.Interfaces;
using DrWatson.CatExp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrWatson.CatExp.Data
{
    public class CatExpUow : ICatExpUow, IDisposable
    {
        private CatExpContext DbContext { get; set; }
        protected IProveedorRepositorios ProveedorDeRepositorios { get; set; }


        public CatExpUow(IProveedorRepositorios proveedorDeRepositorios)
        {
            CreateDbContext();

            proveedorDeRepositorios.DbContext = DbContext;
            ProveedorDeRepositorios = proveedorDeRepositorios;       
        }

        public string Metadata { 
            get {
                return new Breeze.ContextProvider.EF6.EFContextProvider<CatExpContext>().Metadata(); ;
            } 
        }

        public void Commit()
        {
            //System.Diagnostics.Debug.WriteLine("Committed");
            DbContext.SaveChanges();
        }

        protected void CreateDbContext()
        {
            DbContext = new CatExpContext();

            // Do NOT enable proxied entities, else serialization fails
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            DbContext.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }

        public IRepositorio<Exportador> Exportadores
        {
            get { return GetStandardRepo<Exportador>(); }
        }

        public IRepositorio<BusinessCode> BusinessCodes
        {
            get { return GetStandardRepo<BusinessCode>(); }
        }

        public IRepositorio<Provincia> Provincias
        {
            get { return GetStandardRepo<Provincia>(); }
        }

        private IRepositorio<T> GetStandardRepo<T>() where T : class
        {
            return ProveedorDeRepositorios.GetRepositoryForEntityType<T>();
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }
        #endregion
    }
}

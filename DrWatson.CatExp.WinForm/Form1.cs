using DrWatson.CatExp.Data;
using DrWatson.CatExp.Data.Infrastructura;
using DrWatson.CatExp.Data.Interfaces;
using DrWatson.CatExp.Model;
using DrWatson.CatExp.Web.Infrastructura;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrWatson.CatExp.WinForm
{
    public partial class Form1 : Form
    {
        ICatExpUow catExpUow;
        FactoriaRepositorios f;

        public Form1()
        {
            InitializeComponent();

            f = new FactoriaRepositorios();

            RetrieveDataUnity();
        }

        private void RetrieveData()
        {
            IProveedorRepositorios proveedorRepositorios;
            FactoriaRepositorios factoriaRepositorios = new FactoriaRepositorios();  

            proveedorRepositorios = new ProveedorRepositorios(factoriaRepositorios);

            catExpUow = new CatExpUow(proveedorRepositorios);
        }

        private void RetrieveDataUnity()
        {
            var unityContainer = new UnityContainer();
            ContainerBootstrapper.RegisterTypes(unityContainer);
            catExpUow = unityContainer.Resolve<ICatExpUow>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var list = catExpUow.Exportadores.GetAll().ToList();
            dataGridView1.DataSource = list;
        }
    }
}

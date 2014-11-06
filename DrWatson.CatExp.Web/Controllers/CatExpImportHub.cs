using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace DrWatson.CatExp.WebApi.Controllers
{
    public class CatExpImportHub : Hub
    {
        public void ProcessImport(int id)
        {
            Clients.Caller.displayMessage("sbs_Run1", "Completed");
            System.Threading.Thread.Sleep(1000);
            Clients.Caller.displayMessage("sbs_Run2", "Completed");
            System.Threading.Thread.Sleep(3000);
            Clients.Caller.displayMessage("sbs_Run3", "Completed");
            System.Threading.Thread.Sleep(1000);
            Clients.Caller.displayMessage("sbs_Run4", "Completed");
            System.Threading.Thread.Sleep(2000);
            Clients.Caller.displayMessage("sbs_Reconciliation", "Completed");
            System.Threading.Thread.Sleep(1000);
            Clients.Caller.displayMessage("sbs_Finish", "Completed");
        }
    }
}
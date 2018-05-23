using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebVer1.BLL
{
    //Call form js : $connection.pkptHub
    public class PkptHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<PkptHub>();
        // Call this from JS: hub.client.send(duLieu)
        public void Send(string duLieu)
        {
            Clients.All.jsGetBill(duLieu);
        }
        // Call this from C#: PkptHub.Static_Send(duLieu)
        public static void Static_Send(Bill duLieu)
        {
            
            hubContext.Clients.All.jsGetBill(duLieu);
        }
    }
}
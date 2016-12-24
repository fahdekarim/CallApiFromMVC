using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebAppInfoPersoMVC.Hubs
{
    [HubName("NotificationHub")]
    public class NotificationHub:Hub
    {
        static long counter = 0; //count online users
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }
        public override Task OnConnected()
        {
            counter++;//add one when user connected
            Clients.All.UpdateCount(counter);//call client method to update interface
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            counter--;//substract one when user disconnected
            Clients.All.UpdateCount(counter);//call client inteface to update interface
            return base.OnDisconnected(stopCalled);
        }
    
    }
}
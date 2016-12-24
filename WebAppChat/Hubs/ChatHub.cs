using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WebAppChat.Models;

namespace WebAppChat
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        static long counter;
        public List<IOTSigfox> dbhub = new List<IOTSigfox>();
        public void Hello()
        {
            Clients.All.hello();
        }
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }
        public override Task OnConnected()
        {
           
            counter++;
            Clients.All.gocounter(counter);
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            counter--;
            Clients.All.gocounter(counter);
            return base.OnDisconnected(stopCalled);
        }
    }
}
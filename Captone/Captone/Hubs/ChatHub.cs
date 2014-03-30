using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Captone.Hubs
{
    public class ChatHub : Hub
    {
        public class Notification
        {
            public int ID { get; set; }
            public string Username { get; set; }
            public int StationID { get; set; }
            public Boolean isView { get; set; }
        }
        public Task AddGroups()
        {
            //add 1st group
            Groups.Add(Context.ConnectionId, "foo");
            //add 2nd group
            return Groups.Add(Context.ConnectionId, "foobar");
        }
        public void Send(string message)
        {
           
            Clients.OthersInGroup("foobar").send(message);
           
        }
        public override Task OnConnected()
        {
            AddGroups();
            return base.OnConnected();
        }

        //rejoin groups if client disconnects and then reconnects
        public override Task OnReconnected()
        {
            AddGroups();
            return base.OnReconnected();
        }
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.displayStatus();
        }
    }
}
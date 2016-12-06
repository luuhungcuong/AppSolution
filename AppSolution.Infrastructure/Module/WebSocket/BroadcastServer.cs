using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using SuperSocket.WebSocket;
using SuperSocket.WebSocket.SubProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AppSolution.Infrastructure.Module.WebSocket
{
    public class BroadcastServerManager
    {
        private IBootstrap Bootstrap { get; set; }
        private BroadcastServer BroadcastServer { get; set; }

        public void StartSuperWebSocketByConfig()
        {
            Bootstrap = BootstrapFactory.CreateBootstrap();

            if (!Bootstrap.Initialize())
                return;
            var socketServer = Bootstrap.AppServers.FirstOrDefault(s => s.Name.Equals("BroadcastServer")) as BroadcastServer;
            socketServer.NewMessageReceived += new SessionHandler<BroadcastSession, string>(socketServer_NewMessageReceived);
            socketServer.NewSessionConnected += socketServer_NewSessionConnected;
            socketServer.SessionClosed += socketServer_SessionClosed;

            BroadcastServer = socketServer;

            Bootstrap.Start();
        }

        void socketServer_NewMessageReceived(BroadcastSession session, string e)
        {
            if (e.Contains("UserID"))
                SendToAll(e);
            else
                session.Send(Utils.JsonHelper.ToString(new { UserID = "System", Message = "You must login first!" }));
        }

        void socketServer_NewSessionConnected(BroadcastSession session)
        {
            //SendToAll("System: " + session.Cookies["name"] + " connected");            
        }

        void socketServer_SessionClosed(BroadcastSession session, CloseReason reason)
        {
            if (reason == CloseReason.ServerShutdown)
                return;
            // SendToAll("System: " + session.Cookies["name"] + " disconnected");
        }

        void SendToAll(string message)
        {
            foreach (var s in BroadcastServer.GetAllSessions())
            {
                s.Send(message);
            }
        }

        public void StopServer()
        {
            if (Bootstrap != null)
                Bootstrap.Stop();
        }
    }
    public class BroadcastServer : WebSocketServer<BroadcastSession>
    {

    }

    public class BroadcastSession : WebSocketSession<BroadcastSession>
    {
        public string UserId { get; set; }
    }
    public class LOGIN : SubCommandBase
    {
        public override void ExecuteCommand(WebSocketSession session, SubRequestInfo requestInfo)
        {
            session.Send(requestInfo.Body);
        }
    }
}

using Common;
using MyGameServer.MyClient;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    public abstract class BaseHandler
    {
        public OperationCode code;
        public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer);

    }
}

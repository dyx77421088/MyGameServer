using MyGameServer.MyClient;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameServer.Handler
{
    public class DefaultHandler : BaseHandler
    {
        public DefaultHandler()
        {
            code = Common.OperationCode.Defalut;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            throw new NotImplementedException();
        }
    }
}

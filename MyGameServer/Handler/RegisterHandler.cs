using Common;
using Common.Util;
using MyGameServer.Manage;
using MyGameServer.MyClient;
using MyGameServer.Util;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MyGameServer.Handler
{
    public class RegisterHandler : BaseHandler
    {
        public RegisterHandler()
        {
            code = OperationCode.Register;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            Dictionary<byte, object> dict = operationRequest.Parameters;
            //object name, password;
            //dict.TryGetValue((byte)ParameterCode.Name, out name);
            //dict.TryGetValue((byte)ParameterCode.Password, out password);
            string name = DictUtils.GetStringValue<byte, object>(dict, (byte)ParameterCode.Name);
            string password = DictUtils.GetStringValue<byte, object>(dict, (byte)ParameterCode.Password);

            // 注册
            int flag = new UserManage().Add(new Model.User() { Name=name,Password=password});

            MyServerUtils.SendStatusMessageToClient(peer, sendParameters, operationRequest.OperationCode,
                flag != -1 ? 200 : 400,
                flag != -1 ? "注册成功！！" : "用户名重复");

        }
    }
}

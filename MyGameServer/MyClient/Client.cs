﻿using Common;
using Common.Model;
using Common.Util;
using MyGameServer.Handler;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using PhotonHostRuntimeInterfaces;
using System.Collections.Generic;

namespace MyGameServer.MyClient
{
    public class Client : ClientPeer
    {
        private Player player;
        private bool isLogin;
        private ClientTread clientTread;

        public bool IsLogin { get => isLogin; set => isLogin = value; }
        public Player Player { get => player; set => player = value; }
        public ClientTread ClientTread { get => clientTread; set => clientTread = value; }

        public Client(InitRequest initRequest) : base(initRequest)
        {
            IsLogin = false;
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            // 通知其它用户，我离开了！！
            foreach (Client temp in MyGameServer.loginClients)
            {
                if (temp.player.Id !=  player.Id)
                {
                    EventData eventData = new EventData((byte)EventCode.ClosePlayer);
                    Dictionary<byte, object> data = new Dictionary<byte, object>();
                    data.Add((byte)ParameterCode.JsonData, ToGson.Success(Player));
                    eventData.Parameters = data;
                    temp.SendEvent(eventData, new SendParameters());
                }
            }

            // 移除这个对象
            clientTread?.Close(); // 不为空就关闭它
            MyGameServer.loginClients.Remove(this);
            MyGameServer.clients.Remove(this);
            MyGameServer.logger.Info("一个客户端断开连接了");

        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            BaseHandler handler =  DictUtils.GetValue<OperationCode, BaseHandler>(
                MyGameServer.Instance.handlerDict, (OperationCode)operationRequest.OperationCode);

            handler.OnOperationRequest(operationRequest, sendParameters, this);
        }

        /// <summary>
        /// 设置为登录状态
        /// </summary>
        public void SetLogin()
        {
            IsLogin = true;
        }
    }
}

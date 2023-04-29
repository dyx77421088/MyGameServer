﻿using Common;
using Common.Model;
using Common.Util;
using MyGameServer.Manage;
using MyGameServer.Model;
using MyGameServer.MyClient;
using MyGameServer.Util;
using Photon.SocketServer;
using System;
using System.Collections.Generic;

namespace MyGameServer.Handler
{
    public class LoginHandler : BaseHandler
    {
        public LoginHandler() { code = OperationCode.Login; }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, Client peer)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>();
            parameters = operationRequest.Parameters;
            //object username, password;
            //parameters.TryGetValue((byte)ParameterCode.Name, out username);
            //parameters.TryGetValue((byte)ParameterCode.Password, out password);
            string username = DictUtils.GetStringValue<byte, object>(parameters, (byte)ParameterCode.Name);
            string password = DictUtils.GetStringValue<byte, object>(parameters, (byte)ParameterCode.Password);

            MyGameServer.logger.Info("用户("+username+")尝试登录....");
            User user = new UserManage().Login(username, password);
            bool isLogin = user != null;

            

            // 其它用户的角色的位置信息,,还有自己的
            MyList<Player> otherPlayerList = new MyList<Player>();
            if (isLogin)
            {
                // 保存登录的client
                MyGameServer.loginClients.Add(peer);
                peer.Player = new Player(user.Id, user.Name, new Vector3DPosition());
                peer.ClientTread = new ClientTread(peer);
                // 先不开线程
                //peer.ClientTread.Start();

                // 通知其它已在线的用户，
                foreach (Client temp in MyGameServer.loginClients)
                {
                    if (temp.Player.Id != peer.Player.Id)
                    {
                        EventData eventData = new EventData((byte)EventCode.NewPlayer);
                        Dictionary<byte, object> data = new Dictionary<byte, object>();
                        data.Add((byte)ParameterCode.JsonData, ToGson.Success(peer.Player));
                        eventData.Parameters = data;
                        temp.SendEvent(eventData, new SendParameters());

                    }
                    // 添加进来
                    otherPlayerList.Add(temp.Player);
                }

                // 
            }

            MyServerUtils.SendDataToClient(peer, sendParameters, operationRequest.OperationCode,
                ToGson.Info(isLogin ? 200 : 400, isLogin ? "登录成功" : "用户名或密码错误", otherPlayerList));
            //MyGameServer.logger.Info("测试啊啊啊 :" + (ToGson.Info(isLogin ? 200 : 400, otherPlayerList)));
            
            MyGameServer.logger.Info("用户(" + username + ")登录=>" + (isLogin ? "登录成功!" : "用户名或密码错误"));
            MyGameServer.logger.Info("当前在线人数为:" + (otherPlayerList.Count));
        }
    }
}

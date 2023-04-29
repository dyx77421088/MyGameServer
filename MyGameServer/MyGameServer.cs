using Common;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using Google.Protobuf.WellKnownTypes;
using log4net.Config;
using MyGameServer.Handler;
using MyGameServer.MyClient;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using Log4NetLoggerFactory = ExitGames.Logging.Log4Net.Log4NetLoggerFactory;

namespace MyGameServer
{
    class MyGameServer : ApplicationBase
    {
        public static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public static List<Client> clients = new List<Client>();
        public static List<Client> loginClients = new List<Client>();
        public static MyGameServer Instance
        {
            get;
            private set;
        }
        // 存放操作的字典
        public Dictionary<OperationCode, BaseHandler> handlerDict = new Dictionary<OperationCode, BaseHandler> ();
        private void initDict()
        {
            handlerDict.Clear();

            // 默认操作
            DefaultHandler defaultHandler = new DefaultHandler();
            handlerDict.Add(defaultHandler.code, defaultHandler);
 
            // 登录操作
            LoginHandler loginHandler = new LoginHandler();
            handlerDict.Add(loginHandler.code, loginHandler);

            // 注册操作
            RegisterHandler registerHandler = new RegisterHandler();
            handlerDict.Add(registerHandler.code, registerHandler);

            // 更新位置操作
            PlayerNewPositionHandler playerNewPositionHandler = new PlayerNewPositionHandler();
            handlerDict.Add(playerNewPositionHandler.code, playerNewPositionHandler);
        }
        #region 重写的父类，处理一些初始化
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            logger.Info("一个客户端连接过来了");
            Client client = new Client(initRequest);
            // 把客户端添加到clients中
            clients.Add(client);
            return client;
        }

        protected override void Setup()
        {
            // 日志的初始化
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            log4net.GlobalContext.Properties["LogFileName"] = "woShiRiZhiName";
            FileInfo file = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (file.Exists )
            {
                // 使用哪个日志插件
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                // log4net读取配置文件
                XmlConfigurator.ConfigureAndWatch(file);
            }

            logger.Info("初始化完成");


            // 单例
            Instance = this;
            initDict();
        }

        protected override void TearDown()
        {
            logger.Info("断开连接了");
        }
        #endregion


    }
}

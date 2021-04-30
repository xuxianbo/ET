﻿using System;
using System.IO;
using System.Net;
 using ET;

 namespace ETHotfix
{
    [ObjectSystem]
    public class NetKcpComponentAwakeSystem: AwakeSystem<NetKcpComponent>
    {
        public override void Awake(NetKcpComponent self)
        {
            self.MessageDispatcher = new OuterMessageDispatcher();
            
            self.Service = new TService(NetThreadComponent.Instance.ThreadSynchronizationContext, ServiceType.Outer);
            self.Service.ErrorCallback += self.OnError;
            self.Service.ReadCallback += self.OnRead;

            NetThreadComponent.Instance.Add(self.Service);
        }
    }

    [ObjectSystem]
    public class NetKcpComponentAwake1System: AwakeSystem<NetKcpComponent, IPEndPoint>
    {
        public override void Awake(NetKcpComponent self, IPEndPoint address)
        {
            self.MessageDispatcher = new OuterMessageDispatcher();
            
            self.Service = new TService(NetThreadComponent.Instance.ThreadSynchronizationContext, address, ServiceType.Outer);
            self.Service.ErrorCallback += self.OnError;
            self.Service.ReadCallback += self.OnRead;
            self.Service.AcceptCallback += self.OnAccept;

            NetThreadComponent.Instance.Add(self.Service);
        }
    }

    [ObjectSystem]
    public class NetKcpComponentLoadSystem: LoadSystem<NetKcpComponent>
    {
        public override void Load(NetKcpComponent self)
        {
            self.MessageDispatcher = new OuterMessageDispatcher();
        }
    }

    [ObjectSystem]
    public class NetKcpComponentDestroySystem: DestroySystem<NetKcpComponent>
    {
        public override void Destroy(NetKcpComponent self)
        {
            NetThreadComponent.Instance.Remove(self.Service);
            self.Service.Destroy();
        }
    }

    public static class NetKcpComponentSystem
    {
        public static Session Get(this NetKcpComponent self, long id)
        {
            Session session = self.GetChild<Session>(id);
            return session;
        }

        public static Session Create(this NetKcpComponent self, IPEndPoint realIPEndPoint)
        {
            long channelId = RandomHelper.RandInt64();
            Session session = EntityFactory.CreateWithParentAndId<Session, AService>(self, channelId, self.Service);
            session.RemoteAddress = realIPEndPoint;
            session.AddComponent<SessionIdleCheckerComponent, int>(NetThreadComponent.checkInteral);
            
            self.Service.GetOrCreate(session.Id, realIPEndPoint);

            return session;
        }
    }
}
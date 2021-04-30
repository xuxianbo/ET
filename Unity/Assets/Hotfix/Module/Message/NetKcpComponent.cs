using System.IO;
using System.Net;
using ET;

namespace ETHotfix
{
    public class NetKcpComponent: Entity
    {
        public AService Service;

        public IMessageDispatcher MessageDispatcher { get; set; }

        public void OnError(long channelId, int error)
        {
            Session session = this.GetChild<Session>(channelId);
            if (session == null)
            {
                return;
            }

            session.Error = error;
            session.Dispose();
        }
        
        public void OnRead(long channelId, MemoryStream memoryStream)
        {
            Session session = this.GetChild<Session>(channelId);
            if (session == null)
            {
                return;
            }

            session.LastRecvTime = TimeHelper.ClientNow();
            MessageDispatcher.Dispatch(session, memoryStream);
        }

        
        // 这个channelId是由CreateAcceptChannelId生成的
        public void OnAccept(long channelId, IPEndPoint ipEndPoint)
        {
            Session session = EntityFactory.CreateWithParentAndId<Session, AService>(this, channelId, this.Service);
            session.RemoteAddress = ipEndPoint;

            session.AddComponent<SessionAcceptTimeoutComponent>();
            // 客户端连接，2秒检查一次recv消息，10秒没有消息则断开
            session.AddComponent<SessionIdleCheckerComponent, int>(NetThreadComponent.checkInteral);
        }

    }
}
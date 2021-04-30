using ET;

namespace ETHotfix
{
    public class SessionIdleCheckerComponent: Entity
    {
        public long RepeatedTimer;
        
        public void Check()
        {
            Session session = this.GetParent<Session>();
            long timeNow = TimeHelper.ClientNow();
            
            if (timeNow - session.LastRecvTime < 30 * 1000 && timeNow - session.LastSendTime < 30 * 1000)
            {
                return;
            }
            
            Log.Info($"session timeout: {session.Id} {timeNow} {session.LastRecvTime} {session.LastSendTime} {timeNow - session.LastRecvTime} {timeNow - session.LastSendTime}");
            session.Error = ErrorCode.ERR_SessionSendOrRecvTimeout;

            session.Dispose();
        }
    }
}
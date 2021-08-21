//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年8月21日 15:52:53
//------------------------------------------------------------

using libx;

namespace ET
{
    public class XAssetUpdater
    {
        private Updater m_Updater;

        public XAssetUpdater(Updater updater)
        {
            m_Updater = updater;
            m_Updater.Init();
        }
        
        public ETTask StartUpdate()
        {
            ETTask etTask = ETTask.Create(true);
            m_Updater.ResPreparedCompleted = () =>
            {
                etTask.SetResult();
            };
            m_Updater.StartUpdate();
            return etTask;
        }
    }
}
//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年8月29日 10:32:54
//------------------------------------------------------------

namespace ET
{
    public static class FUI_LobbyUtilities
    {
        public static void EnterMap(FUI_LobbyComponent self)
        {
            EnterMapHelper.EnterMapAsync(self.ZoneScene()).Coroutine();
        }
    }
}
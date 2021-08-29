//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年8月29日 9:39:13
//------------------------------------------------------------

namespace ET
{
    public class FUI_LobbyComponentAwakeSystem : AwakeSystem<FUI_LobbyComponent, FUI_Lobby>
    {
        public override void Awake(FUI_LobbyComponent self, FUI_Lobby fuiLobby)
        {
            fuiLobby.m_normalPVPBtn.self.onClick.Add(() => { FUI_LobbyUtilities.EnterMap(self); });
            self.FUILobby = fuiLobby;
        }
    }

    public class FUI_LobbyComponentUpdateSystem : UpdateSystem<FUI_LobbyComponent>
    {
        public override void Update(FUI_LobbyComponent self)
        {

        }
    }

    public class FUI_LobbyComponentDestroySystem : DestroySystem<FUI_LobbyComponent>
    {
        public override void Destroy(FUI_LobbyComponent self)
        {
            self.FUILobby = null;
        }
    }
}
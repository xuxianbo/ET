//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年8月29日 9:41:17
//------------------------------------------------------------

namespace ET
{
    public class FUILoginComponentAwakeSystem : AwakeSystem<FUI_LoginComponent, FUI_Login>
    {
        public override void Awake(FUI_LoginComponent self, FUI_Login fuiLogin)
        {
            fuiLogin.m_Btn_Login.self.onClick.Add(() => { FUI_LoginUtilities.OnLogin(self); });
            self.FUILogin = fuiLogin;
        }
    }

    public class FUILoginComponentUpdateSystem : UpdateSystem<FUI_LoginComponent>
    {
        public override void Update(FUI_LoginComponent self)
        {
        }
    }

    public class FUILoginComponentDestroySystem : DestroySystem<FUI_LoginComponent>
    {
        public override void Destroy(FUI_LoginComponent self)
        {
            self.FUILogin = null;
        }
    }
}
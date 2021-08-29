namespace ET
{
    public class AppStartInitFinish_CreateLoginUI : AEvent<EventType.AppStartInitFinish>
    {
        protected override async ETTask Run(EventType.AppStartInitFinish args)
        {
            Scene scene = args.ZoneScene;

            await scene.GetComponent<FUIPackageManagerComponent>().AddPackageAsync(FUIPackage.Login);
            FUI_Login fuiLogin = await FUI_Login.CreateInstanceAsync(args.ZoneScene);

            FUIManagerComponent fuiManagerComponent = scene.GetComponent<FUIManagerComponent>();
            EntityFactory.CreateWithParent<FUI_LoginComponent, FUI_Login>(fuiManagerComponent, fuiLogin, true);

            scene.GetComponent<FUIManagerComponent>().Add(FUIPackage.Login, fuiLogin);
        }
    }
}
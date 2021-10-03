namespace ET
{
    public class LoginFinish_CreateLobbyUI : AEvent<EventType.LoginFinish>
    {
        protected override async ETTask Run(EventType.LoginFinish args)
        {
            Scene scene = args.ZoneScene;

            await scene.GetComponent<FUIPackageManagerComponent>().AddPackageAsync(FUIPackage.Lobby);
            FUI_Lobby fuiLobby = await FUI_Lobby.CreateInstanceAsync(args.ZoneScene);

            FUIManagerComponent fuiManagerComponent = scene.GetComponent<FUIManagerComponent>();
            Entity.Create<FUI_LobbyComponent, FUI_Lobby>(fuiManagerComponent, fuiLobby, true);

            scene.GetComponent<FUIManagerComponent>().Add(FUIPackage.Lobby, fuiLobby);
        }
    }
}
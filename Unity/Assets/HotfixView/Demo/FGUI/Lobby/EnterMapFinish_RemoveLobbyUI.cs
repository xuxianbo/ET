namespace ET
{
	public class EnterMapFinish_RemoveLobbyUI: AEvent<EventType.EnterMapFinish>
	{
		protected override async ETTask Run(EventType.EnterMapFinish args)
		{
			await XAssetLoader.LoadSceneAsync(XAssetPathUtilities.GetScenePath("Map"));
			
			Scene scene = args.ZoneScene;
			
			scene.GetComponent<FUIManagerComponent>().Remove(FUIPackage.Lobby);
			scene.AddComponent<OperaComponent>();
		}
	}
}

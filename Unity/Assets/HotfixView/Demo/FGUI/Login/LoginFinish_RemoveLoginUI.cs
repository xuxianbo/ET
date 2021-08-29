

namespace ET
{
	public class LoginFinish_RemoveLoginUI: AEvent<EventType.LoginFinish>
	{
		protected override async ETTask Run(EventType.LoginFinish args)
		{
			args.ZoneScene.GetComponent<FUIManagerComponent>().Remove(FUIPackage.Login);
			await ETTask.CompletedTask;
		}
	}
}

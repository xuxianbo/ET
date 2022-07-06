

using ET.Client;
using ET.EventType;

namespace ET
{
	[Event(SceneType.Client)]
	public class LoginFinish_RemoveLoginUI: AEvent<Scene, EventType.LoginFinish>
	{
		protected override async ETTask Run(Scene entity, LoginFinish a)
		{
			EnterMapHelper.EnterMapAsync(entity).Coroutine();
			entity.GetComponent<FUIManagerComponent>().Remove(FUIPackage.Login);
			await ETTask.CompletedTask;
		}
	}
}

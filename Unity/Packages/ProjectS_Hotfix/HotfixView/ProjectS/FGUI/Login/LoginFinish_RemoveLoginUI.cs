

using Cysharp.Threading.Tasks;
using ET.Client;
using ET.EventType;

namespace ET
{
	[Event(SceneType.Client)]
	public class LoginFinish_RemoveLoginUI: AEvent<Scene, EventType.LoginFinish>
	{
		protected override async UniTask Run(Scene entity, LoginFinish a)
		{
			EnterMapHelper.EnterMapAsync(entity).Coroutine();
			entity.GetComponent<FUIManagerComponent>().Remove(FUIPackage.Login);
			await ETTask.CompletedTask;
		}
	}
}

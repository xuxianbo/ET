

using ET;

namespace ETHotfix
{
    [Event]
	public class LoginFinish_CreateLobbyUI: AEvent<HotfixEventType.LoginFinish>
	{
		protected override async ETTask Run(HotfixEventType.LoginFinish args)
		{
			await UIHelper.Create(args.ZoneScene, UIType.UILobby);
		}
	}
}



using ET;

namespace ETHotfix
{

	[Event]
	public class LoginFinish_RemoveLoginUI: AEvent<HotfixEventType.LoginFinish>
	{
		protected override async ETTask Run(HotfixEventType.LoginFinish args)
		{
			await UIHelper.Remove(args.ZoneScene, UIType.UILogin);
		}
	}
}

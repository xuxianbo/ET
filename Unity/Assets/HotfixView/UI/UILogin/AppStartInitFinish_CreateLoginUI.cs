

using ET;

namespace ETHotfix
{
	public class AppStartInitFinish_RemoveLoginUI: AEvent<HotfixEventType.AppStartInitFinish>
	{
		protected override async ETTask Run(HotfixEventType.AppStartInitFinish args)
		{
			await UIHelper.Create(args.ZoneScene, UIType.UILogin);
		}
	}
}

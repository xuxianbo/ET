using ET;

namespace ETHotfix
{

	[ObjectSystem]	
	public class SessionComponentDestroySystem: DestroySystem<SessionComponent>
	{
		public override void Destroy(SessionComponent self)
		{
			self.Session.Dispose();
		}
	}
}

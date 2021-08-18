
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
	public class UILobbyComponent : Entity
	{
		public GameObject enterMap;
		public Text text;
		
		public void EnterMap()
		{
			MapHelper.EnterMapAsync(this.ZoneScene(), "Map").Coroutine();
		}
	}
}

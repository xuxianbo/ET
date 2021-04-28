using System;

using UnityEngine;

namespace ETHotfix
{
	public class OperaComponent: Entity
    {
        public Vector3 ClickPoint;

	    public int mapMask;

	    public readonly C2M_PathfindingResult frameClickMap = new C2M_PathfindingResult();
    }
}

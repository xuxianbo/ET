using System;
using System.Net;

using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
	public class UILoginComponent: Entity
	{
		public GameObject account;
		public GameObject loginBtn;
		
		public void OnLogin()
		{
			LoginHelper.Login(this.DomainScene(), "127.0.0.1:10002", this.account.GetComponent<InputField>().text).Coroutine();
		}
	}
}

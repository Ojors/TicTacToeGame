using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
	[Serializable]
	public class UIBinding
	{
		public string name;
		public Component component;
	}
	
	public class UILink : MonoBehaviour
	{
		public List<UIBinding> uiBindings;
	}
}
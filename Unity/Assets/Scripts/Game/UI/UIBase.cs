using System.IO;
using Data;
using UnityEngine;

namespace Game.UI
{
	public class UIBase
	{
		public virtual string prefabPath { get; }

		public virtual UILayer uiLayer { get; }

		public virtual bool isStatic => false;

		public GameObject uiGameObject;

		private UILink m_UiLink;

		public virtual void OnShow() {}
		
		public virtual void OnHide() {}

		public virtual void OnDestroy() {}

		public T GetComponent<T>(string bindName) where T : Component
		{
			foreach (UIBinding uiBinding in m_UiLink.uiBindings)
			{
				if (uiBinding.name == bindName && uiBinding.component is T)
				{
					return (T) uiBinding.component;
				}
			}

			return null;
		}

		public void Load()
		{
			GameObject uiPrefab = Resources.Load<GameObject>(prefabPath);
			if (!uiPrefab)
			{
				throw new FileLoadException($"UI Prefab Load Failed. {prefabPath}");
			}

			Transform parentCanvas = null;
			switch (uiLayer)
			{
				case UILayer.Window:
					parentCanvas = UIManager.Instance.windowCanvas;
					break;
				case UILayer.Widget:
					parentCanvas = UIManager.Instance.widgetCanvas;
					break;
				case UILayer.Tool:
					parentCanvas = UIManager.Instance.toolCanvas;
					break;
				case UILayer.Tips:
					parentCanvas = UIManager.Instance.tipsCanvas;
					break;
			}
			
			uiGameObject = Object.Instantiate(uiPrefab, parentCanvas);
			m_UiLink = uiGameObject.GetComponent<UILink>();
		}

		public void SetShow(bool isShow)
		{
			if (uiGameObject)
			{
				uiGameObject.SetActive(isShow);
			}
		}
	}
}
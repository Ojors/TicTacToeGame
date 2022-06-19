using System;
using System.Collections.Generic;
using Data;
using Game.Core.Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.UI
{
	public class UIManager : Singleton<UIManager>
	{
		private Dictionary<UILayer, List<UIBase>> m_UIStack;

		public Transform windowCanvas;
		public Transform widgetCanvas;
		public Transform toolCanvas;
		public Transform tipsCanvas;
		
		protected override void Init()
		{
			m_UIStack = new Dictionary<UILayer, List<UIBase>>();
			InitLayer();
		}

		private void InitLayer()
		{
			GameObject uiRoot = GameObject.Find("UIRoot");
			if (uiRoot)
			{
				windowCanvas = uiRoot.transform.Find("WindowCanvas");
				widgetCanvas = uiRoot.transform.Find("WidgetCanvas");
				toolCanvas = uiRoot.transform.Find("ToolCanvas");
				tipsCanvas = uiRoot.transform.Find("TipsCanvas");
			}
		}

		public T ShowUI<T>() where T : UIBase, new()
		{
			T ui = new T();
			if (!m_UIStack.TryGetValue(ui.uiLayer, out List<UIBase> uiCache))
			{
				uiCache = new List<UIBase>();
				m_UIStack.Add(ui.uiLayer, uiCache);
			}
			
			foreach (UIBase uiBase in uiCache)
			{
				if (uiBase is T)
				{
					uiBase.SetShow(true);
					uiBase.OnShow();
					return uiBase as T;
				}
			}
			
			ui.Load();
			ui.SetShow(true);
			ui.OnShow();
			uiCache.Add(ui);
			return ui;
		}

		public T GetUI<T>() where T : UIBase, new()
		{
			T ui = new T();
			if (m_UIStack.TryGetValue(ui.uiLayer, out List<UIBase> uiCache))
			{
				foreach (UIBase uiBase in uiCache)
				{
					if (uiBase is T)
					{
						return (T) uiBase;
					}
				}
			}

			return null;
		}

		public T CreateUI<T>() where T : UIBase, new()
		{
			T ui = new T();
			ui.Load();
			ui.SetShow(true);
			ui.OnShow();
			return ui;
		}

		public void HideUI<T>(bool isDestroy = true) where T : UIBase, new()
		{
			T ui = new T();
			if (m_UIStack.TryGetValue(ui.uiLayer, out List<UIBase> uiCache))
			{
				Type t = typeof(T);
				for (int i = 0; i < uiCache.Count; i++)
				{
					if (uiCache[i].GetType() == t)
					{
						T targetUI = (T) uiCache[i];
						HideUILogic(targetUI, isDestroy);
						if (isDestroy)
							uiCache.RemoveAt(i);
						return;
					}
				}
			}
		}
		

		private void HideUILogic(UIBase ui, bool isDestroy)
		{
			ui.OnHide();
			if (isDestroy)
			{
				ui.OnDestroy();
				Object.Destroy(ui.uiGameObject);
			}
			else
			{
				ui.SetShow(false);
			}
		}
		
		public void ClearUI()
		{
			foreach (List<UIBase> uiStack in m_UIStack.Values)
			{
				for (int i = uiStack.Count - 1; i >= 0; i--)
				{
					UIBase ui = uiStack[i];
					if (!ui.isStatic)
					{
						HideUILogic(ui, true);
						uiStack.RemoveAt(i);
					}
				}
			}
		}
	}
}
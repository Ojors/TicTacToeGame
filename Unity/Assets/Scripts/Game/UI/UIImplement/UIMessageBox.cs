using System;
using Data;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI.UIImplement
{
	public class UIMessageBox : UIBase
	{
		public override string prefabPath => "Prefab/UI/MessageBox/MessageBox";
		public override UILayer uiLayer => UILayer.Tips;

		private Text m_ContentText;
		private Button m_PositiveBtn;
		private Button m_NegativeBtn;
		private Text m_PositiveBtnText;
		private Text m_NegativeBtnText;
		
		public override void OnShow()
		{
			base.OnShow();
			FindView();
		}

		private void FindView()
		{
			m_ContentText = GetComponent<Text>("ContentText");
			m_PositiveBtn = GetComponent<Button>("PositiveBtn");
			m_NegativeBtn = GetComponent<Button>("NegativeBtn");
			m_PositiveBtnText = GetComponent<Text>("PositiveBtnText");
			m_NegativeBtnText = GetComponent<Text>("NegativeBtnText");
		}

		public void SetContent(string content)
		{
			m_ContentText.text = content;
		}

		public void SetPositiveCallback(UnityAction callback)
		{
			m_PositiveBtn.onClick.AddListener(callback);
		}

		public void SetPositiveBtnText(string text)
		{
			m_PositiveBtnText.text = text;
		}

		public void SetNegativeBtnText(string text)
		{
			m_NegativeBtnText.text = text;
		}

		public void SetNegativeCallback(UnityAction callback)
		{
			m_NegativeBtn.onClick.AddListener(callback);
		}
	}
}
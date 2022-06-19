using System.Collections;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.UIImplement
{
	public class UITips : UIBase
	{
		public override string prefabPath => "Prefab/UI/Tips/Tips";
		public override UILayer uiLayer => UILayer.Tips;

		private Text m_TipsText;
		
		public override void OnShow()
		{
			base.OnShow();
			FindView();
		}

		private void FindView()
		{
			m_TipsText = GetComponent<Text>("TipsText");
		}

		public void SetText(string text)
		{
			m_TipsText.text = text;
		}

		public static void ShowTips(string tips, float showTime)
		{
			AppMain.appInstance.StartCoroutine(ShowTipsForSeconds(tips, showTime));
		}

		private static IEnumerator ShowTipsForSeconds(string tips, float seconds)
		{
			UITips uiTips = UIManager.Instance.ShowUI<UITips>();
			uiTips.SetText(tips);
			
			yield return new WaitForSeconds(seconds);
			UIManager.Instance.HideUI<UITips>();
		}
	}
}
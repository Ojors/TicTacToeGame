using Data;
using Game.Battle;
using Game.Model;
using UnityEngine.UI;

namespace Game.UI.UIImplement
{
	public class UIToolBar : UIBase
	{
		public override string prefabPath => "Prefab/UI/ToolBar/ToolBar";
		public override UILayer uiLayer => UILayer.Tool;

		private Button m_ReturnBtn;
		private Button m_RetryBtn;
		private Text m_TittleText;
		
		public override void OnShow()
		{
			base.OnShow();
			m_ReturnBtn = GetComponent<Button>("ReturnBtn");
			m_RetryBtn = GetComponent<Button>("RetryBtn");
			m_TittleText = GetComponent<Text>("TittleText");
			InitTittle();
			InitBtn();
		}

		private void InitTittle()
		{
			string gameMode = "";
			switch (ModelManager.Instance.battleModel.gameMode)
			{
				case GameMode._3x3:
					gameMode = "3x3";
					break;
				case GameMode._4x4:
					gameMode = "4x4";
					break;
				case GameMode._5x5:
					gameMode = "5x5";
					break;
			}

			string level = "";
			switch (ModelManager.Instance.battleModel.level)
			{
				case Level.Easy:
					level = "简单";
					break;
				case Level.Normal:
					level = "普通";
					break;
				case Level.Hard:
					level = "困难";
					break;
			}

			m_TittleText.text = $"{gameMode} {level}";
		}

		private void InitBtn()
		{
			m_ReturnBtn.onClick.AddListener(OnReturnBtnClick);
			m_RetryBtn.onClick.AddListener(OnRetryBtnClick);
		}

		private void OnRetryBtnClick()
		{
			UIMessageBox messageBox = UIManager.Instance.ShowUI<UIMessageBox>();
			messageBox.SetContent("确定重新开局？");
			messageBox.SetPositiveBtnText("确定");
			messageBox.SetNegativeBtnText("取消");
			messageBox.SetPositiveCallback((() =>
			{
				BattleManager.Instance.ReleaseBattle();
				UIManager.Instance.ClearUI();
				BattleManager.Instance.CreateBattle();
			}));
			messageBox.SetNegativeCallback(() =>
			{
				UIManager.Instance.HideUI<UIMessageBox>();
			});
		}

		private void OnReturnBtnClick()
		{
			UIMessageBox messageBox = UIManager.Instance.ShowUI<UIMessageBox>();
			messageBox.SetContent("确定退出对局？");
			messageBox.SetPositiveCallback((() =>
			{
				BattleManager.Instance.ReleaseBattle();
			}));
			messageBox.SetNegativeCallback(() =>
			{
				UIManager.Instance.HideUI<UIMessageBox>();
			});
		}
	}
}
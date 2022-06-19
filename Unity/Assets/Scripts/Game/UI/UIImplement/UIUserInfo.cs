using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.UIImplement
{
	public class UIUserInfo : UIBase
	{
		public override string prefabPath => "Prefab/UI/UserInfo/UserInfo";
		public override UILayer uiLayer => UILayer.Widget;

		private Image m_RobotHeadImg;
		private Text m_RobotNameText;
		private Text m_RobotStateText;
		private Text m_RobotTimerText;
		
		private Image m_PlayerHeadImg;
		private Text m_PlayerNameText;
		private Text m_PlayerStateText;
		private Text m_PlayerTimerText;
		
		public override void OnShow()
		{
			base.OnShow();
			FindView();
			InitView();
		}

		private void FindView()
		{
			m_RobotHeadImg = GetComponent<Image>("RobotHeadImg");
			m_RobotNameText = GetComponent<Text>("RobotNameText");
			m_RobotStateText = GetComponent<Text>("RobotStateText");
			m_RobotTimerText = GetComponent<Text>("RobotTimerText");
			
			m_PlayerHeadImg = GetComponent<Image>("PlayerHeadImg");
			m_PlayerNameText = GetComponent<Text>("PlayerNameText");
			m_PlayerStateText = GetComponent<Text>("PlayerStateText");
			m_PlayerTimerText = GetComponent<Text>("PlayerTimerText");
		}

		private void InitView()
		{
			m_PlayerTimerText.gameObject.SetActive(false);
			m_RobotTimerText.gameObject.SetActive(false);
		}

		public void HighlightPlayer(bool isHighlight)
		{
			HighlightUser(m_PlayerHeadImg, m_PlayerNameText, m_PlayerStateText, isHighlight);
		}

		public void HighlightRobot(bool isHighlight)
		{
			HighlightUser(m_RobotHeadImg, m_RobotNameText, m_RobotStateText, isHighlight);
		}

		private void HighlightUser(Image head, Text name, Text state, bool isHighlight)
		{
			Color col = isHighlight ? Color.yellow : Color.white;
			string stateText = isHighlight ? "思考中" : "等待中";
			head.color = col;
			name.color = col;
			state.color = col;
			state.text = stateText;
		}

		public void HideUserState()
		{
			m_PlayerStateText.gameObject.SetActive(false);
			m_RobotStateText.gameObject.SetActive(false);
		}

		public void UpdatePlayerCountDown(int second)
		{
			UpdateUserCountDown(second, m_PlayerTimerText);
		}

		public void UpdateRobotCountDown(int second)
		{
			UpdateUserCountDown(second, m_RobotTimerText);
		}

		private void UpdateUserCountDown(int second, Text countDownText)
		{
			if (!countDownText)
				return;
			
			if (second < 0)
			{
				countDownText.gameObject.SetActive(false);
				return;
			}
			countDownText.gameObject.SetActive(true);
			
			Color col = Color.white;
			if (second <= 3)
			{
				col = Color.red;
			}
			else if (second <= 8)
			{
				col = Color.yellow;
			}

			countDownText.text = second.ToString();
			countDownText.color = col;
		}
	}
}
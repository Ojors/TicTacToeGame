using System.Collections;
using Data;
using Game.Core.EventSystem;
using Game.Model;
using Game.UI;
using Game.UI.UIImplement;
using UnityEngine;

namespace Game.Battle.BattleState
{
	public class PlayerTurnState : IBattleState
	{
		public GameState state => GameState.PlayerTurn;
		
		private WaitForSeconds m_WaitForOneSecond = new WaitForSeconds(1);
		private Coroutine m_CountDownCo;
		
		public void Init()
		{
			
		}

		public void OnStateEnter()
		{
			HighlightPlayer(true);
			m_CountDownCo = AppMain.appInstance.StartCoroutine(StartCountDown());
		}

		public void OnStateExit()
		{
			HighlightPlayer(false);
			HideCountDown();
			
			if (m_CountDownCo != null)
				AppMain.appInstance.StopCoroutine(m_CountDownCo);
		}

		private void HighlightPlayer(bool isHighlight)
		{
			UIUserInfo userInfo = UIManager.Instance.GetUI<UIUserInfo>();
			userInfo.HighlightPlayer(isHighlight);
		}

		private IEnumerator StartCountDown()
		{
			UIUserInfo userInfo = UIManager.Instance.GetUI<UIUserInfo>();

			int seconds = 15;
			while (seconds > 0)
			{
				userInfo.UpdatePlayerCountDown(seconds);
				yield return m_WaitForOneSecond;
				--seconds;
			}
			
			var eventData = new BattleUserTimeoutEventData();
			EventSystem.Instance.Dispatch(eventData);
		}

		private void HideCountDown()
		{
			UIUserInfo userInfo = UIManager.Instance.GetUI<UIUserInfo>();
			userInfo.UpdatePlayerCountDown(-1);
		}

		public void Release()
		{
			if (m_CountDownCo != null)
				AppMain.appInstance.StopCoroutine(m_CountDownCo);
		}
	}
}
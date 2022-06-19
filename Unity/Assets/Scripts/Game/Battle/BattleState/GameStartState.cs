using System.Collections;
using Data;
using Game.Core.EventSystem;
using Game.UI.UIImplement;
using UnityEngine;

namespace Game.Battle.BattleState
{
	public class GameStartState : IBattleState
	{
		public GameState state => GameState.GameStart;

		private Coroutine m_GameStartEffectCo;
		
		public void Init()
		{
			
		}

		public void OnStateEnter()
		{
			m_GameStartEffectCo = AppMain.appInstance.StartCoroutine(ShowGameStartEffect());
		}

		private IEnumerator ShowGameStartEffect()
		{
			yield return new WaitForSeconds(0.5f);
			
			float tipsTime = 2;
			UITips.ShowTips("对局开始！", tipsTime);
			
			yield return new WaitForSeconds(tipsTime);
			
			var battleStateChangedEventData = new BattleStateChangedEventData();
			battleStateChangedEventData.gameState = GameState.PlayerTurn;
			EventSystem.Instance.Dispatch(battleStateChangedEventData);
		}

		public void OnStateExit()
		{
			
		}

		public void Release()
		{
			if (m_GameStartEffectCo != null)
			{
				AppMain.appInstance.StopCoroutine(m_GameStartEffectCo);
			}
		}
	}
}
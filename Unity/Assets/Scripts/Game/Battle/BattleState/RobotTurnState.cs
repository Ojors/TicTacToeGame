using System.Collections;
using Data;
using Game.Core.EventSystem;
using Game.Model;
using Game.SceneObject;
using Game.UI;
using Game.UI.UIImplement;
using UnityEngine;

namespace Game.Battle.BattleState
{
	public class RobotTurnState : IBattleState
	{
		public GameState state => GameState.RobotTurn;

		private Coroutine m_DoRobotCalcCo;
		private Coroutine m_CountDownCo;
		private WaitForSeconds m_WaitForOneSecond = new WaitForSeconds(1);
		
		public void Init()
		{
			
		}

		public void OnStateEnter()
		{
			HighlightRobot(true);
			m_DoRobotCalcCo = AppMain.appInstance.StartCoroutine(DoRobotCalc());
			m_CountDownCo = AppMain.appInstance.StartCoroutine(StartCountDown());
		}

		private IEnumerator DoRobotCalc()
		{
			Move nextMove = BattleManager.Instance.GetBattle().sceneObjectManager.ticTacToeAi.CalcNextMove();
			yield return new WaitForSeconds(2);

			Chess chess = BattleManager.Instance.GetBattle().sceneObjectManager.GetChess(nextMove.row, nextMove.col);
			chess?.SetChessState();
		}

		public void OnStateExit()
		{
			HighlightRobot(false);
			HideCountDown();
			if (m_CountDownCo != null)
				AppMain.appInstance.StopCoroutine(m_CountDownCo);
		}
		
		private void HighlightRobot(bool isHighlight)
		{
			UIUserInfo userInfo = UIManager.Instance.GetUI<UIUserInfo>();
			userInfo.HighlightRobot(isHighlight);
		}
		
		private IEnumerator StartCountDown()
		{
			UIUserInfo userInfo = UIManager.Instance.GetUI<UIUserInfo>();

			int seconds = 15;
			while (seconds > 0)
			{
				userInfo.UpdateRobotCountDown(seconds);
				yield return m_WaitForOneSecond;
				--seconds;
			}
			
			var eventData = new BattleUserTimeoutEventData();
			EventSystem.Instance.Dispatch(eventData);
		}
		
		private void HideCountDown()
		{
			UIUserInfo userInfo = UIManager.Instance.GetUI<UIUserInfo>();
			userInfo.UpdateRobotCountDown(-1);
		}

		public void Release()
		{
			if (m_DoRobotCalcCo != null)
				AppMain.appInstance.StopCoroutine(m_DoRobotCalcCo);
			
			if (m_CountDownCo != null)
				AppMain.appInstance.StopCoroutine(m_CountDownCo);
		}
	}
}
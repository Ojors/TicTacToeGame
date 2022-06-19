using System.Collections.Generic;
using Data;
using Game.AI.Minimax;
using Game.Battle.BattleState;
using Game.Core.EventSystem;
using Game.Model;
using Game.SceneObject;
using Game.UI;
using Game.UI.UIImplement;
using UnityEngine;
using EventType = Game.Core.EventSystem.EventType;

namespace Game.Battle
{
	public class Battle
	{
		public SceneObjectManager sceneObjectManager;
		public TicTacToeMinimaxEvaluator minimaxEvaluator;

		public ControlType winner;
		
		private Dictionary<GameState, IBattleState> m_BattleStates;
		private IBattleState m_CurrentBattleState;
		private int m_BattleStateChangedEventId;
		private int m_BattleUserTimeoutEventId;

		public void Init()
		{
			RegistEvents();
			ModelManager.Instance.board.Clear();
			
			sceneObjectManager = new SceneObjectManager();
			sceneObjectManager.Init();
			
			minimaxEvaluator = new TicTacToeMinimaxEvaluator();
			
			m_BattleStates = new Dictionary<GameState, IBattleState>();
			m_BattleStates.Add(GameState.Init, new InitState());
			m_BattleStates.Add(GameState.GameStart, new GameStartState());
			m_BattleStates.Add(GameState.RobotTurn, new RobotTurnState());
			m_BattleStates.Add(GameState.PlayerTurn, new PlayerTurnState());
			m_BattleStates.Add(GameState.Settle, new SettleState());
			m_BattleStates.Add(GameState.End, new EndState());

			foreach (IBattleState battleState in m_BattleStates.Values)
			{
				battleState.Init();
			}

			m_CurrentBattleState = GetBattleState(GameState.Init);
			
			BattleStateChangedEventData battleStateChangedEventData = new BattleStateChangedEventData();
			battleStateChangedEventData.gameState = GameState.Init;
			EventSystem.Instance.Dispatch(battleStateChangedEventData);
		}

		private void RegistEvents()
		{
			m_BattleStateChangedEventId = EventSystem.Instance.Regist(EventType.BattleStateChanged, OnGameStateChanged);
			m_BattleUserTimeoutEventId = EventSystem.Instance.Regist(EventType.BattleUserTimeout, OnUserTimeout);
		}

		public IBattleState GetBattleState(GameState gameState)
		{
			if (m_BattleStates.TryGetValue(gameState, out IBattleState battleState))
			{
				return battleState;
			}

			return null;
		}

		public GameState GetCurrentState()
		{
			return m_CurrentBattleState.state;
		}

		private void OnGameStateChanged(IEventData eventData)
		{
			BattleStateChangedEventData battleStateChangedEventData = (BattleStateChangedEventData) eventData;
			if (m_BattleStates.TryGetValue(battleStateChangedEventData.gameState, out IBattleState battleState))
			{
				m_CurrentBattleState.OnStateExit();
				Debug.Log($"EnterState: {battleStateChangedEventData.gameState}");
				battleState.OnStateEnter();
				
				m_CurrentBattleState = battleState;
			}
		}

		private void OnUserTimeout(IEventData eventData)
		{
			UITips.ShowTips("超时，已自动下子。", 1);
			Move nextMove = BattleManager.Instance.GetBattle().sceneObjectManager.ticTacToeAi.CalcNextMove();
			Chess chess = BattleManager.Instance.GetBattle().sceneObjectManager.GetChess(nextMove.row, nextMove.col);
			chess?.SetChessState();
		}

		public void DoNextState()
		{
			if (IsBattleEnd(out int evaluateResult))
			{
				winner = ControlType.None;
				if (evaluateResult > 0)
					winner = ControlType.Player;
				if (evaluateResult < 0)
					winner = ControlType.Robot;
				
				ChangeBattleState(GameState.Settle);
				return;
			}

			if (GetCurrentState() == GameState.PlayerTurn)
			{
				ChangeBattleState(GameState.RobotTurn);
				return;
			}

			if (GetCurrentState() == GameState.RobotTurn)
			{
				ChangeBattleState(GameState.PlayerTurn);
				return;
			}
		}

		public bool IsBattleEnd(out int evaluateResult)
		{
			evaluateResult = minimaxEvaluator.Evaluate(ModelManager.Instance.board, Define.POSITIVE);
			int emptyMoveCount = ModelManager.Instance.board.GetEmptyMoves().Count;
			return evaluateResult != 0 || emptyMoveCount <= 0;
		}

		private void ChangeBattleState(GameState gameState)
		{
			BattleStateChangedEventData battleStateChangedEventData = new BattleStateChangedEventData();
			battleStateChangedEventData.gameState = gameState;
			EventSystem.Instance.Dispatch(battleStateChangedEventData);
		}
		
		public void Release()
		{
			if (m_BattleStates.TryGetValue(GameState.End, out IBattleState endState))
			{
				endState.OnStateEnter();
				endState.OnStateExit();
			}
			
			foreach (IBattleState battleState in m_BattleStates.Values)
			{
				battleState.Release();
			}
			
			EventSystem.Instance.UnRegist(m_BattleStateChangedEventId);
			EventSystem.Instance.UnRegist(m_BattleUserTimeoutEventId);
			ModelManager.Instance.board.Clear();
			sceneObjectManager.Release();
		}
	}
}
using Data;
using Game.Battle;
using Game.Core.EventSystem;
using Game.Model;
using Game.UI;
using Game.UI.UIImplement;
using UnityEngine.UI;

namespace Game.SceneObject
{
	public class Chess
	{
		private UIChessCell m_ChessCell;

		private bool m_IsSelected;
		private int m_Row, m_Col;
		private int m_ChessSelectEventId;
		
		public void Init(int row, int col)
		{
			m_Row = row;
			m_Col = col;
			
			GridLayoutGroup boardGrid = UIManager.Instance.GetUI<UIBoard>().GetBoardGrid();
			m_ChessCell = UIManager.Instance.CreateUI<UIChessCell>();
			m_ChessCell.uiGameObject.transform.SetParent(boardGrid.transform);
			m_ChessCell.SetCellIndex(row, col);
			m_ChessCell.SetController(this);
		}

		public void DoClick()
		{
			GameState curGameState = BattleManager.Instance.GetBattle().GetCurrentState();
			if (curGameState != GameState.PlayerTurn && curGameState != GameState.RobotTurn)
			{
				return;
			}
			
			if (curGameState == GameState.RobotTurn)
			{
				UITips.ShowTips("请等待对家", 1);
				return;
			}
			
			SetChessState();
		}

		public void SetChessState()
		{
			GameState curGameState = BattleManager.Instance.GetBattle().GetCurrentState();
			ControlType controlType = curGameState == GameState.PlayerTurn ? ControlType.Player : ControlType.Robot;

			m_ChessCell.SetChess(controlType);
			m_IsSelected = true;
			ModelManager.Instance.board.DoMove(m_Row, m_Col, controlType == ControlType.Player ? Define.POSITIVE : Define.OPPOSITE);

			BattleManager.Instance.GetBattle().DoNextState();
		}

		public void Release()
		{
			EventSystem.Instance.UnRegist(m_ChessSelectEventId);
		}
	}
}
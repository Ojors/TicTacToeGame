using Data;
using Game.UI;
using Game.UI.UIImplement;

namespace Game.SceneObject
{
	public class Board
	{
		private UIBoard m_UiBoard;
		
		public void Init()
		{
			m_UiBoard = UIManager.Instance.ShowUI<UIBoard>();
		}

		public Chess[][] InitChessCell()
		{
			Chess[][] chesses = new Chess[Define.BOARD_SIZE][];
			for (int i = 0; i < Define.BOARD_SIZE; i++)
			{
				chesses[i] = new Chess[Define.BOARD_SIZE];
				for (int j = 0; j < Define.BOARD_SIZE; j++)
				{
					Chess chess = new Chess();
					chess.Init(i, j);
					chesses[i][j] = chess;
				}
			}

			return chesses;
		}

		public void Release()
		{
			UIManager.Instance.HideUI<UIBoard>();
		}
	}
}
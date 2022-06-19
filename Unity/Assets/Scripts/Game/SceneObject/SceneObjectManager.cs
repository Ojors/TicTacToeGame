using Game.AI;
using Game.Model;
using Game.UI;
using Game.UI.UIImplement;

namespace Game.SceneObject
{
	public class SceneObjectManager
	{
		public Board board;
		public Chess[][] chess;

		public TicTacToeAI ticTacToeAi;

		public void Init()
		{
			
		}

		public void CreateBoard()
		{
			board = new Board();
			board.Init();
			chess = board.InitChessCell();
		}

		public void CreateAI()
		{
			ticTacToeAi = new TicTacToeAI(ModelManager.Instance.battleModel.level);
		}

		public Chess GetChess(int row, int col)
		{
			if (row >= 0 && row < chess.Length)
			{
				var chesses = chess[row];
				if (col >= 0 && col < chesses.Length)
					return chesses[col];
			}

			return null;
		}

		public void Release()
		{
			board.Release();
			foreach (Chess[] chesses in chess)
			{
				foreach (Chess c in chesses)
				{
					c.Release();
				}
			}
		}
	}
}
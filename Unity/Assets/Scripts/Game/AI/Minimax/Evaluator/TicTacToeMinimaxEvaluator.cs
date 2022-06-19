using System;
using Data;
using Game.Model;

namespace Game.AI.Minimax
{
	public class TicTacToeMinimaxEvaluator : IMinimaxEvaluate
	{
		public int Evaluate(Board board, int playerValue)
		{
			int win = Math.Abs(Define.POSITIVE * Define.BOARD_SIZE);
			for (int i = 0; i < Define.BOARD_SIZE; i++)
			{
				int judgeRow = 0;
				int judgeCol = 0;
				for (int j = 0; j < Define.BOARD_SIZE; j++)
				{
					judgeRow += board[i][j];
					judgeCol += board[j][i];
				}

				if (Math.Abs(judgeRow) == win)
					return judgeRow / Define.BOARD_SIZE == playerValue ? 10 : -10;
				if (Math.Abs(judgeCol) == win)
					return judgeCol / Define.BOARD_SIZE == playerValue ? 10 : -10;
			}

			int judgeDiagonalP = 0;
			int judgeDiagonalN = 0;
			for (int i = 0; i < Define.BOARD_SIZE; i++)
			{
				judgeDiagonalP += board[i][i];
				judgeDiagonalN += board[i][Define.BOARD_SIZE - i - 1];
			}
			if (Math.Abs(judgeDiagonalP) == win)
				return judgeDiagonalP / Define.BOARD_SIZE == playerValue ? 10 : -10;
			if (Math.Abs(judgeDiagonalN) == win)
				return judgeDiagonalN / Define.BOARD_SIZE == playerValue ? 10 : -10;

			return 0;
		}
	}
}
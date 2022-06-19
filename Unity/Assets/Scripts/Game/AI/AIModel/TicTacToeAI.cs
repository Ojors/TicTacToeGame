using System;
using System.Collections.Generic;
using Data;
using Game.AI.Minimax;
using Game.AI.Minimax.Algorithm;
using Game.Model;

namespace Game.AI
{
	public class TicTacToeAI
	{
		public Level m_Level;

		private MinimaxAlgorithm<TicTacToeMinimaxEvaluator> m_Minimax;

		public TicTacToeAI(Level level)
		{
			m_Level = level;
			m_Minimax = new MinimaxAlgorithm<TicTacToeMinimaxEvaluator>(Define.OPPOSITE, Define.POSITIVE);
		}

		public Move CalcNextMove()
		{
			switch (m_Level)
			{
				case Level.Easy:
					return RandomMove(ModelManager.Instance.board);
				case Level.Normal:
					int r = UnityEngine.Random.Range(0, 1);
					return r > 0 ? m_Minimax.FindBestMove(ModelManager.Instance.board) : RandomMove(ModelManager.Instance.board);
				case Level.Hard:
					return m_Minimax.FindBestMove(ModelManager.Instance.board);
			}

			return new Move();
		}

		private Move RandomMove(Board board)
		{
			List<Move> emtpyMoves = board.GetEmptyMoves();
			if (emtpyMoves.Count <= 0)
			{
				throw new Exception("No move exist.");
			}

			int r = UnityEngine.Random.Range(0, emtpyMoves.Count - 1);
			return emtpyMoves[r];
		}
	}
}
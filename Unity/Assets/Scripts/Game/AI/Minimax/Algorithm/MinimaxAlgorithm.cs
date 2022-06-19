using System;
using Data;
using Game.Model;

namespace Game.AI.Minimax.Algorithm
{
    public class MinimaxAlgorithm<T> where T : IMinimaxEvaluate, new ()
    {
        private const int MIN = -1000;
        private const int MAX = 1000;
        
        private int m_PlayerValue;
        private int m_OpponentValue;
        private IMinimaxEvaluate evaluator;

        public MinimaxAlgorithm(int playerValue, int opponentValue)
        {
            m_PlayerValue = playerValue;
            m_OpponentValue = opponentValue;
            evaluator = new T();
        }

        public Move FindBestMove(Board board)
        {
            int bestVal = -1000;
            Move bestMove = new Move(-1, -1);

            for (int i = 0; i < Define.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Define.BOARD_SIZE; j++)
                {
                    if (board[i][j] == 0)
                    {
                        board[i][j] = m_PlayerValue;
                        int moveVal = Minimax(board, 0, false, MIN, MAX);
                        board[i][j] = 0;

                        if (moveVal > bestVal)
                        {
                            bestMove.row = i;
                            bestMove.col = j;
                            bestVal = moveVal;
                        }
                    }
                }
            }

            return bestMove;
        }

        public bool IsMovesLeft(Board board)
        {
            for (int i = 0; i < Define.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Define.BOARD_SIZE; j++)
                {
                    if (board[i][j] == 0)
                        return true;
                }
            }

            return false;
        }

        public int Minimax(Board board, int depth, bool isMax, int alpha, int beta)
        {
            int score = evaluator.Evaluate(board, m_PlayerValue);
            if (score != 0)
                return score;

            if (!IsMovesLeft(board))
                return 0;

            int best = isMax ? MIN : MAX;
            int judgeValue = isMax ? m_PlayerValue : m_OpponentValue;
            Func<int, int, int> minMaxFunc;
            if (isMax)
                minMaxFunc = Math.Max;
            else
                minMaxFunc = Math.Min;

            for (int i = 0; i < Define.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Define.BOARD_SIZE; j++)
                {
                    if (board[i][j] == 0)
                    {
                        board[i][j] = judgeValue;
                        best = minMaxFunc(best, Minimax(board, depth + 1, !isMax, alpha, beta));
                        if (isMax)
                        {
                            alpha = Math.Max(alpha, best);
                            if (beta <= alpha)
                            {
                                board[i][j] = 0;
                                return beta;
                            }
                        }
                        else
                        {
                            beta = Math.Min(beta, best);
                            if (beta <= alpha)
                            {
                                board[i][j] = 0;
                                return alpha;
                            }
                        }
                        board[i][j] = 0;
                    }
                }
            }

            return best;
        }
    }
}
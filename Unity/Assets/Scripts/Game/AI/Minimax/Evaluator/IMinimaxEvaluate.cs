using Game.Model;

namespace Game.AI.Minimax
{
	public interface IMinimaxEvaluate
	{
		/// <summary>
		/// 赢局判定
		/// </summary>
		int Evaluate(Board board, int playerValue);
	}
}
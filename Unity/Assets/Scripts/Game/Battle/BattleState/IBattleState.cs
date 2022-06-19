using Data;

namespace Game.Battle.BattleState
{
	public interface IBattleState
	{
		GameState state { get; }
		void Init();
		void OnStateEnter();
		void OnStateExit();
		void Release();
	}
}
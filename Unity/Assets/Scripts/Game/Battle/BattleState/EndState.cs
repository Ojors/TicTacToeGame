using Data;
using Game.UI;
using Game.UI.UIImplement;

namespace Game.Battle.BattleState
{
	public class EndState : IBattleState
	{
		public GameState state => GameState.End;
		
		public void Init()
		{
			
		}

		public void OnStateEnter()
		{
			
		}

		public void OnStateExit()
		{
			UIManager.Instance.ClearUI();
			UIManager.Instance.ShowUI<UIBattleSelect>();
		}

		public void Release()
		{
		}
	}
}
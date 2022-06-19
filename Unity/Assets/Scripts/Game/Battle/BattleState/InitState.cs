using Data;
using Game.Core.EventSystem;
using Game.UI;
using Game.UI.UIImplement;

namespace Game.Battle.BattleState
{
	public class InitState : IBattleState
	{
		public GameState state => GameState.Init;
		
		public void Init()
		{
			
		}

		public void OnStateEnter()
		{
			ShowBattleUI();
			CreateSceneObject();

			BattleStateChangedEventData battleStateChangedEventData = new BattleStateChangedEventData();
			battleStateChangedEventData.gameState = GameState.GameStart;
			EventSystem.Instance.Dispatch(battleStateChangedEventData);
		}

		private void CreateSceneObject()
		{
			BattleManager.Instance.GetBattle().sceneObjectManager.CreateAI();
			BattleManager.Instance.GetBattle().sceneObjectManager.CreateBoard();
		}

		private void ShowBattleUI()
		{
			UIManager.Instance.ShowUI<UIToolBar>();
			UIManager.Instance.ShowUI<UIUserInfo>();
		}

		public void OnStateExit()
		{
			
		}

		public void Release()
		{
			
		}
	}
}
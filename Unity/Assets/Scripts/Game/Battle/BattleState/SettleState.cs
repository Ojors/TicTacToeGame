using Data;
using Game.UI;
using Game.UI.UIImplement;

namespace Game.Battle.BattleState
{
	public class SettleState : IBattleState
	{
		public GameState state => GameState.Settle;
		
		public void Init()
		{
			
		}

		public void OnStateEnter()
		{
			UIUserInfo userInfo = UIManager.Instance.GetUI<UIUserInfo>();
			userInfo.HideUserState();
			
			string winner = "平局";
			if (BattleManager.Instance.GetBattle().winner == ControlType.Player)
				winner = "胜利";
			if (BattleManager.Instance.GetBattle().winner == ControlType.Robot)
				winner = "失败";
			
			UIMessageBox messageBox = UIManager.Instance.ShowUI<UIMessageBox>();
			messageBox.SetContent($"对局结束，对局结果：{winner}。");
			messageBox.SetPositiveBtnText("返回主界面");
			messageBox.SetNegativeBtnText("重新开始");
			messageBox.SetPositiveCallback((() =>
			{
				BattleManager.Instance.ReleaseBattle();
			}));
			messageBox.SetNegativeCallback(() =>
			{
				BattleManager.Instance.ReleaseBattle();
				
				UIManager.Instance.ClearUI();
				BattleManager.Instance.CreateBattle();
			});
		}

		public void OnStateExit()
		{
			
		}

		public void Release()
		{
			
		}
	}
}
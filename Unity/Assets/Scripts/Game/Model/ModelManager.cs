using Game.Core.Common;

namespace Game.Model
{
	public class ModelManager : Singleton<ModelManager>
	{
		public Board board;
		public BattleModel battleModel;
		
		protected override void Init()
		{
			board = new Board();
			battleModel = new BattleModel();
		}
	}
}
using Game.Core.Common;

namespace Game.Battle
{
	public class BattleManager : Singleton<BattleManager>
	{
		private Battle m_Battle;
		
		protected override void Init()
		{
			base.Init();
		}

		public void CreateBattle()
		{
			m_Battle = new Battle();
			m_Battle.Init();
		}

		public Battle GetBattle()
		{
			return m_Battle;
		}

		public void ReleaseBattle()
		{
			if (m_Battle != null)
			{
				m_Battle.Release();
				m_Battle = null;
			}
		}
	}
}
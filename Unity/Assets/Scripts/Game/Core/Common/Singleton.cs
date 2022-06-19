namespace Game.Core.Common
{
	public class Singleton<T> where T : Singleton<T>, new()
	{
		public static T Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new T();
					m_Instance.Init();
				}
				return m_Instance;
			}
		}

		private static T m_Instance;
		
		protected virtual void Init() {}
	}
}
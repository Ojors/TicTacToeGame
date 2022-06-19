using System;
using System.Collections.Generic;
using Game.Core.Common;

namespace Game.Core.EventSystem
{
	public class EventSystem : Singleton<EventSystem>
	{
		private int m_EventId;
		private Dictionary<EventType, Dictionary<int, Action<IEventData>>> m_EventContainer;
		
		protected override void Init()
		{
			m_EventId = 0;
			m_EventContainer = new Dictionary<EventType, Dictionary<int, Action<IEventData>>>();
		}

		public int Regist(EventType eventType, Action<IEventData> callback)
		{
			if (!m_EventContainer.TryGetValue(eventType, out Dictionary<int, Action<IEventData>> callbacks))
			{
				callbacks = new Dictionary<int, Action<IEventData>>();
				m_EventContainer.Add(eventType, callbacks);
			}

			++m_EventId;
			if (callbacks.ContainsKey(m_EventId))
				throw new InvalidProgramException("EventSystem eventId Error");
			
			callbacks.Add(m_EventId, callback);
			return m_EventId;
		}

		public void UnRegist(int eventId)
		{
			foreach (var callbacks in m_EventContainer.Values)
			{
				if (callbacks.ContainsKey(eventId))
				{
					callbacks.Remove(eventId);
				}
			}
		}

		public void Dispatch(IEventData eventData)
		{
			if (m_EventContainer.TryGetValue(eventData.eventType, out Dictionary<int, Action<IEventData>> callbacks))
			{
				foreach (Action<IEventData> callback in callbacks.Values)
				{
					callback?.Invoke(eventData);
				}
			}
		}
	}
}
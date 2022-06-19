using Data;

namespace Game.Core.EventSystem
{
	public interface IEventData
	{
		EventType eventType { get; }
	}

	public class BattleStateChangedEventData : IEventData
	{
		public EventType eventType => EventType.BattleStateChanged;
		public GameState gameState;
	}
	
	public class BattleUserChessClickEventData : IEventData
	{
		public EventType eventType => EventType.BattleChessSelect;
		public int row, col;
		public ControlType controlType;
	}

	public class BattleUserTimeoutEventData : IEventData
	{
		public EventType eventType => EventType.BattleUserTimeout;
	}
}
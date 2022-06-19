namespace Data
{
	public class Define
	{
		public static int POSITIVE = 1;
		public static int OPPOSITE = -1;
		public static int BOARD_SIZE = 3;
	}
	
	public enum Level
	{
		Easy,
		Normal,
		Hard,
	}

	public enum GameMode
	{
		_3x3,
		_4x4,
		_5x5,
	}

	public enum UILayer
	{
		Window,
		Widget,
		Tool,
		Tips
	}

	public enum ControlType
	{
		None,
		Player,
		Robot,
	}

	public enum GameState
	{
		Init,
		GameStart,
		RobotTurn,
		PlayerTurn,
		Settle,
		End,
	}
}
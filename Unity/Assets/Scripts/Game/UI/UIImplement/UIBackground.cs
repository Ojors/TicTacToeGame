using Data;

namespace Game.UI.UIImplement
{
	public class UIBackground : UIBase
	{
		public override string prefabPath => "Prefab/UI/Background/Background";
		public override UILayer uiLayer => UILayer.Window;
		public override bool isStatic => true;
	}
}
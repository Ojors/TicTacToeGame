using Data;
using Game.Model;
using Game.SceneObject;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.UIImplement
{
	public class UIBoard : UIBase
	{
		public override string prefabPath => "Prefab/UI/Board/Board";
		public override UILayer uiLayer => UILayer.Widget;

		private GridLayoutGroup m_BoardGridLayoutGroup;

		public override void OnShow()
		{
			base.OnShow();
			m_BoardGridLayoutGroup = GetComponent<GridLayoutGroup>("BoardGrid");
			InitGrid();
		}

		private void InitGrid()
		{
			m_BoardGridLayoutGroup.constraintCount = Define.BOARD_SIZE;
			float boardW = ((RectTransform) (m_BoardGridLayoutGroup.transform)).sizeDelta.x;
			float cellSize = (boardW - 20 - 10 * (Define.BOARD_SIZE - 1)) / Define.BOARD_SIZE;
			m_BoardGridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
		}

		public GridLayoutGroup GetBoardGrid()
		{
			return m_BoardGridLayoutGroup;
		}
	}
}
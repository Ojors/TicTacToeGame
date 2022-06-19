using Data;
using Game.SceneObject;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.UIImplement
{
	public class UIChessCell : UIBase
	{
		public override string prefabPath => "Prefab/UI/Chess/ChessCell";
		public override UILayer uiLayer => UILayer.Widget;

		private string m_PlayerChessImgPath = "Textures/Chess/Chess_Circle";
		private string m_RobotChessImgPath = "Textures/Chess/Chess_Cross";

		private Image m_CellBgImg;
		private Image m_ChessImg;
		private Button m_ChessBtn;

		private Chess m_ChessCtrl;
		private int m_Row;
		private int m_Col;
		
		public override void OnShow()
		{
			base.OnShow();
			FindView();
			InitView();
			InitCell();
		}

		private void FindView()
		{
			m_CellBgImg = GetComponent<Image>("CellBgImg");
			m_ChessImg = GetComponent<Image>("ChessImg");
			m_ChessBtn = GetComponent<Button>("ChessBtn");
		}

		private void InitView()
		{
			m_ChessImg.enabled = false;
		}

		public void SetCellIndex(int row, int col)
		{
			m_Row = row;
			m_Col = col;
			m_ChessImg.name = $"Chess{row}_{col}";
		}

		public void SetController(Chess chessCtrl)
		{
			m_ChessCtrl = chessCtrl;
		}

		private void InitCell()
		{
			m_ChessBtn.onClick.AddListener(OnChessBtnClick);
		}

		public void SetChess(ControlType controlType)
		{
			Sprite chessImg = controlType == ControlType.Player ? Resources.Load<Sprite>(m_PlayerChessImgPath) : Resources.Load<Sprite>(m_RobotChessImgPath);
			m_ChessImg.enabled = true;
			m_ChessImg.sprite = chessImg;
			m_ChessBtn.interactable = false;
		}

		private void OnChessBtnClick()
		{
			m_ChessCtrl.DoClick();
		}
	}
}
using Data;
using Game.Battle;
using Game.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.UIImplement
{
	public class UIBattleSelect : UIBase
	{
		public override string prefabPath => "Prefab/UI/BattleSelect/BattleSelect";
		public override UILayer uiLayer => UILayer.Widget;

		private Dropdown m_LevelDropdown;
		private Dropdown m_GameModeDropdown;
		private Button m_GameStartBtn;
		
		public override void OnShow()
		{
			base.OnShow();
			FindView();
			InitView();
			BindCallback();
		}

		private void FindView()
		{
			m_LevelDropdown = GetComponent<Dropdown>("LevelDropdown");
			m_GameModeDropdown = GetComponent<Dropdown>("GameModeDropdown");
			m_GameStartBtn = GetComponent<Button>("GameStartBtn");
		}

		private void InitView()
		{
			m_LevelDropdown.value = (int) ModelManager.Instance.battleModel.level;
			m_GameModeDropdown.value = (int) ModelManager.Instance.battleModel.gameMode;
		}

		private void BindCallback()
		{
			m_LevelDropdown.onValueChanged.AddListener(OnLevelDropdownChanged);
			m_GameModeDropdown.onValueChanged.AddListener(OnGameModeDropdownChanged);
			m_GameStartBtn.onClick.AddListener(OnGameStartClick);
		}

		private void OnLevelDropdownChanged(int index)
		{
			ModelManager.Instance.battleModel.level = (Level) index;
		}

		private void OnGameModeDropdownChanged(int index)
		{
			ModelManager.Instance.battleModel.gameMode = (GameMode) index;
			int boardSize = 3;
			switch ((GameMode) index)
			{
				case GameMode._3x3:
					boardSize = 3;
					break;
				case GameMode._4x4:
					boardSize = 4;
					break;
				case GameMode._5x5:
					boardSize = 5;
					break;
			}

			Define.BOARD_SIZE = boardSize;
		}

		private void OnGameStartClick()
		{
			UIManager.Instance.ClearUI();
			BattleManager.Instance.CreateBattle();
		}
	}
}
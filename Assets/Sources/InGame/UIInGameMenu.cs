using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Casual
{
	public class UIInGameMenu : MonoBehaviour
	{
		UIInGameMenuLogic m_logic;

		[SerializeField]
		GameObject m_goRoot = null;		

		[SerializeField]
		GameObject[] m_goCheck = new GameObject[(int)UIInGameMenuLogic.eMenuState.CNT];
		

		private void Awake()
		{
			_CreateLogic();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				m_logic.SelectMenu();
			}
			else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W))		
			{
				_MoveCursor(true);
			}
			else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
			{
				_MoveCursor(false);
			}
			else if(Input.GetKeyDown(KeyCode.Escape))
			{
				m_logic.ChangeToDefaultState();
			}
		}

		void _CreateLogic()
		{
			if(m_logic == null)
			{
				var gameContainer = LifetimeScope.Find<GameLifetimeScope>().Container;
				var gameStateManager = gameContainer.Resolve<IGameStateManager>();
				var choiceController = gameContainer.Resolve<IChoiceObjectController>();
				var mainManager = LifetimeScope.Find<RootLifetimeScope>().Container.Resolve<IMainManager>();
				m_logic = new UIInGameMenuLogic(mainManager, choiceController, gameStateManager);
			}
		}

		void _MoveCursor(bool isUp)
		{
			m_logic.MoveCursor(isUp);
			_SetCheckCursor();
		}

		void _SetCheckCursor()
		{
			for (int i = 0; i < (int)UIInGameMenuLogic.eMenuState.CNT; i++)
			{
				m_goCheck[i].SetActive(i == (int)m_logic.GetCurSel());
			}
		}


		public void OnClickCursor(int iSel)
		{
			m_logic.SetCurSel((UIInGameMenuLogic.eMenuState)iSel);
			_SetCheckCursor();
			m_logic.SelectMenu();
		}


		public void ShowMenu()
		{
			m_goRoot.SetActive(true);
			m_logic.SetCurSel(UIInGameMenuLogic.eMenuState.SAVE);
			_SetCheckCursor();
		}

		public void HideMenu()
		{
			m_goRoot.SetActive(false);
		}
	}

	public class UIInGameMenuLogic
	{
		IMainManager m_mainManager;
		IChoiceObjectController m_choiceController;
		IGameStateManager m_gameStateManager;

		public enum eMenuState
		{
			SAVE = 0,
			LOAD,
			CLOSE,
			EXIT,
			CNT
		}
		eMenuState m_eMenuSel = eMenuState.SAVE;

		public UIInGameMenuLogic(IMainManager mainManager, IChoiceObjectController choiceController, IGameStateManager gameStateManager)
		{
			m_mainManager = mainManager;
			m_choiceController = choiceController;
			m_gameStateManager = gameStateManager;
		}

		public void SelectMenu()
		{
			switch (m_eMenuSel)
			{
				case eMenuState.SAVE:
					m_mainManager.saveLoadManager.Save();
					ChangeToDefaultState();
					break;
				case eMenuState.LOAD:
					m_mainManager.saveLoadManager.Load();
					m_choiceController.UpdateObjects();
					ChangeToDefaultState();
					break;
				case eMenuState.CLOSE:
					ChangeToDefaultState();
					break;
				case eMenuState.EXIT:
					m_mainManager.ChangeState(new StateMenu());
					break;
			}
		}

		public void ChangeToDefaultState()
		{
			m_gameStateManager.ChangeState(new DefaultState());
		}

		public void MoveCursor(bool isUp)
		{
			int iPlus = isUp ? -1 : 1;
			m_eMenuSel += iPlus;
			if (m_eMenuSel < 0)
				m_eMenuSel = (eMenuState.CNT - 1);
			else if (m_eMenuSel >= eMenuState.CNT)
				m_eMenuSel = eMenuState.SAVE;
		}

		public void SetCurSel(eMenuState eMenuSel) { m_eMenuSel = eMenuSel; }
		public eMenuState GetCurSel() { return m_eMenuSel; }
	}
}

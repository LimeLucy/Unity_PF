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

		/// <summary>
		/// 로직 class 생성
		/// </summary>
		void _CreateLogic()
		{
			if(m_logic == null)
			{
				var gameContainer = LifetimeScope.Find<GameLifetimeScope>().Container;
				var gameStateManager = gameContainer.Resolve<IGameStateManager>();
				var gameRoot = gameContainer.Resolve<IGameRoot>();
				var container = LifetimeScope.Find<RootLifetimeScope>().Container;
				var mainManager = container.Resolve<IMainStateManager>();
				var saveLoadManager = container.Resolve<ISaveLoadManager>();
				var gameSwitch = container.Resolve<GameSwitch>();
				m_logic = new UIInGameMenuLogic(mainManager, gameStateManager, saveLoadManager, gameSwitch, gameRoot);
			}
		}

		/// <summary>
		/// 커서 이동 처리
		/// </summary>
		/// <param name="isUp"></param>
		void _MoveCursor(bool isUp)
		{
			m_logic.MoveCursor(isUp);
			_SetCheckCursor();
		}

		/// <summary>
		/// 이동된 커서값에 따라 실제 UI 커서 처리
		/// </summary>
		void _SetCheckCursor()
		{
			for (int i = 0; i < (int)UIInGameMenuLogic.eMenuState.CNT; i++)
			{
				m_goCheck[i].SetActive(i == (int)m_logic.GetCurSel());
			}
		}

		/// <summary>
		/// Inspector에서 터치시 불리는 커서 함수
		/// </summary>
		/// <param name="iSel"></param>
		public void OnClickCursor(int iSel)
		{
			m_logic.SetCurSel((UIInGameMenuLogic.eMenuState)iSel);
			_SetCheckCursor();
			m_logic.SelectMenu();
		}

		/// <summary>
		/// 메뉴 보여주기
		/// </summary>
		public void ShowMenu()
		{
			m_goRoot.SetActive(true);
			m_logic.SetCurSel(UIInGameMenuLogic.eMenuState.SAVE);
			_SetCheckCursor();
		}

		/// <summary>
		/// 메뉴 숨기기
		/// </summary>
		public void HideMenu()
		{
			m_goRoot.SetActive(false);
		}
	}

	public class UIInGameMenuLogic
	{
		IMainStateManager m_mainManager;
		IGameStateManager m_gameStateManager;
		ISaveLoadManager m_saveLoadManager;
		IGameRoot m_gameRoot;
		GameSwitch	m_gameSwitch;

		public enum eMenuState
		{
			SAVE = 0,
			LOAD,
			CLOSE,
			EXIT,
			CNT
		}
		eMenuState m_eMenuSel = eMenuState.SAVE;

		public UIInGameMenuLogic(IMainStateManager mainManager, IGameStateManager gameStateManager, ISaveLoadManager saveLoadManager, GameSwitch gameSwitch, IGameRoot gameRoot)
		{
			m_mainManager = mainManager;
			m_gameStateManager = gameStateManager;
			m_saveLoadManager = saveLoadManager;
			m_gameSwitch = gameSwitch;
			m_gameRoot = gameRoot;
		}

		/// <summary>
		/// 메뉴 선택시 처리
		/// </summary>
		public void SelectMenu()
		{
			switch (m_eMenuSel)
			{
				case eMenuState.SAVE:
					m_saveLoadManager.Save();
					ChangeToDefaultState();
					break;
				case eMenuState.LOAD:
					m_saveLoadManager.Load();
					m_gameSwitch.CheckOnOffs();
					ChangeToDefaultState();
					break;
				case eMenuState.CLOSE:
					ChangeToDefaultState();
					break;
				case eMenuState.EXIT:
					m_gameRoot.Dispose();
					m_mainManager.ChangeState(new StateMenu());
					break;
			}
		}

		/// <summary>
		/// 다시 게임 상태로 돌아가기
		/// </summary>
		public void ChangeToDefaultState()
		{
			m_gameStateManager.ChangeState(new DefaultState());
		}

		/// <summary>
		/// 커서 이동
		/// </summary>
		/// <param name="isUp">true : 위로, false : 아래로</param>
		public void MoveCursor(bool isUp)
		{
			int iPlus = isUp ? -1 : 1;
			m_eMenuSel += iPlus;
			if (m_eMenuSel < 0)
				m_eMenuSel = (eMenuState.CNT - 1);
			else if (m_eMenuSel >= eMenuState.CNT)
				m_eMenuSel = eMenuState.SAVE;
		}

		/// <summary>
		/// 커서 선택 설정
		/// </summary>
		/// <param name="eMenuSel"></param>
		public void SetCurSel(eMenuState eMenuSel) { m_eMenuSel = eMenuSel; }

		/// <summary>
		/// 현재 선택된 커서 return
		/// </summary>
		/// <returns></returns>
		public eMenuState GetCurSel() { return m_eMenuSel; }
	}
}

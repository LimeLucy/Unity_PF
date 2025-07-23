using UnityEngine;

namespace Casual
{
	public class UIInGameMenu : MonoBehaviour
	{
		[SerializeField]
		GameObject m_goRoot = null;

		enum eMenuState 
		{
			SAVE = 0,
			LOAD,
			CLOSE,
			EXIT,
			CNT
		}

		[SerializeField]
		GameObject[] m_goCheck = new GameObject[(int)eMenuState.CNT];

		eMenuState m_eMenuSel = eMenuState.SAVE;

	#region unity ÇÔ¼ö
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				_SelectMenu();
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
				_ChangeToDefaultState();
			}
		}
	#endregion

		void _SelectMenu()
		{
			switch(m_eMenuSel)
			{
				case eMenuState.SAVE :
					MainManager.instance.saveLoadManager.Save();
					_ChangeToDefaultState();
					break;
				case eMenuState.LOAD :
					MainManager.instance.saveLoadManager.Load();
					GameEngine.instance.SetObjects();
					_ChangeToDefaultState();
					break;
				case eMenuState.CLOSE :
					_ChangeToDefaultState();
					break;
				case eMenuState.EXIT :
					MainManager.instance.ChangeState(new StateMenu());
					break;
			}
		}

		void _MoveCursor(bool isUp)
		{
			int iPlus = isUp ? -1 : 1;
			m_eMenuSel += iPlus;
			if(m_eMenuSel < 0)
				m_eMenuSel = (eMenuState.CNT - 1);
			else if(m_eMenuSel >= eMenuState.CNT)
				m_eMenuSel = eMenuState.SAVE;

			_SetCheckCursor();
		}

		void _SetCheckCursor()
		{
			for (int i = 0; i < (int)eMenuState.CNT; i++)
			{
				m_goCheck[i].SetActive(i == (int)m_eMenuSel);
			}
		}

		void _ChangeToDefaultState()
		{
			GameStateManager.instance.ChangeState(new DefaultState());
		}

		public void OnClickCursor(int eSel)
		{
			m_eMenuSel = (eMenuState)eSel;
			_SetCheckCursor();
			_SelectMenu();
		}


		public void ShowMenu()
		{
			m_goRoot.SetActive(true);
			m_eMenuSel = eMenuState.SAVE;
			_SetCheckCursor();
		}

		public void HideMenu()
		{
			m_goRoot.SetActive(false);
		}
	}
}

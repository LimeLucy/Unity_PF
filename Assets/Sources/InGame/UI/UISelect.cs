using TMPro;
using VContainer;
using UnityEngine;
using VContainer.Unity;

namespace Casual
{
	public class UISelect : MonoBehaviour
	{
		UISelectLogic m_logic = null;

		[SerializeField]
		GameObject m_goRoot = null;

		[SerializeField]
		TextMeshProUGUI m_tmpDialogue = null;

		const int CNT_SELECT = 2;
		[SerializeField]
		TextMeshProUGUI[] m_tmpAnswer = new TextMeshProUGUI[CNT_SELECT];
		[SerializeField]
		GameObject[] m_goCheck = new GameObject[CNT_SELECT];
		
		SelectState m_selectState = null;

		private void Awake()
		{
			_CreateLogic();
		}

		private void OnDestroy()
		{
			m_logic = null;
		}

		public void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				EndSelect();
			}
			else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
			{
				SetSelIdx(m_logic.GetCurIdx() == 0 ? 1 : 0);
			}
		}

		void _CreateLogic()
		{
			if (m_logic == null)
			{
				var container = LifetimeScope.Find<RootLifetimeScope>().Container;
				var mainManager = container.Resolve<IMainStateManager>();
				var gameSwitch = container.Resolve<GameSwitch>();
				var gameStateManager = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IGameStateManager>();
				m_logic = new UISelectLogic(mainManager, gameStateManager, gameSwitch);
			}
		}

		/// <summary>
		/// 선택지 텍스트 셋팅 및 초기화
		/// </summary>
		/// <param name="selectState"></param>
		public void SetSelectText(SelectState selectState)
		{
			_CreateLogic();

			m_selectState = selectState;
			m_tmpDialogue.text = selectState.m_select.m_strQuest;
			SetSelIdx(0);
			m_tmpAnswer[0].text = selectState.m_select.m_strAns[0];
			m_tmpAnswer[1].text = selectState.m_select.m_strAns[1];
			m_goRoot.SetActive(true);
		}

		/// <summary>
		/// 선택지 UI 숨김
		/// </summary>
		public void HideSelectUI()
		{
			m_goRoot.SetActive(false);
		}

		/// <summary>
		/// 선택지 인덱스 셋팅 및 UI 변경
		/// </summary>
		/// <param name="iSelIdx"> 선택되는 index </param>
		public void SetSelIdx(int iSelIdx)
		{
			m_logic.SetCurIdx(iSelIdx);
			for(int i = 0; i < CNT_SELECT; i++)
				m_goCheck[i].SetActive(iSelIdx == i);
		}

		/// <summary>
		/// 선택지 선택 완료
		/// </summary>
		public void EndSelect()
		{
			m_logic.EndSelect(m_selectState);
		}
	}

	public class UISelectLogic
	{
		IMainStateManager m_mainManager;
		IGameStateManager m_gameStateManager;
		GameSwitch m_gameSwitch;

		int m_iSelIdx = 0; // 현재 선택되고 있는 index

		public UISelectLogic(IMainStateManager mainManager, IGameStateManager gameStateManager, GameSwitch gameSwitch)
		{
			m_mainManager = mainManager;
			m_gameStateManager = gameStateManager;
			m_gameSwitch = gameSwitch;
		}

		/// <summary>
		/// 선택지 선택 완료
		/// </summary>
		public void EndSelect(SelectState sState)
		{
			if (sState.m_select.m_iAnsSwitchOnIdx[m_iSelIdx] != 0)
				m_gameSwitch.SetSwitch(sState.m_select.m_iAnsSwitchOnIdx[m_iSelIdx], true);

			if (sState.m_select.m_iAnsSwitchOffIdx[m_iSelIdx] != 0)
				m_gameSwitch.SetSwitch(sState.m_select.m_iAnsSwitchOffIdx[m_iSelIdx], false);

			m_gameStateManager.ChangeState(new DefaultState());
		}

		public void SetCurIdx(int iIdx)
		{
			m_iSelIdx = iIdx;
		}

		public int GetCurIdx()	{ return m_iSelIdx; }
	}
}

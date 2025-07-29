using System.Collections;
using VContainer;
using TMPro;
using UnityEngine;
using VContainer.Unity;

namespace Casual
{
	public class UIDialogue : MonoBehaviour
	{
		UIDialogueLogic m_logic = null;

		[SerializeField]
		GameObject m_goDialogueUI = null;
		TextMeshProUGUI m_tmpDialogue = null;		

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
			if (m_goDialogueUI.activeInHierarchy)
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					_NextPage();
				}
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
				m_logic = new UIDialogueLogic(mainManager, gameStateManager, gameSwitch);
			}
		}

		/// <summary>
		/// 다이얼로그 텍스트 및 기본 셋팅
		/// </summary>
		/// <param name="script"> 사용되는 Script 연결 </param>
		public IEnumerator SetDialogueText(Scripts script)
		{
			_CreateLogic();

			m_tmpDialogue = this.GetComponentInChildren<TextMeshProUGUI>(true);
			m_logic.SetDialogueScript(script);
			m_tmpDialogue.text = m_logic.GetDialogueText();;
			m_goDialogueUI.SetActive(true);
			yield return new WaitForEndOfFrame(); // 한번 화면에 갱신되기 전에는 pagecount가 갱신되지 않으므로 wait
			m_logic.SetTotalPage(m_tmpDialogue.textInfo.pageCount);
		}

		/// <summary>
		/// 다이얼로그 UI 숨김
		/// </summary>
		public void HideDialogueUI()
		{
			m_goDialogueUI.SetActive(false);
		}

		/// <summary>
		/// 다음 페이지로 넘깁니다. 마지막 페이지일 경우 상태가 기본으로 돌아갑니다.
		/// </summary>
		void _NextPage()
		{
			if(!m_logic.NextPage(m_tmpDialogue))
				HideDialogueUI();
		}
	}

	public class UIDialogueLogic
	{
		IMainStateManager m_mainManager;
		IGameStateManager m_gameStateManager;
		GameSwitch m_gameSwitch;

		Scripts m_script;

		int m_iTotalPage = 0;
		int m_iCurPage = 0;

		public UIDialogueLogic(IMainStateManager mainManager, IGameStateManager gameStateManager, GameSwitch gameSwitch)
		{
			m_mainManager = mainManager;
			m_gameStateManager = gameStateManager;
			m_gameSwitch = gameSwitch;
		}

		public void SetDialogueScript(Scripts script)
		{
			m_script = script;
		}

		public string GetDialogueText()
		{
			bool isCheckSwitchOn = m_gameSwitch.GetSwitch(m_script.m_iCheckEventIdx);
			return isCheckSwitchOn ? m_script.m_strTrueText : m_script.m_strFalseText;
		}

		public void SetTotalPage(int iTotalPage)
		{
			m_iTotalPage = iTotalPage;
			m_iCurPage = 1;
		}

		public bool NextPage(TextMeshProUGUI textMesh)
		{
			bool isContinue = m_iTotalPage > m_iCurPage;
			if (isContinue)
				m_iCurPage = ++textMesh.pageToDisplay;
			else
			{
				if(m_script.m_iOnEventIdx != -1)
					m_gameSwitch.SetSwitch(m_script.m_iOnEventIdx, true);
				m_gameStateManager.ChangeState(new DefaultState());
			}
			return isContinue;
		}
	}
}

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

		private void Update()
		{
			if (m_goDialogueUI.activeInHierarchy)
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					_NextPage();
				}
			}
		}

		/// <summary>
		/// 로직 부분 담당할 class 생성
		/// </summary>
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
		/// 다음 페이지로 넘깁니다. 마지막 페이지일 경우 상태가 기본으로 돌아갑니다.
		/// </summary>
		void _NextPage()
		{
			if (!m_logic.NextPage(m_tmpDialogue))
				HideDialogueUI();
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

		/// <summary>
		/// 다이얼로그 스크립트 셋팅
		/// </summary>
		/// <param name="script"></param>
		public void SetDialogueScript(Scripts script)
		{
			m_script = script;
		}

		/// <summary>
		/// 다이얼로그에서 사용될 텍스트 리턴
		/// </summary>
		/// <returns></returns>
		public string GetDialogueText()
		{
			bool isCheckSwitchOn = m_gameSwitch.GetSwitch(m_script.m_iCheckEventIdx);
			return isCheckSwitchOn ? m_script.m_strTrueText : m_script.m_strFalseText;
		}

		/// <summary>
		/// 전체 페이지 카운트 셋팅
		/// </summary>
		/// <param name="iTotalPage"></param>
		public void SetTotalPage(int iTotalPage)
		{
			m_iTotalPage = iTotalPage;
			m_iCurPage = 1;
		}

		/// <summary>
		/// 다음 페이지 처리
		/// </summary>
		/// <param name="textMesh"></param>
		/// <returns></returns>
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

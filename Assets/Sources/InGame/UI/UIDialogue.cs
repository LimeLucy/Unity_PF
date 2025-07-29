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
		/// ���̾�α� �ؽ�Ʈ �� �⺻ ����
		/// </summary>
		/// <param name="script"> ���Ǵ� Script ���� </param>
		public IEnumerator SetDialogueText(Scripts script)
		{
			_CreateLogic();

			m_tmpDialogue = this.GetComponentInChildren<TextMeshProUGUI>(true);
			m_logic.SetDialogueScript(script);
			m_tmpDialogue.text = m_logic.GetDialogueText();;
			m_goDialogueUI.SetActive(true);
			yield return new WaitForEndOfFrame(); // �ѹ� ȭ�鿡 ���ŵǱ� ������ pagecount�� ���ŵ��� �����Ƿ� wait
			m_logic.SetTotalPage(m_tmpDialogue.textInfo.pageCount);
		}

		/// <summary>
		/// ���̾�α� UI ����
		/// </summary>
		public void HideDialogueUI()
		{
			m_goDialogueUI.SetActive(false);
		}

		/// <summary>
		/// ���� �������� �ѱ�ϴ�. ������ �������� ��� ���°� �⺻���� ���ư��ϴ�.
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

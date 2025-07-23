using System.Collections;
using TMPro;
using UnityEngine;

namespace Casual
{
	public class UIDialogue : MonoBehaviour
	{
		[SerializeField]
		GameObject m_goDialogueUI = null;
		TextMeshProUGUI m_tmpDialogue = null;

		int m_iTotalPage = 0;
		int m_iCurPage = 0;

		#region Unity �Լ�
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
		#endregion

		/// <summary>
		/// ���̾�α� �ؽ�Ʈ �� �⺻ ����
		/// </summary>
		/// <param name="script"> ���Ǵ� Script ���� </param>
		public IEnumerator SetDialogueText(Scripts script)
		{
			m_tmpDialogue = this.GetComponentInChildren<TextMeshProUGUI>(true);

			bool isCheckSwitchOn = MainManager.instance.gameSwitch.GetSwitch(script.m_iCheckEventIdx);			
			m_tmpDialogue.text = isCheckSwitchOn ? script.m_strTrueText : script.m_strFalseText;
			m_goDialogueUI.SetActive(true);
			yield return new WaitForEndOfFrame(); // �ѹ� ȭ�鿡 ���ŵǱ� ������ pagecount�� ���ŵ��� �����Ƿ� wait
			m_iTotalPage = m_tmpDialogue.textInfo.pageCount;
			m_iCurPage = 1;
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
			if (m_iTotalPage > m_iCurPage)
				m_iCurPage = ++m_tmpDialogue.pageToDisplay;
			else
				GameStateManager.instance.ChangeState(new DefaultState());
		}
	}
}

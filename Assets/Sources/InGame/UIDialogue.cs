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

		#region Unity 함수
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
		/// 다이얼로그 텍스트 및 기본 셋팅
		/// </summary>
		/// <param name="script"> 사용되는 Script 연결 </param>
		public IEnumerator SetDialogueText(Scripts script)
		{
			m_tmpDialogue = this.GetComponentInChildren<TextMeshProUGUI>(true);

			bool isCheckSwitchOn = MainManager.instance.gameSwitch.GetSwitch(script.m_iCheckEventIdx);			
			m_tmpDialogue.text = isCheckSwitchOn ? script.m_strTrueText : script.m_strFalseText;
			m_goDialogueUI.SetActive(true);
			yield return new WaitForEndOfFrame(); // 한번 화면에 갱신되기 전에는 pagecount가 갱신되지 않으므로 wait
			m_iTotalPage = m_tmpDialogue.textInfo.pageCount;
			m_iCurPage = 1;
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
			if (m_iTotalPage > m_iCurPage)
				m_iCurPage = ++m_tmpDialogue.pageToDisplay;
			else
				GameStateManager.instance.ChangeState(new DefaultState());
		}
	}
}

using TMPro;
using UnityEngine;

namespace Casual
{
	public class UISelect : MonoBehaviour
	{
		[SerializeField]
		GameObject m_goRoot = null;

		[SerializeField]
		TextMeshProUGUI m_tmpDialogue = null;

		const int CNT_SELECT = 2;
		[SerializeField]
		TextMeshProUGUI[] m_tmpAnswer = new TextMeshProUGUI[CNT_SELECT];
		[SerializeField]
		GameObject[] m_goCheck = new GameObject[CNT_SELECT];

		int m_iSelIdx = 0; // 현재 선택되고 있는 index
		SelectState m_selectState = null;

		#region Unity 함수
			public void Update()
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					EndSelect();
				}
				else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
				{
					SetSelIdx(m_iSelIdx == 0 ? 1 : 0);
				}
			}
		#endregion

		/// <summary>
		/// 선택지 텍스트 셋팅 및 초기화
		/// </summary>
		/// <param name="selectState"></param>
		public void SetSelectText(SelectState selectState)
		{
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
			m_iSelIdx = iSelIdx;
			for(int i = 0; i < CNT_SELECT; i++)
				m_goCheck[i].SetActive(iSelIdx == i);
		}

		/// <summary>
		/// 선택지 선택 완료
		/// </summary>
		public void EndSelect()
		{
			if(m_selectState.m_select.m_iAnsSwitchOnIdx[m_iSelIdx] != 0)
				MainManager.instance.gameSwitch.SetSwitch(m_selectState.m_select.m_iAnsSwitchOnIdx[m_iSelIdx], true);

			if (m_selectState.m_select.m_iAnsSwitchOffIdx[m_iSelIdx] != 0)
				MainManager.instance.gameSwitch.SetSwitch(m_selectState.m_select.m_iAnsSwitchOffIdx[m_iSelIdx], false);

			GameStateManager.instance.ChangeState(new DefaultState());
		}
	}
}

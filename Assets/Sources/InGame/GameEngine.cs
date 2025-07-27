using UnityEngine;

namespace Casual
{
	public class GameEngine : MonoBehaviour, IGameEngine
	{
		public static GameEngine instance { get; private set; } = null;

		[SerializeField]
		PlayerController m_player; // 플레이어
		[SerializeField]
		UIDialogue m_UIdialogue; // 대사창 UI
		[SerializeField]
		UISelect m_UISelect; // 선택지창 UI
		[SerializeField]
		UIInGameMenu m_UIInGameMenu; // 인게임메뉴 UI
		[SerializeField]
		public GameObject m_goHideScreen = null;    // 화면이 셋팅되는 동안 화면을 가림		

		[SerializeField]
		GameObject m_goAGroup; // 선택지 따라 변경되는 오브젝트 그룹 A
		[SerializeField]
		GameObject m_goBGroup; // 선택지 따라 변경되는 오브젝트 그룹 B

		private void Awake()
		{
			instance = this;
		}

		private void OnDestroy()
		{
			instance = null;
		}

		/// <summary>
		/// 이벤트 인덱스 확인하여 A Group 과 B Group 의 오브젝트를 키고 끕니다.
		/// </summary>
		/// <param name="iEvtIdx"> 확인할 이벤트 인덱스 </param>
		void _CheckEvntAndOnOff(int iEvtIdx)
		{
			bool isOn = MainManager.instance.gameSwitch.GetSwitch(iEvtIdx);
			m_goAGroup.SetActive(!isOn);
			m_goBGroup.SetActive(isOn);
		}

		public void SetObjects()
		{
			_CheckEvntAndOnOff(4);
		}

		public PlayerController GetPlayer() { return m_player; }		
		public UIDialogue GetUIDialogue() { return m_UIdialogue; }
		public UISelect GetUISelect() { return m_UISelect; }
		public UIInGameMenu GetUIInGameMenu() { return m_UIInGameMenu; }
	}
}

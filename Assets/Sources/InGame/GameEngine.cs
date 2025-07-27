using UnityEngine;

namespace Casual
{
	public class GameEngine : MonoBehaviour, IGameEngine
	{
		public static GameEngine instance { get; private set; } = null;

		[SerializeField]
		PlayerController m_player; // �÷��̾�
		[SerializeField]
		UIDialogue m_UIdialogue; // ���â UI
		[SerializeField]
		UISelect m_UISelect; // ������â UI
		[SerializeField]
		UIInGameMenu m_UIInGameMenu; // �ΰ��Ӹ޴� UI
		[SerializeField]
		public GameObject m_goHideScreen = null;    // ȭ���� ���õǴ� ���� ȭ���� ����		

		[SerializeField]
		GameObject m_goAGroup; // ������ ���� ����Ǵ� ������Ʈ �׷� A
		[SerializeField]
		GameObject m_goBGroup; // ������ ���� ����Ǵ� ������Ʈ �׷� B

		private void Awake()
		{
			instance = this;
		}

		private void OnDestroy()
		{
			instance = null;
		}

		/// <summary>
		/// �̺�Ʈ �ε��� Ȯ���Ͽ� A Group �� B Group �� ������Ʈ�� Ű�� ���ϴ�.
		/// </summary>
		/// <param name="iEvtIdx"> Ȯ���� �̺�Ʈ �ε��� </param>
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

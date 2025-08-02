using Casual;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Casual
{
	public class EventCheckOnOff : MonoBehaviour
	{
		[SerializeField]
		int m_iEventIdx;
		[SerializeField]
		bool isOn;
		[SerializeField]
		GameObject m_goOwn = null;

		EventCheckOnOffLogic m_logic;

		private void Awake()
		{
			var container = LifetimeScope.Find<RootLifetimeScope>().Container;
			var gameSwitch = container.Resolve<GameSwitch>();
			m_logic = new EventCheckOnOffLogic(gameSwitch);
		}

		private void OnDestroy()
		{
			m_logic = null;
		}

		public void SetOnOff()
		{
			bool isCheck = m_logic.IsSwitchOn(m_iEventIdx);
			m_goOwn.SetActive(isOn ? isCheck : !isCheck);
		}
	}

	public class EventCheckOnOffLogic
	{
		GameSwitch m_gameSwitch;
		public EventCheckOnOffLogic(GameSwitch gameSwitch)
		{
			m_gameSwitch = gameSwitch;
		}

		public bool IsSwitchOn(int iSwitchIdx)
		{
			return m_gameSwitch.GetSwitch(iSwitchIdx);
		}
	}
}

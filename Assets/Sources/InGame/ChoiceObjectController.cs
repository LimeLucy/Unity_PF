using UnityEngine;
using VContainer;

namespace Casual
{
	public interface IChoiceObjectController
	{
		void UpdateObjects();
	}

	public class ChoiceObjectController : MonoBehaviour, IChoiceObjectController
	{
		[SerializeField]
		GameObject m_goAGroup = null;
		[SerializeField]
		GameObject m_goBGroup = null;
		[SerializeField]
		int m_eventIdx = 4;

		public void UpdateObjects()
		{
			var gameSwitch = GameLifetimeScope.Find<RootLifetimeScope>().Container.Resolve<GameSwitch>();
			bool isOn = gameSwitch.GetSwitch(m_eventIdx);
			m_goAGroup.SetActive(!isOn);
			m_goBGroup.SetActive(isOn);
		}
	}
}
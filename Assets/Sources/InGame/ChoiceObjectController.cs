using UnityEngine;

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
			bool isOn = MainManager.instance.gameSwitch.GetSwitch(m_eventIdx);
			Debug.Log(m_eventIdx + "¿©±â" + isOn);
			m_goAGroup.SetActive(!isOn);
			m_goBGroup.SetActive(isOn);
		}
	}
}
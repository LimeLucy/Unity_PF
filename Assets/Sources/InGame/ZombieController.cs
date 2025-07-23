using UnityEngine;

namespace Casual
{
	public class ZombieController : MonoBehaviour
	{
		public enum eEvtState {
			None,
			Dialogue,
			Select,
		}
		[SerializeField]
		Scripts m_script = null;

		/// <summary>
		/// 스크립트 run
		/// </summary>
		public void RunEvent()
		{
			bool isCheckEvt = MainManager.instance.gameSwitch.GetSwitch(m_script.m_iCheckEventIdx);
			if ((isCheckEvt && !string.IsNullOrEmpty(m_script.m_strTrueText)) || (!isCheckEvt))
				GameStateManager.instance.ChangeState(new DialogueState(m_script));
			else
				GameStateManager.instance.ChangeState(new SelectState(m_script.m_trueSelect));
		}
	}
}

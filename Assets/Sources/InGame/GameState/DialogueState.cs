using System.Collections;

namespace Casual
{
	/// <summary>
	/// 대사 상태
	/// </summary>
	public class DialogueState : IGameState
	{
		Scripts m_script = null;

		public DialogueState(Scripts script)
		{		
			m_script = script;		
		}

		/// <summary>
		/// 대사 UI 셋팅
		/// </summary>
		/// <returns></returns>
		public IEnumerator Enter()
		{
			yield return GameEngine.instance.GetUIDialogue().SetDialogueText(m_script);
		}

		/// <summary>
		/// 대사 UI 숨김, 대사 사용 이벤트 스위치 ON
		/// </summary>
		/// <returns></returns>

		public IEnumerator Exit()
		{
			if(m_script.m_iOnEventIdx != -1)
				MainManager.instance.gameSwitch.SetSwitch(m_script.m_iOnEventIdx, true);
			GameEngine.instance.GetUIDialogue().HideDialogueUI();		
			yield return null;
		}
	}
}
